using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using testWork.Abstractions;
using testWork.Common;
using testWork.Data.Domain;
using testWork.Dtos.Account;

namespace testWork.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IRepositry<Account> accountRespositry;
        private readonly IRepositry<Contact> contactRepositry;
        private readonly IMapper mapper;

        public AccountController(
            IRepositry<Account> accountRespositry,
            IRepositry<Contact> contactRepositry,
            IMapper mapper)
        {
            this.accountRespositry = accountRespositry;
            this.contactRepositry = contactRepositry;
            this.mapper = mapper;
        }

        [HttpGet("{accountId}", Name = "GetAccount")]
        public async Task<IActionResult> GetAccount(Guid accountId)
        {
            var account = await accountRespositry.Read(accountId);
            if (account == null)
            {
                return NotFound(new ResponseModel() { ResponseMessage = "Account not found" });
            }

            var readAccount = mapper.Map<ReadAccountDto>(account);
            return Ok(readAccount);
        }

        [HttpPost]
        public async Task<IActionResult> AddAccount(CreateAccountDto accountDto)
        {
            var contacts = accountDto.Contacts;
            if (contacts == null || contacts.Count() == 0)
            {
                return BadRequest(new ResponseModel() { ResponseMessage = "Contact should have an accounts" });
            }


            var account = mapper.Map<Account>(accountDto);

            try
            {
                var newContactList = await findContactsRange(accountDto.Contacts.ToList());
                account.Contacts = newContactList;
            }
            catch (Exception ex)
            {
                return NotFound(new ResponseModel() { ResponseMessage = ex.Message });
            }

            if (!await accountRespositry.Add(account))
            {
                return BadRequest(new ResponseModel() { ResponseMessage = "Account shouk have unique name" });
            }

            var readAccountDto = mapper.Map<ReadAccountDto>(account);

            return CreatedAtRoute(nameof(GetAccount), new { accountId = account.AccountId }, readAccountDto);
        }

        [HttpPut("{accountId}")]
        public async Task<IActionResult> UpdateAccount(Guid accountId, [FromBody] CreateAccountDto accountDto)
        {
            var account = await accountRespositry.Read(accountId);
            if (account == null)
            {
                return NotFound(new ResponseModel() { ResponseMessage = "Account not found" });
            }

            mapper.Map<CreateAccountDto, Account>(accountDto, account);

            if (accountDto.Contacts != null)
            {
                try
                {
                    account.Contacts.Clear();
                    var newContactList = await findContactsRange(accountDto.Contacts.ToList());
                    account.Contacts = newContactList;
                }
                catch (Exception ex)
                {
                    return NotFound(new ResponseModel() { ResponseMessage = ex.Message });
                }
            }

            await accountRespositry.Update(account);

            var accountReadDto = mapper.Map<ReadAccountDto>(account);

            return CreatedAtRoute(nameof(GetAccount), new { accountId = account.AccountId }, accountReadDto);
        }

        private async Task<List<Contact>> findContactsRange(List<Guid> newContacts)
        {
            List<Contact> contactsList = new List<Contact>();
            foreach (var c in newContacts)
            {
                var contact = await contactRepositry.Read(c);
                if (contact == null)
                {
                    throw new Exception("Contact does not exists");
                }
                contactsList.Add(contact);
            }

            return contactsList;
        }

        [HttpDelete("{accountId}")]
        public async Task<IActionResult> DeleteAccount(Guid accountId)
        {
            var account = await accountRespositry.Read(accountId);
            if (account == null)
            {
                return NotFound(new ResponseModel() { ResponseMessage = "Account not found" });
            }

            if (await accountRespositry.Delete(account))
            {
                return Ok(new ResponseModel()
                {
                    ResponseMessage = "Account is deleted"
                });
            }
            else
            {
                return BadRequest(new ResponseModel()
                {
                    ResponseMessage = "Smth went wrong"
                });
            }
        }
    }
}