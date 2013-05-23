using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.IO;

namespace PhoneFind
{
    class WebRequestObj
    {
        private String searchTerm1 { get; set; }
        private String searchTerm2 { get; set; }
        private String searchEngine { get; set; }
        private String pageNumber { get; set; }

        public WebRequestObj()
        {
        }
        public WebRequestObj(String searchTerm, String searchEngine)
        {
            this.searchTerm1 = searchTerm;
            this.searchEngine = searchEngine;
        }
        public WebRequestObj(String searchTerm, String searchEngine, String pageNumber)
        {
            this.searchTerm1 = searchTerm;
            this.searchEngine = searchEngine;
            this.pageNumber = pageNumber;
        }
        public WebRequestObj(String searchTerm1, String searchTerm2, String searchEngine, String pageNumber)
        {
            this.searchTerm1   = searchTerm1;
            this.searchTerm2 = searchTerm2;
            this.searchEngine = searchEngine;
            this.pageNumber = pageNumber;
        }


        // sends a request to the search engine and returns the response as string
        public String sendRequest()
        {
            Stream S_DataStream;
            StreamReader SR_DataStream;
            //Create a Web-Request to a URL
            HttpWebRequest HWR_Request = null;
            HttpWebResponse HWR_Response = null;
            string s_ResponseString = "";
            try
            {
                switch (searchEngine)
                {
                    case "hitta_general":
                        HWR_Request = (HttpWebRequest)WebRequest.Create("http://www.hitta.se/" + searchTerm1 + "/företag_och_personer");
                        break;
                    case "hitta_person_page":
                        HWR_Request = (HttpWebRequest)WebRequest.Create("http://www.hitta.se/" + searchTerm1 + "/personer/" + pageNumber + "/");
                        break;
                    case "hitta_company_page":
                        HWR_Request = (HttpWebRequest)WebRequest.Create("http://www.hitta.se/" + searchTerm1 + "/företag/" + pageNumber + "/");
                        break;
                    case "hitta_person_page_nameaddress":
                        HWR_Request = (HttpWebRequest)WebRequest.Create("http://www.hitta.se/" + searchTerm1 + "/" + searchTerm2 + "/personer/" + pageNumber + "/");
                        break;
                    case "hitta_company_page_nameaddress":
                        HWR_Request = (HttpWebRequest)WebRequest.Create("http://www.hitta.se/" + searchTerm1 + "/" + searchTerm2 + "/företag/" + pageNumber + "/");
                        break;
                    case "hitta_direct":
                        HWR_Request = (HttpWebRequest)WebRequest.Create("http://www.hitta.se/" + searchTerm1);
                        break;
                    case "eniro_general_person":
                        HWR_Request = (HttpWebRequest)WebRequest.Create("http://personer.eniro.se/resultat/" + searchTerm1);
                        break;
                    case "eniro_general_company":
                        HWR_Request = (HttpWebRequest)WebRequest.Create("http://gulasidorna.eniro.se/hitta:" + searchTerm1);
                        break;
                }
                //receive a Web-Response
               HWR_Response = (HttpWebResponse)HWR_Request.GetResponse();
            }
            catch
            {
                s_ResponseString = "Error";
            }
            if (HWR_Response.GetResponseStream() != null)
            {
                //Translate data from the Web-Response to a string
                S_DataStream = HWR_Response.GetResponseStream();
                SR_DataStream = new StreamReader(S_DataStream, Encoding.UTF8);
                s_ResponseString = SR_DataStream.ReadToEnd();
                S_DataStream.Close();
            }
            else
            {
                s_ResponseString = "Error";
            }
            return s_ResponseString;
        }

        public bool checkForInternetConnection()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
