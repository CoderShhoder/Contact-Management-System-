using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assessment
{
    public class ObjContact
    {
        public int ContactID { get; set; }
        public string CompanyName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Category { get; set; }
        public int ProfessionID { get; set; }
    }
}