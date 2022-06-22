using System;

namespace testWork.Dtos.Contact
{
    public class ReadContactDto
    {
        public Guid ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}