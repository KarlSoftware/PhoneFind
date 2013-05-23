using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneFind
{
    public class Result
    {
        public String streetAddressName { get; set; }
        public String streetAddressNumber { get; set; }
        public String streetAddressZipcode { get; set; }
        public String streetAddressCity { get; set; }
        public String boxAddress { get; set; }
        public String postAddress { get; set; }
        public String coordinateEast { get; set; }
        public String coordinateNorth { get; set; }
        public String phone1 { get; set; }
        public String phone2 { get; set; }
        public String phone3 { get; set; }
        public Result()
        {
            this.streetAddressName = "";
            this.streetAddressNumber = "";
            this.streetAddressZipcode = "";
            this.streetAddressCity = "";
            this.boxAddress = "";
            this.postAddress = "";
            this.coordinateEast = "";
            this.coordinateNorth = "";
            this.phone1 = "";
            this.phone2 = "";
            this.phone3 = "";
        }
    }
}
