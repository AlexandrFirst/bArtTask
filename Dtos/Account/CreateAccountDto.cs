
using System;
using System.Collections.Generic;

namespace testWork.Dtos.Account
{
    public class CreateAccountDto
    {
        public string Name { get; set; }
        public IEnumerable<Guid> Contacts { get; set; }
    }
}