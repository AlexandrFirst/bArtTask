using System;
using System.Collections.Generic;

namespace testWork.Data.Domain
{
    public class Account
    {
        public Account()
        {
            Contacts = new List<Contact>();
        }

        public Guid AccountId { get; set; }
        public String Name { get; set; }

        public Incident Incident { get; set; }

        public List<Contact> Contacts { get; set; }
    }
}