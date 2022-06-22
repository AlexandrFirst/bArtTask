using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using testWork.Abstractions;
using testWork.Data;
using testWork.Data.Domain;

namespace testWork.Repositries
{
    public class ContactRepositry : BaseRepositry<Contact, ContactRepositry>
    {
        public ContactRepositry(DataContext context, ILogger<ContactRepositry> logger)
        : base(context, logger) { }

        public override async Task<Contact> Read(Guid id)
        {
            var contact = await context.Contacts.FirstOrDefaultAsync(a => a.ContactId == id);
            return contact;
        }
    }
}