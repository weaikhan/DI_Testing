using System;
using System.Collections.Generic;

namespace DI_Testing.Models
{
    public class PeopleViewModel
    {
        public List<People> peoples { get; set; }
    }

    public class People
    {
        public People(string Name, string Address, string PhoneNo)
        {
            this.Name = Name;
            this.Address = Address;
            this.PhoneNo = PhoneNo;

        }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
    }
}
