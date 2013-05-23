using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneFind.Results;
using HtmlAgilityPack;

namespace PhoneFind
{
    class Eniro
    {
        private String searchTerm { get; set; }
        private List<String> resultsList = new List<String>();
        WebRequestObj request = null;
        String response;
        public Eniro()
        {
        }
        // search type can be "number" or "nameaddress"
        public List<PersonResult> searchPerson(String searchTerm, String searchType)
        {
            List<PersonResult> results = new List<PersonResult>();
            if (searchType.Equals("number"))
                request = new WebRequestObj(searchTerm, "eniro_general_person");
            else if (searchType.Equals("nameaddress"))
            {
                string[] selectedItem_split = searchTerm.ToString().Split(GlobalVariables.NAME_ADDRESS_SEPARATOR);
                request = new WebRequestObj(selectedItem_split[0] + "/" + selectedItem_split[1], "eniro_general_person");
            }
            response = request.sendRequest();
            StreamWriter sw;
            File.WriteAllText(GlobalVariables.PATH, String.Empty); // clear the content of the file
            sw = File.AppendText(GlobalVariables.PATH);
            sw.WriteLine(response);
            sw.Flush();
            sw.Close();
            String resultPerson = findResultTypePersonEniro(GlobalVariables.PATH);
            switch (resultPerson)
            {
                case "nothing":
                    return null;
                case "oneperson":
                    if (getPersonInfo(response) != null)
                    {
                        results.Add(getPersonInfo(response));
                        return results;
                    }
                    break;
                default:
                    return null;
            }
            return null;
        }

        public List<CompanyResult> searchCompany(String searchTerm, String searchType)
        {
            return null;
        }


        public String findResultTypePersonEniro(String reponsepath)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(reponsepath);
            List<String> itemList = new List<String>();
            // first check to see if there are no persons found. 
            // if an infobox element is found, that means there hasn't been any found records
            var item = doc.DocumentNode.SelectNodes("//div[@class='infobox']");
            if (item != null)
                return "nothing";
            // check to see if the result is only one person
            // if there's a span tag with the class name index that means more than one result is found.
            var bodyItem = doc.DocumentNode.SelectNodes("//span[@class='index']");
            if (bodyItem != null)
                return "multiperson";
            else
                return "oneperson";
        }

        public PersonResult getPersonInfo(String response)
        {
            PersonResult person = new PersonResult();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(response);
            person.firstName = doc.DocumentNode.SelectSingleNode("//span[@class='given-name']").InnerHtml;
            person.lastName = doc.DocumentNode.SelectSingleNode("//span[@class='family-name']").InnerHtml;
            if (doc.DocumentNode.SelectSingleNode("//span[@class='street-address']") != null)
            {
                person.streetAddressName = doc.DocumentNode.SelectSingleNode("//span[@class='street-address']").InnerText;
            }
            if (doc.DocumentNode.SelectSingleNode("//span[@class='postal-code']") != null)
            {
                person.streetAddressZipcode = doc.DocumentNode.SelectSingleNode("//span[@class='postal-code']").InnerHtml;
            }
            if (doc.DocumentNode.SelectSingleNode("//span[@class='locality']") != null)
            {
                person.streetAddressCity = doc.DocumentNode.SelectSingleNode("//span[@class='locality']").InnerHtml;
            }
            if (doc.DocumentNode.SelectSingleNode("//span[@class='latitude']") != null)
            {
                person.coordinateEast = doc.DocumentNode.SelectSingleNode("//span[@class='latitude']").InnerHtml;
            }
            if (doc.DocumentNode.SelectSingleNode("//span[@class='longitude']") != null)
            {
                person.coordinateNorth = doc.DocumentNode.SelectSingleNode("//span[@class='longitude']").InnerHtml;
            }
            HtmlNodeCollection mobilePhoneContainer = doc.DocumentNode.SelectNodes("//span[@class='tel type-phone_normal_mobile']");
            if (mobilePhoneContainer != null)
            {
                foreach (HtmlAgilityPack.HtmlNode node in mobilePhoneContainer)
                {
                    var childCollection = node.Descendants();
                    if (childCollection != null && childCollection.ToList().Count > 0)
                        foreach (var child in childCollection)
                        {
                            if (child.Name.Equals("a") && child.Attributes["class"] != null && child.Attributes["class"].Value == "value")
                                person.phone1 = child.InnerHtml;
                        }
                }
            }
            HtmlNodeCollection landlinePhoneContainer = doc.DocumentNode.SelectNodes("//span[@class='tel type-phone_normal_land_line']");
            if (landlinePhoneContainer != null)
            {               
                foreach (HtmlAgilityPack.HtmlNode node in landlinePhoneContainer)
                {
                    var childCollection = node.Descendants();
                    if (childCollection != null && childCollection.ToList().Count > 0)
                        foreach (var child in childCollection)
                        {
                            if (child.Name.Equals("a") && child.Attributes["class"] != null && child.Attributes["class"].Value == "value")
                                person.phone2 = child.InnerHtml;
                        }                   
                }
            }
            return person;
        }
    }
}
