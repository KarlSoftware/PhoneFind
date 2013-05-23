using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using PhoneFind.Results;

namespace PhoneFind
{
    public partial class frmMain : Form
    {
        static String resultFilePath = "";
        //column header for the resuts file
        int numberOfPages;
        String emptyResult = "";
        static int currentWorkingIndex = 0;
        // the number of persons and companies found in the search in Hitta.se
        int foundPersons = 0;
        int foundCompanies = 0;
        static String currentSelectedValueInListbox = "";
        // wait flag when there are more than one result waiting for the user to select his/her choice
        static bool wait = false;
        // flag indicating whether we are before of after pressing the select button for multi results.
        static bool comingFromSelect = false;
        StringFuntions sf = new StringFuntions();
        WebRequestObj wro = new WebRequestObj();
        public frmMain()
        {
            InitializeComponent();
            ofdPhones.Filter = "txt files (*.txt)|*.txt";
            listbxMessages.Items.Clear();
            addItemToListBox(listbxMessages, "Welcome to Action Rex!");
            addItemToListBox(listbxMessages, "Select the file containing the numbers.");
            listviewMultipleResults.ShowItemToolTips = true;
            radioNumber.Checked = true;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            listviewMultipleResults.SmallImageList = imgList;
            btnSelect.Enabled = false;
            btnClear.Enabled = false;
            StreamWriter sw;
            if (!Directory.Exists(GlobalVariables.PATH_DIRECTORY))
            {
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(GlobalVariables.PATH_DIRECTORY);
            }
            if (!File.Exists(GlobalVariables.PATH))
            {
                // Try to create the file.
                sw = File.CreateText(GlobalVariables.PATH);
            }
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            if (ofdPhones.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFile.Text = ofdPhones.FileName;

                // Read the file line by line and add the numbers to the numbers listbox.
                listbxNumbers.Items.Clear();
                String line;
                int numbersCounter = 0;
                System.IO.StreamReader file = new System.IO.StreamReader(ofdPhones.FileName, Encoding.Default, true);
                while ((line = file.ReadLine()) != null)
                {
                    listbxNumbers.Items.Add(line.Trim());
                    numbersCounter++;
                }
                addItemToListBox(listbxMessages, ofdPhones.FileName + " loaded.");
                addItemToListBox(listbxMessages, numbersCounter + " record(s) found in the file.");
                file.Close();
            }
            progressbar.Value = 0;
        }

