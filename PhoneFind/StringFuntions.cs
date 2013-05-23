using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PhoneFind.Results;

namespace PhoneFind
{
    class StringFuntions
    {
        public String ISO8859_1_Filter(String input)
        {
            String output = "";
            output = input.Replace("&ouml;", "ö");
            output = output.Replace("&Ouml;", "Ö");
            output = output.Replace("&auml;", "ä");
            output = output.Replace("&Auml;", "Ä");
            output = output.Replace("&aring;", "å");
            output = output.Replace("&Aring;", "Å");
            output = output.Replace("&eacute;", "é");
            output = output.Replace("&egrave;", "è");
            output = output.Replace("&Eacute;", "È");
            output = output.Replace("&Egrave;", "È");
            output = output.Replace("&amp;", "&");
            return output;
        }

        // removes extra spaces from a string.
        public String extraSpaceRemover(String inputString)
        {
            String output = inputString.Replace("&nbsp;", " ");
            Regex trimmer = new Regex(@"\s\s+");
            return trimmer.Replace(output, @" ");
        }

        // receives the file path, attaches a text at its end to indicate the result file path
        public String resultFilePathGenerator(String sourceFilePath, String attachedText)
        {
            sourceFilePath = sourceFilePath.Replace("//", "/");
            string[] sourceFilePath_split = sourceFilePath.Split('/');

            String fileName = sourceFilePath_split[sourceFilePath_split.Length - 1];
            String[] fileName_split = fileName.Split('.');
            String fileNameWithoutExtension = "";
            for (int i = 0; i < fileName_split.Length - 1; i++)
                if (i == 0)
                    fileNameWithoutExtension += fileName_split[i];
                else
                    fileNameWithoutExtension += "." + fileName_split[i];
            String result = fileNameWithoutExtension + "_" + attachedText + ".txt";
            return result;
        }

        // converts the received person/company object into a string
        public String convertObjectToString(Result obj, String separator)
        {
            String result = "";
            switch (obj is PersonResult)
            {
                case true:
                    PersonResult person = obj as PersonResult;
                    result += "person" + separator +// result type
                              person.firstName + separator +
                              person.lastName + separator +
                              "" + separator + // company name
                              person.phone1 + separator +
                              person.phone2 + separator +
                              person.phone3 + separator +
                              person.streetAddressName + separator +
                              person.streetAddressNumber + separator +
                              person.streetAddressZipcode + separator +
                              person.streetAddressCity + separator +
                              person.boxAddress + separator +
                              person.postAddress + separator +
                              person.coordinateEast + separator +
                              person.coordinateNorth + separator +
                              person.birthday;
                    break;
                case false:
                    CompanyResult company = obj as CompanyResult;
                    result += "company" + separator +// result type
                              "" + separator + // first name
                              "" + separator + // last name
                              company.companyName + separator +
                              company.phone1 + separator +
                              company.phone2 + separator +
                              company.phone3 + separator +
                              company.streetAddressName + separator +
                              company.streetAddressNumber + separator +
                              company.streetAddressZipcode + separator +
                              company.streetAddressCity + separator +
                              company.boxAddress + separator +
                              company.postAddress + separator +
                              company.coordinateEast + separator +
                              company.coordinateNorth + separator +
                              ""; // birthday
                    break;
            }
            return ISO8859_1_Filter(extraSpaceRemover(result));
        }

        public virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return false; 
        }
    }

}
