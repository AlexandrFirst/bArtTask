using System;
using System.Collections.Generic;

namespace testWork.Data.Domain
{
    public class Incident
    {
        public Incident()
        {
            Accounts = new List<Account>();
        }

        public Guid IncidentId { get; set; }
        public String Desciption { get; set; }

        public IEnumerable<Account> Accounts { get; set; }
    }
}