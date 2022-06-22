using System;

namespace testWork.Data.Domain
{
    public class Contact
    {
        public Guid ContactId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }

        public Account Account { get; set; }
    }
}