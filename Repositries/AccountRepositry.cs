using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using testWork.Abstractions;
using testWork.Data;
using testWork.Data.Domain;

namespace testWork.Repositries
{
    public class AccountRepositry : BaseRepositry<Account, AccountRepositry>
    {

        public AccountRepositry(DataContext context,
                                ILogger<AccountRepositry> logger)
                                : base(context, logger) { }

        public override async Task<Account> Read(Guid id)
        {
            var account = await context.Accounts
                .Include(c => c.Contacts)
                .FirstOrDefaultAsync(a => a.AccountId == id);
            return account;
        }
    }
}