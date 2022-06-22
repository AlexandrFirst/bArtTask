using System;
using System.Collections.Generic;
using testWork.Dtos.Account;

namespace testWork.Dtos.Incident
{
    public class ReadIncidentDto
    {
        public Guid IncidentId { get; set; }
        public String Desciption { get; set; }

        public IEnumerable<ReadAccountDto> Accounts { get; set; }
    }
}