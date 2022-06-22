using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using testWork.Abstractions;
using testWork.Common;
using testWork.Data.Domain;
using testWork.Dtos.Contact;

namespace testWork.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IRepositry<Contact> contactRespositry;
        private readonly IMapper mapper;

        public ContactController(
            IRepositry<Contact> contactRespositry, 
            IMapper mapper)
        {
            this.contactRespositry = contactRespositry;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddContact(CreateContactDto contactDto)
        {

            Contact contact = mapper.Map<Contact>(contactDto);
            if (await contactRespositry.Add(contact))
            {
                var contactReadDto = mapper.Map<ReadContactDto>(contact);
                return CreatedAtRoute(nameof(GetContact), new { contactId = contact.ContactId }, contactReadDto);
            }
            else
            {
                return BadRequest(new ResponseModel()
                {
                    ResponseMessage = "Somthing went wrong while creating user, mail must be unique"
                });
            }

        }

        [HttpGet("{contactId}", Name = "GetContact")]
        public async Task<IActionResult> GetContact(Guid contactId)
        {
            var contact = await contactRespositry.Read(contactId);
            if (contact == null)
            {
                return NotFound(new ResponseModel() { ResponseMessage = "No contact found" });
            }
            ReadContactDto responseContact = mapper.Map<ReadContactDto>(contact);
            return Ok(responseContact);
        }

        [HttpPut("{contactId}")]
        public async Task<IActionResult> UpdateContact(Guid contactId, [FromBody] CreateContactDto contactDto)
        {
            var contact = await contactRespositry.Read(contactId);
            if (contact == null)
            {
                return NotFound(new ResponseModel() { ResponseMessage = "No contact found" });
            }

            mapper.Map<CreateContactDto, Contact>(contactDto, contact);
            if (await contactRespositry.Update(contact))
            {
                var contactReadDto = mapper.Map<ReadContactDto>(contact);
                return CreatedAtRoute(nameof(GetContact), new { contactId = contact.ContactId }, contactReadDto);
            }
            else
            {
                return BadRequest(new ResponseModel()
                {
                    ResponseMessage = "Somthing went wrong while updaing user, mail must be unique"
                });
            }
        }

        [HttpDelete("{contactId}")]
        public async Task<IActionResult> DeleteContact(Guid contactId)
        {
            var contact = await contactRespositry.Read(contactId);
            if (contact == null)
            {
                return NotFound(new ResponseModel() { ResponseMessage = "No contact found" });
            }

            if (await contactRespositry.Delete(contact))
            {
                return Ok(new ResponseModel()
                {
                    ResponseMessage = "Contatct is deleted"
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