using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneFind
{
    public static class GlobalVariables
    {
        // the path to the temporary file to store the webresponse
        public const string PATH = "C:\\Windows\\Temp\\actionbase_temp_folder8765\\webresponse.txt";
        public const string PATH_DIRECTORY = @"C:\\Windows\\Temp\\actionbase_temp_folder8765\\";
        // the text that has to be attached to the results file
        public const string ATTACHED_TEXT = "result";
        public const string SEPARATOR = "|";
        public const char NAME_ADDRESS_SEPARATOR = ',';
        public const int NUMBER_OF_SPLITS = 16;
        // PAGINATION_LIMIT_HITTA / PAGINATION_LIMIT_ENIRO 
        // the max number of results returned by the  search engines for every page
        public const int PAGINATION_LIMIT_HITTA = 20;
        public const int PAGINATION_LIMIT_ENIRO = 25;
        public const string COLUMN_HEADER = "searchTerm" + SEPARATOR +
                                            "type" + SEPARATOR +
                                            "firstName" + SEPARATOR +
                                            "lastName" + SEPARATOR +
                                            "companyName" + SEPARATOR +
                                            "phone1" + SEPARATOR +
                                            "phone2" + SEPARATOR +
                                            "phone3" + SEPARATOR +
                                            "streetAddressName" + SEPARATOR +
                                            "streetAddressNumber" + SEPARATOR +
                                            "streetAddressZipcode" + SEPARATOR +
                                            "streetAddressCity" + SEPARATOR +
                                            "boxAddress" + SEPARATOR +
                                            "postAddress" + SEPARATOR +
                                            "eastCoordinate" + SEPARATOR +
                                            "northCoordinate" + SEPARATOR +
                                            "birthday" + SEPARATOR +
                                            "foundPersons" + SEPARATOR +
                                            "foundCompanies";
    }
}
