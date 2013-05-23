using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneFind.Results
{
    public class PersonResult: Result
    {
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String birthdayDay { get; set; }
        public String birthdayMonth { get; set; }
        public String birthdayYear { get; set; }
        public String birthday { get; set; }
    }
}
