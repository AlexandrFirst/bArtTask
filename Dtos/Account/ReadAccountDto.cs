using System;
using System.Collections.Generic;
using testWork.Dtos.Contact;

namespace testWork.Dtos.Account
{
    public class ReadAccountDto
    {
        public Guid AccountId { get; set; }
        public string Name { get; set; }
        public IEnumerable<ReadContactDto> Contacts { get; set; }
    }
}