        // add item to the listbox and move scroll to the end of the listbox for the latest messages to be visibile to the user
        private void addItemToListBox(ListBox listbox, String item)
        {
            listbox.Items.Add(item);
            listbox.SelectedIndex = (listbox.Items.Count - 1);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            currentWorkingIndex = 0;
            listviewMultipleResults.Items.Clear();
            //create results file
            StreamWriter sw;

            if (txtFile.Text == "")
            {
                MessageBox.Show("No records have been loaded." + "\n" + "Use the browse button to load records.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                grpbxPreparations.Enabled = true;
            }
            else
            {
                resultFilePath = sf.resultFilePathGenerator(txtFile.Text, GlobalVariables.ATTACHED_TEXT);
            }
            if (!File.Exists(resultFilePath))
            {
                sw = File.CreateText(resultFilePath);
                sw.WriteLine(GlobalVariables.COLUMN_HEADER);
                sw.Flush();
                sw.Close();
                if (listbxNumbers.Items.Count == 0)
                {
                    MessageBox.Show("No records have been loaded." + "\n" + "Use the browse button to load records.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    grpbxPreparations.Enabled = true;
                }
                else if (!dataSanityCheck(GlobalVariables.SEPARATOR))
                {
                    DialogResult dr = MessageBox.Show("It looks like that the loaded data contains the separator character '" + GlobalVariables.SEPARATOR + "'.\nThe data might have already been processed.\nIf not, do you want Action Rex to remove the separator characters for you?\nThey may cause problems in the future if you want to import the information somewhere.", "Separator Found", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        separatorRemover(GlobalVariables.SEPARATOR);
                        MessageBox.Show("Clean!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    grpbxPreparations.Enabled = true;
                }
                else if (!wro.checkForInternetConnection())
                {
                    MessageBox.Show("No internet connection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    grpbxPreparations.Enabled = true;
                }
                else
                {
                    grpbxPreparations.Enabled = false;
                    wait = false;
                    this.search();
                }
            }
            else
            {
                DialogResult dr = MessageBox.Show("A file with the same name as the result file already exists in: " + resultFilePath + "\nWould you like to delete the file?", "File Exists", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (dr == DialogResult.Yes)
                {
                    TryToDelete(sf.resultFilePathGenerator(txtFile.Text, GlobalVariables.ATTACHED_TEXT));
                    MessageBox.Show("File deleted.\nYou can try again.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.btnClear_Click(this, EventArgs.Empty);
            }

        }

        public PersonResult getPersonInfo(String response)
        {
            PersonResult person = new PersonResult();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(response);
            person.firstName = doc.DocumentNode.SelectSingleNode("//span[@class='hidden first']").InnerHtml;
            person.lastName = doc.DocumentNode.SelectSingleNode("//span[@class='hidden last']").InnerHtml;
            if (doc.DocumentNode.SelectSingleNode("//span[@class='street-address-name']") != null)
            {
                person.streetAddressName = doc.DocumentNode.SelectSingleNode("//span[@class='street-address-name']").InnerHtml;
            }
            if (doc.DocumentNode.SelectSingleNode("//span[@class='street-address-number']") != null)
            {
                person.streetAddressNumber = doc.DocumentNode.SelectSingleNode("//span[@class='street-address-number']").InnerHtml;
            }
            if (doc.DocumentNode.SelectSingleNode("//span[@class='street-address-zipcode']") != null)
            {
                person.streetAddressZipcode = doc.DocumentNode.SelectSingleNode("//span[@class='street-address-zipcode']").InnerHtml;
            }
            if (doc.DocumentNode.SelectSingleNode("//span[@class='street-address-city']") != null)
            {
                person.streetAddressCity = doc.DocumentNode.SelectSingleNode("//span[@class='street-address-city']").InnerHtml;
            }
            if (doc.DocumentNode.SelectSingleNode("//span[@class='street-address-zipcode']") == null || doc.DocumentNode.SelectSingleNode("//span[@class='street-address-city']") == null)
            {
                if (doc.DocumentNode.SelectSingleNode("//div[@class='box-address']") != null)
                {
                    person.boxAddress = "Post Address: " + doc.DocumentNode.SelectSingleNode("//div[@class='box-address']").InnerText;
                }
                if (doc.DocumentNode.SelectSingleNode("//div[@class='post-address']") != null)
                {
                    person.postAddress = doc.DocumentNode.SelectSingleNode("//div[@class='post-address']").InnerText;
                }
            }
            if (doc.DocumentNode.SelectSingleNode("//div[@class='east']") != null)
            {
                person.coordinateEast = doc.DocumentNode.SelectSingleNode("//div[@class='east']").InnerHtml;
            }
            if (doc.DocumentNode.SelectSingleNode("//div[@class='north']") != null)
            {
                person.coordinateNorth = doc.DocumentNode.SelectSingleNode("//div[@class='north']").InnerHtml;
            }
            if (doc.DocumentNode.SelectSingleNode("//span[@id='birthday-day']") != null)
            {
                person.birthdayDay = doc.DocumentNode.SelectSingleNode("//span[@id='birthday-day']").InnerHtml;
            }
            if (doc.DocumentNode.SelectSingleNode("//span[@id='birthday-month']") != null)
            {
                person.birthdayMonth = doc.DocumentNode.SelectSingleNode("//span[@id='birthday-month']").InnerHtml;
            }
            String birthdayYear = "";
            String age = "";
            var birthdayyeardiv = doc.DocumentNode.SelectNodes("//li[@class='desc-list-item birthday']");
            if (birthdayyeardiv != null)
            {
                foreach (HtmlAgilityPack.HtmlNode node in birthdayyeardiv)
                {
                    var childCollection = node.Descendants("span").ToList();
                    if (childCollection != null && childCollection.ToList().Count > 0)
                    {
                        age = childCollection[1].InnerHtml;
                        birthdayYear = (DateTime.Now.Year - Convert.ToInt32(age) + 1).ToString();
                    }
                }
            }
            if (birthdayYear != "")
                person.birthday = birthdayYear + "-" + person.birthdayMonth + "-" + person.birthdayDay;
            else
                person.birthday = "";

            var phonediv = doc.DocumentNode.SelectNodes("//div[@class='phone']");
            if (phonediv != null)
            {
                foreach (HtmlAgilityPack.HtmlNode node in phonediv)
                {
                    var childCollection = node.Descendants("h2");
                    if (childCollection != null && childCollection.ToList().Count > 0)
                        person.phone1 = childCollection.First().LastChild.InnerHtml + " ";
                }
            }
            var telephoneDiv = doc.DocumentNode.SelectNodes("//div[@class='phone-container']");
            if (telephoneDiv != null)
            {
                foreach (HtmlAgilityPack.HtmlNode node in telephoneDiv)
                {
                    var phoneNumber = node.Descendants("h2").ToList();
                    if (phoneNumber[0] != null)
                    {
                        if (person.phone1.Equals(""))
                        {
                            person.phone1 = phoneNumber[0].InnerHtml;
                        }
                        else if (!person.phone1.Equals("") && person.phone2.Equals(""))
                        {
                            person.phone2 = phoneNumber[0].InnerHtml;
                        }
                        else if (!person.phone1.Equals("") && !person.phone2.Equals("") && person.phone3.Equals(""))
                        {
                            person.phone3 = phoneNumber[0].InnerHtml;
                        }
                    }
                }
            }
            return person;
        }

        public CompanyResult getCompanyInfo(String response)
        {
            CompanyResult company = new CompanyResult();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(response);
            if (doc.DocumentNode.SelectSingleNode("//span[@class='street-address-name']") != null)
            {
                company.streetAddressName = doc.DocumentNode.SelectSingleNode("//span[@class='street-address-name']").InnerHtml;
            }
            if (doc.DocumentNode.SelectSingleNode("//span[@class='street-address-number']") != null)
            {
                company.streetAddressNumber = doc.DocumentNode.SelectSingleNode("//span[@class='street-address-number']").InnerHtml;
            }
            if (doc.DocumentNode.SelectSingleNode("//span[@class='street-address-zipcode']") != null)
            {
                company.streetAddressZipcode = doc.DocumentNode.SelectSingleNode("//span[@class='street-address-zipcode']").InnerHtml;
            }
            if (doc.DocumentNode.SelectSingleNode("//span[@class='street-address-city']") != null)
            {
                company.streetAddressCity = doc.DocumentNode.SelectSingleNode("//span[@class='street-address-city']").InnerHtml;
            }
            if (doc.DocumentNode.SelectSingleNode("//div[@class='east']") != null)
            {
                company.coordinateEast = doc.DocumentNode.SelectSingleNode("//div[@class='east']").InnerHtml;
            }
            if (doc.DocumentNode.SelectSingleNode("//div[@class='north']") != null)
            {
                company.coordinateNorth = doc.DocumentNode.SelectSingleNode("//div[@class='north']").InnerHtml;
            }
            company.postAddress = "";
            var nodes = doc.DocumentNode.SelectNodes("//div[@class='address' and h3='Postadress']/div[@class='post-address']");
            if (nodes != null)
            {
                foreach (HtmlAgilityPack.HtmlNode node in nodes)
                    company.postAddress = sf.extraSpaceRemover(node.InnerHtml).Trim();
            }
            if (doc.DocumentNode.SelectSingleNode("//div[@class='box-address']") != null)
            {
                company.boxAddress = doc.DocumentNode.SelectSingleNode("//div[@class='box-address']").InnerHtml;
            }

            var companyNameDiv = doc.DocumentNode.SelectNodes("//div[@class='details-header']");
            String companyName = "";
            if (companyNameDiv != null)
            {
                foreach (HtmlAgilityPack.HtmlNode node in companyNameDiv)
                {
                    var childCollection = node.Descendants("h1");
                    if (childCollection != null && childCollection.ToList().Count > 0)
                        companyName = childCollection.First().LastChild.InnerHtml;
                }
            }
            company.companyName = companyName;
            var telephoneDiv = doc.DocumentNode.SelectNodes("//div[@class='phone-container']");
            if (telephoneDiv != null)
            {
                foreach (HtmlAgilityPack.HtmlNode node in telephoneDiv)
                {
                    var phoneNumber = node.Descendants("h2").ToList();
                    if (phoneNumber[0] != null)
                    {
                        if (company.phone1.Equals(""))
                        {
                            company.phone1 = phoneNumber[0].InnerHtml;
                        }
                        else if (!company.phone1.Equals("") && company.phone2.Equals(""))
                        {
                            company.phone2 = phoneNumber[0].InnerHtml;
                        }
                        else if (!company.phone1.Equals("") && !company.phone2.Equals("") && company.phone3.Equals(""))
                        {
                            company.phone3 = phoneNumber[0].InnerHtml;
                        }
                    }

                }
            }
            return company;
        }

        public List<PersonResult> getPersonsInPage(String response)
        {
            List<PersonResult> persons = new List<PersonResult>();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(response);
            HtmlNodeCollection resultContainer = doc.DocumentNode.SelectNodes("//body[@class='result']");
            if (resultContainer != null)
            {
                foreach (HtmlAgilityPack.HtmlNode node in resultContainer)
                {
                    var childCollection = node.Descendants();
                    if (childCollection != null && childCollection.ToList().Count > 0)
                        foreach (var child in childCollection)
                        {
                            if (child.Name.Equals("a") && child.Attributes["id"] != null && child.Attributes["id"].Value.Contains("person"))
                            {
                                String url = child.Attributes["href"].Value;
                                WebRequestObj request = new WebRequestObj(url, "hitta_direct");
                                String directURLResponse = request.sendRequest();

                                if (directURLResponse != "")
                                {
                                    try
                                    {
                                        PersonResult foundPerson = new PersonResult();
                                        foundPerson = getPersonInfo(directURLResponse);
                                        persons.Add(foundPerson);
                                    }
                                    catch
                                    {
                                        // MessageBox.Show(e.Message, "Error: Retrieving person information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }

                        }
                }
            }
            else
            {
                MessageBox.Show("Result container is null.");
            }
            return persons;
        }

        public List<CompanyResult> getCompaniesInPage(String response)
        {
            List<CompanyResult> companies = new List<CompanyResult>();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(response);
            HtmlNodeCollection resultContainer = doc.DocumentNode.SelectNodes("//body[@class='result']");
            if (resultContainer != null)
            {
                foreach (HtmlAgilityPack.HtmlNode node in resultContainer)
                {
                    var childCollection = node.Descendants();
                    if (childCollection != null && childCollection.ToList().Count > 0)
                        foreach (var child in childCollection)
                        {
                            if (child.Name.Equals("a") && child.Attributes["id"] != null && child.Attributes["id"].Value.Contains("company"))
                            {
                                String url = child.Attributes["href"].Value;

                                WebRequestObj request = new WebRequestObj(url, "hitta_direct");
                                String directURLResponse = request.sendRequest();

                                if (directURLResponse != "")
                                {
                                    try
                                    {
                                        CompanyResult foundCompany = new CompanyResult();
                                        foundCompany = getCompanyInfo(directURLResponse);
                                        companies.Add(foundCompany);
                                    }
                                    catch
                                    {
                                        // MessageBox.Show(e.Message, "Error: Retrieving company information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }

                        }
                }
            }
            else
            {
                MessageBox.Show("Result container is null.");
            }
            return companies;
        }

        /*
         * findResultTypeHitta
         * gets the web response and finds out if the matching result of the search term is one of the following:
         * 1. one person (oneperson)
         * 2. one company (onecompany)
         * 3. x person, y company (x_person_y_company)
         */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2241:Provide correct arguments to formatting methods")]
        public String findResultTypeHitta(String reponsepath)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(reponsepath);
            List<String> itemList = new List<String>();
            IEnumerable<String> v = null;
            var item = doc.DocumentNode.SelectNodes("//body[@id='person']");

            if (item != null) // first check to see if the found result is ONE person.
            {
                v = item.Select(p => p.InnerText);
                itemList = v.ToList();
                if (itemList.Count == 1)
                    return "oneperson";
            }
            else
            {
                item = doc.DocumentNode.SelectNodes("//body[@id='company']");
                if (item != null) // if it's not a person then it's probably ONE company
                {
                    v = item.Select(p => p.InnerText);
                    itemList = v.ToList();
                    if (itemList.Count == 1)
                        return "onecompany";
                }
                else // it's neither. so we have to check case 3 above. 
                {
                    // finding number of persons found, could also be zero. we don't care
                    String numberOfFoundPersons = "";
                    String numberOfFoundCompanies = "";
                    var nodeCollection = doc.DocumentNode.SelectNodes("//div[@id='mixed-header-person']");
                    if (nodeCollection != null && nodeCollection.Count > 0)
                    {
                        foreach (HtmlAgilityPack.HtmlNode div in nodeCollection)
                        {
                            var id = div.GetAttributeValue("id", string.Empty);
                            if (!div.HasChildNodes)
                                MessageBox.Show(string.Format("no children", id));

                            var childCollection = div.Descendants("span");
                            if (childCollection != null && childCollection.ToList().Count > 0)
                                numberOfFoundPersons = childCollection.First().LastChild.InnerHtml;
                        }
                    }

                    // finding number of companies found, could also be zero.
                    nodeCollection.Clear();
                    nodeCollection = doc.DocumentNode.SelectNodes("//div[@id='mixed-header-company']");
                    if (nodeCollection != null && nodeCollection.Count > 0)
                    {
                        foreach (HtmlAgilityPack.HtmlNode div in nodeCollection)
                        {
                            var id = div.GetAttributeValue("id", string.Empty);
                            if (!div.HasChildNodes)
                                MessageBox.Show(string.Format("no children", id));

                            var childCollection = div.Descendants("span");
                            if (childCollection != null && childCollection.ToList().Count > 0)
                                numberOfFoundCompanies = childCollection.First().LastChild.InnerHtml;
                        }
                    }
                    return numberOfFoundPersons + "_" + "person" + "_" + numberOfFoundCompanies + "_" + "company";
                }
            }
            return "Error";
        }

        private void listbxNumbers_MouseHover(object sender, EventArgs e)
        {
            if (listbxNumbers.Items.Count != 0 && listbxNumbers.SelectedItem != null)
                toolTip.Show(listbxNumbers.SelectedItem.ToString(), listbxNumbers);
        }

        private void toolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawText();
            e.DrawBorder();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (listbxNumbers.SelectedIndex == -1)
            {
                MessageBox.Show("No item from numbers table is selected.", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else if (listviewMultipleResults.SelectedItems.Count == 0)
            {
                MessageBox.Show("Select a match from 'Results'!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                String selectedText = listviewMultipleResults.SelectedItems[0].Text;
                int index = listbxNumbers.SelectedIndex;
                String oldValue = listbxNumbers.Items[index].ToString();
                listbxNumbers.Items[index] = "";
                listbxNumbers.Items[index] = currentSelectedValueInListbox + GlobalVariables.SEPARATOR + selectedText + GlobalVariables.SEPARATOR + foundPersons + GlobalVariables.SEPARATOR + foundCompanies;
                StreamWriter sw;
                sw = File.AppendText(resultFilePath);
                sw.WriteLine(listbxNumbers.Items[index]);
                sw.Flush();
                sw.Close();
                index++;
                currentWorkingIndex = index;
                listviewMultipleResults.Items.Clear();
                wait = false;
                comingFromSelect = true;
                this.search();
            }
        }

        private void search()
        {
            String response = "";
            StringFuntions sf = new StringFuntions();
            //create a file to work on the response
            StreamWriter sw;
            if (!comingFromSelect)
                grpbxPreparations.Enabled = false;
            progressbar.Value = currentWorkingIndex;
            progressbar.Maximum = listbxNumbers.Items.Count;            
           
            FileInfo fileInfo = new FileInfo(GlobalVariables.PATH);
            if (sf.IsFileLocked(fileInfo))
            {
                MessageBox.Show("The temporary file needed to work with the search is either used by another program or does not exist.\nFile path: " + GlobalVariables.PATH);
                this.btnClear_Click(this, EventArgs.Empty);
            }
            else
            {
                for (int index = currentWorkingIndex; index < listbxNumbers.Items.Count && !wait; index++)
                {
                    if (index == listbxNumbers.Items.Count - 1)
                    {
                        btnSelect.Enabled = false;
                        btnClear.Enabled = true;
                        grpbxPreparations.Enabled = true;
                    }
                    var selectedItem = listbxNumbers.Items[index];
                    listbxNumbers.SelectedIndex = index;
                    string[] selectedItem_split = selectedItem.ToString().Split(GlobalVariables.NAME_ADDRESS_SEPARATOR);
                    WebRequestObj request = null;
                    if (radioNumber.Checked)
                    {
                        request = new WebRequestObj(selectedItem.ToString(), "hitta_general");
                        response = request.sendRequest();
                    }
                    else if (selectedItem_split.Length < 2 && !radioNumber.Checked)
                    {
                        MessageBox.Show("If you select 'Name and Address' there should be two search terms found in the 'Names and Address' table. \nE.g. John Smith, New York", "Type Select Mistake", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        grpbxPreparations.Enabled = true;
                        break;
                    }
                    else
                    {
                        String address = "";
                        for (int j = 1; j < selectedItem_split.Length; j++)
                            address += selectedItem_split[j].ToString();
                        request = new WebRequestObj(selectedItem_split[0] + "/" + address, "hitta_general");
                        response = request.sendRequest();
                    }
                    if (response.Equals("Error"))
                    {
                        addItemToListBox(listbxMessages, "Error sending '" + selectedItem.ToString() + "' to the server.");
                    }
                    else
                    {
                        progressbar.Increment(1);

                        try
                        {
                            File.WriteAllText(GlobalVariables.PATH, String.Empty); // clear the content of the file
                            sw = File.AppendText(GlobalVariables.PATH);
                            sw.WriteLine(response);
                            sw.Flush();
                            sw.Close();
                            String result = findResultTypeHitta(GlobalVariables.PATH);
                            addItemToListBox(listbxMessages, "Searching for '" + listbxNumbers.Items[index] + "'...");

                            switch (result)
                            {
                                case "oneperson":
                                    addItemToListBox(listbxMessages, "One person matching " + listbxNumbers.Items[index] + " found.");
                                    listbxNumbers.Items[index] += GlobalVariables.SEPARATOR + sf.convertObjectToString(getPersonInfo(response), GlobalVariables.SEPARATOR) + GlobalVariables.SEPARATOR + "1" + GlobalVariables.SEPARATOR + "0";
                                    sw = File.AppendText(resultFilePath);
                                    sw.WriteLine(listbxNumbers.Items[index]);
                                    sw.Flush();
                                    sw.Close();
                                    break;
                                case "onecompany":
                                    addItemToListBox(listbxMessages, "One company matching " + listbxNumbers.Items[index] + " found.");
                                    listbxNumbers.Items[index] += GlobalVariables.SEPARATOR + sf.convertObjectToString(getCompanyInfo(response), GlobalVariables.SEPARATOR) + GlobalVariables.SEPARATOR + "0" + GlobalVariables.SEPARATOR + "1";
                                    sw = File.AppendText(resultFilePath);
                                    sw.WriteLine(listbxNumbers.Items[index]);
                                    sw.Flush();
                                    sw.Close();
                                    break;
                                case "0_person_0_company": // try Eniro.se
                                    Eniro eniro = new Eniro();
                                    if (radioNumber.Checked)
                                    {
                                        List<PersonResult> EniroFoundPersons = new List<PersonResult>();
                                        EniroFoundPersons = eniro.searchPerson(selectedItem.ToString(), "number");
                                        if (EniroFoundPersons != null)
                                        {
                                            if (EniroFoundPersons.Count == 1)
                                                listbxNumbers.Items[index] += GlobalVariables.SEPARATOR + sf.convertObjectToString(EniroFoundPersons[0], GlobalVariables.SEPARATOR) + GlobalVariables.SEPARATOR + "1" + GlobalVariables.SEPARATOR + "0";
                                            else
                                            {
                                                emptyResult = "";
                                                for (int h = 0; h < GlobalVariables.NUMBER_OF_SPLITS; h++)
                                                    emptyResult += GlobalVariables.SEPARATOR;
                                                emptyResult += GlobalVariables.SEPARATOR + "0" + GlobalVariables.SEPARATOR + "0";
                                                listbxNumbers.Items[index] = selectedItem.ToString() + emptyResult;
                                            }
                                            sw = File.AppendText(resultFilePath);
                                            sw.WriteLine(listbxNumbers.Items[index]);
                                            sw.Flush();
                                            sw.Close();
                                        }
                                        else
                                        {
                                            emptyResult = "";
                                            for (int h = 0; h < GlobalVariables.NUMBER_OF_SPLITS; h++)
                                                emptyResult += GlobalVariables.SEPARATOR;
                                            emptyResult += GlobalVariables.SEPARATOR + "0" + GlobalVariables.SEPARATOR + "0";
                                            listbxNumbers.Items[index] = selectedItem.ToString() + emptyResult;

                                            sw = File.AppendText(resultFilePath);
                                            sw.WriteLine(listbxNumbers.Items[index]);
                                            sw.Flush();
                                            sw.Close();
                                        }
                                    }
                                    else
                                    {
                                        List<PersonResult> EniroFoundPersons = eniro.searchPerson(selectedItem.ToString(), "nameaddress");
                                        EniroFoundPersons = eniro.searchPerson(selectedItem.ToString(), "nameaddress");
                                        if (EniroFoundPersons != null)
                                        {
                                            if (EniroFoundPersons.Count == 1)
                                                listbxNumbers.Items[index] += GlobalVariables.SEPARATOR + sf.convertObjectToString(EniroFoundPersons[0], GlobalVariables.SEPARATOR) + GlobalVariables.SEPARATOR + "1" + GlobalVariables.SEPARATOR + "0";
                                            else
                                            {
                                                emptyResult = "";
                                                for (int h = 0; h < GlobalVariables.NUMBER_OF_SPLITS; h++)
                                                    emptyResult += GlobalVariables.SEPARATOR;
                                                emptyResult += GlobalVariables.SEPARATOR + "0" + GlobalVariables.SEPARATOR + "0";
                                                listbxNumbers.Items[index] = selectedItem.ToString() + emptyResult;
                                            }
                                            sw = File.AppendText(resultFilePath);
                                            sw.WriteLine(listbxNumbers.Items[index]);
                                            sw.Flush();
                                            sw.Close();
                                        }
                                        else
                                        {
                                            emptyResult = "";
                                            for (int h = 0; h < GlobalVariables.NUMBER_OF_SPLITS; h++)
                                                emptyResult += GlobalVariables.SEPARATOR;
                                            emptyResult += GlobalVariables.SEPARATOR + "0" + GlobalVariables.SEPARATOR + "0";
                                            listbxNumbers.Items[index] = selectedItem.ToString() + emptyResult;

                                            sw = File.AppendText(resultFilePath);
                                            sw.WriteLine(listbxNumbers.Items[index]);
                                            sw.Flush();
                                            sw.Close();
                                        }
                                    }
                                    break;
                                default: // mixed result                                      
                                    currentSelectedValueInListbox = listbxNumbers.Items[index].ToString();
                                    string[] resultArray = result.Split('_');
                                    foundPersons = Convert.ToInt32(resultArray[0]);
                                    foundCompanies = Convert.ToInt32(resultArray[2]);
                                    if (foundPersons + foundCompanies != 0)
                                    {
                                        addItemToListBox(listbxMessages, foundPersons + foundCompanies + " matches for '" + listbxNumbers.Items[index] + "' found.");
                                        if (!checkbxAutomaticCheck.Checked)
                                            addItemToListBox(listbxMessages, "Select the correct item!");
                                    }
                                    if (foundCompanies == 0 && foundPersons == 0 && !wait)
                                    {
                                        addItemToListBox(listbxMessages, "No match found for '" + listbxNumbers.Items[index] + "'.");
                                        emptyResult = "";
                                        for (int h = 0; h < GlobalVariables.NUMBER_OF_SPLITS; h++)
                                            emptyResult += GlobalVariables.SEPARATOR;
                                        emptyResult += GlobalVariables.SEPARATOR + "0" + GlobalVariables.SEPARATOR + "0";
                                        listbxNumbers.Items[listbxNumbers.SelectedIndex] = currentSelectedValueInListbox + emptyResult;
                                        btnSelect.Enabled = false;
                                        wait = false;
                                    }
                                    if (checkbxAutomaticCheck.Checked)
                                    {
                                        emptyResult = "";
                                        for (int h = 0; h < GlobalVariables.NUMBER_OF_SPLITS; h++)
                                            emptyResult += GlobalVariables.SEPARATOR;
                                        emptyResult += GlobalVariables.SEPARATOR + foundPersons + GlobalVariables.SEPARATOR + foundCompanies;
                                        listbxNumbers.Items[listbxNumbers.SelectedIndex] = currentSelectedValueInListbox + emptyResult;
                                        sw = File.AppendText(resultFilePath);
                                        sw.WriteLine(listbxNumbers.Items[listbxNumbers.SelectedIndex]);
                                        sw.Flush();
                                        sw.Close();
                                        btnSelect.Enabled = false;
                                        wait = false;
                                    }
                                    else
                                    {
                                        if (foundCompanies != 0)
                                        {
                                            numberOfPages = (int)Math.Ceiling((double)foundCompanies / GlobalVariables.PAGINATION_LIMIT_ENIRO);
                                            if (numberOfPages == 1)
                                                numberOfPages = 2;
                                            for (int j = 2; j <= numberOfPages; j++)
                                            {
                                                WebRequestObj requestCompanies = null;
                                                if (radioNumber.Checked)
                                                    requestCompanies = new WebRequestObj(selectedItem.ToString(), "hitta_company_page", j.ToString());
                                                else
                                                    requestCompanies = new WebRequestObj(selectedItem_split[0].ToString(), selectedItem_split[1].ToString(), "hitta_company_page_nameaddress", j.ToString());
                                                response = requestCompanies.sendRequest();
                                                File.WriteAllText(GlobalVariables.PATH, String.Empty); // clear the content of the file
                                                sw = File.AppendText(GlobalVariables.PATH);
                                                sw.WriteLine(response);
                                                sw.Flush();
                                                sw.Close();
                                                List<CompanyResult> companyList = getCompaniesInPage(response);
                                                foreach (CompanyResult company in companyList)
                                                {
                                                    ListViewItem itemInfo = new ListViewItem(sf.convertObjectToString(company, GlobalVariables.SEPARATOR), 1);
                                                    listviewMultipleResults.Items.AddRange(new ListViewItem[] { itemInfo });
                                                }
                                            }
                                            btnSelect.Enabled = true;
                                            btnClear.Enabled = true;
                                            if (!checkbxAutomaticCheck.Checked)
                                                wait = true;

                                        }
                                        if (foundPersons != 0)
                                        {
                                            btnSelect.Enabled = true;
                                            btnClear.Enabled = true;
                                            numberOfPages = (int)Math.Ceiling((double)foundPersons / GlobalVariables.PAGINATION_LIMIT_ENIRO);
                                            if (numberOfPages == 1)
                                                numberOfPages = 2;
                                            for (int j = 2; j <= numberOfPages; j++)
                                            {
                                                WebRequestObj requestPersons = null;
                                                if (radioNumber.Checked)
                                                    requestPersons = new WebRequestObj(selectedItem.ToString(), "hitta_person_page", j.ToString());
                                                else
                                                    requestPersons = new WebRequestObj(selectedItem_split[0].ToString(), selectedItem_split[1].ToString(), "hitta_person_page_nameaddress", j.ToString());
                                                response = requestPersons.sendRequest();
                                                File.WriteAllText(GlobalVariables.PATH, String.Empty); // clear the content of the file
                                                sw = File.AppendText(GlobalVariables.PATH);
                                                sw.WriteLine(response);
                                                sw.Flush();
                                                sw.Close();
                                                List<PersonResult> personList = getPersonsInPage(response);
                                                foreach (PersonResult person in personList)
                                                {
                                                    ListViewItem itemInfo = new ListViewItem(sf.convertObjectToString(person, GlobalVariables.SEPARATOR), 0);
                                                    listviewMultipleResults.Items.AddRange(new ListViewItem[] { itemInfo });
                                                }
                                            }
                                            if (!checkbxAutomaticCheck.Checked)
                                                wait = true;
                                        }
                                    }
                                    break;
                            }
                            listbxNumbers.SelectedIndex = index;
                        }
                        catch (IOException ioe)
                        {
                            MessageBox.Show(ioe.Message, "IOException", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
            }

        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (listbxNumbers.Items.Count != 0)
                {
                    StreamWriter sw;
                    if (!txtFile.Text.Equals(""))
                        File.WriteAllText(txtFile.Text, String.Empty); // clear the content of the file
                    sw = File.AppendText(txtFile.Text);
                    for (int k = 0; k < listbxNumbers.Items.Count; k++)
                        sw.WriteLine(listbxNumbers.Items[k]);
                    sw.Flush();
                    sw.Close();
                    MessageBox.Show("New data written on the file.", "Save File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    addItemToListBox(listbxMessages, "New data saved in '" + txtFile.Text + "'.");
                }
                else
                {
                    MessageBox.Show("No data loaded.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (IOException ioe)
            {
                MessageBox.Show(ioe.Message, "IOException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show("No data loaded:" + ae.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void radioNumber_CheckedChanged(object sender, EventArgs e)
        {
            if (radioNumber.Checked)
            {
                grpbxNumbers.Text = "Numbers";
            }
            else
            {
                grpbxNumbers.Text = "Name and Address";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            grpbxPreparations.Enabled = true;
            listbxNumbers.Items.Clear();
            listviewMultipleResults.Items.Clear();
            listbxMessages.Items.Clear();
            addItemToListBox(listbxMessages, "Welcome to Action Rex!");
            addItemToListBox(listbxMessages, "Select the file containing the numbers.");
            listviewMultipleResults.ShowItemToolTips = true;
            radioNumber.Checked = true;
            btnSelect.Enabled = false;
            btnClear.Enabled = false;
            progressbar.Value = 0;
            txtFile.Text = "";
        }

        /* 
         * dataSanityCheck(String separator)
         * checks the entry list to see if they already contain the separator character.
         * if yes the search cannot be done,
         * because either the list has already been processed or the user has to clean it from the separator character
         */
        private Boolean dataSanityCheck(String separator)
        {
            String line = "";
            for (int index = 0; index < listbxNumbers.Items.Count; index++)
            {
                line = listbxNumbers.Items[index].ToString();
                if (line.Contains(separator))
                    return false;
            }
            return true;
        }


        private void separatorRemover(String separator)
        {
            String line = "";
            for (int index = 0; index < listbxNumbers.Items.Count; index++)
            {
                line = listbxNumbers.Items[index].ToString();
                line = line.Replace(separator, " ");
                listbxNumbers.Items[index] = line;
            }
        }

        static bool TryToDelete(string f)
        {
            try
            {
                File.Delete(f);
                return true;
            }
            catch (IOException)
            {
                MessageBox.Show("Error trying to delete the file.");
                return false;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnClear_Click(this, EventArgs.Empty);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnLoadFile_Click(this, EventArgs.Empty);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // The user wants to exit the application. Close everything down.
            Application.Exit();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHelp frmHelp = new frmHelp();
            frmHelp.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            helpToolStripMenuItem_Click(this, EventArgs.Empty);
        }

        private void txtFile_MouseHover(object sender, EventArgs e)
        {
            if (!txtFile.Text.Equals(""))
                toolTip.Show(txtFile.Text, txtFile);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (File.Exists(GlobalVariables.PATH))
                    Directory.Delete(GlobalVariables.PATH_DIRECTORY, true);
            }
            catch 
            {
                //MessageBox.Show("Cannot delete the temporary file created:\nError Message: " + ioe.ToString());
            }
        }

        private void txtFile_KeyPress(object sender, KeyPressEventArgs e)
        {
                e.Handled = true;
        }

    }
}
