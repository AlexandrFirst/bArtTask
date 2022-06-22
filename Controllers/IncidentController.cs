using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testWork.Abstractions;
using testWork.Common;
using testWork.Data;
using testWork.Data.Domain;
using testWork.Dtos.Contact;
using testWork.Dtos.Incident;

namespace testWork.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncidentController : ControllerBase
    {
        private readonly IRepositry<Incident> incidentRepositry;
        private readonly IRepositry<Contact> contactRepositry;
        private readonly DataContext context;
        private readonly IMapper mapper;

        public IncidentController(IRepositry<Incident> incidentRepositry,
                                    IRepositry<Contact> contactRepositry,
                                    DataContext context,
                                    IMapper mapper)
        {
            this.incidentRepositry = incidentRepositry;
            this.contactRepositry = contactRepositry;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{incidentId}", Name = "GetIncident")]
        public async Task<IActionResult> GetIncident(Guid incidentId)
        {
            var incident = await incidentRepositry.Read(incidentId);
            if (incident == null)
            {
                return NotFound(new ResponseModel() { ResponseMessage = "No incident found" });
            }

            var readIncidentDto = mapper.Map<ReadIncidentDto>(incident);

            return Ok(readIncidentDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddIncident([FromBody] CreateIncidentDto incidentDto)
        {
            var account = await context.Accounts.FirstOrDefaultAsync(a => a.Name == incidentDto.AccountName);
            if (account == null)
            {
                return NotFound(new ResponseModel() { ResponseMessage = "Account is not found" });
            }

            var contact = await context.Contacts.FirstOrDefaultAsync(c => c.Email == incidentDto.ContactEmail);

            CreateContactDto newContactDto = new CreateContactDto()
            {
                Email = incidentDto.ContactEmail,
                FirstName = incidentDto.ContactFirstName,
                LastName = incidentDto.ContactLastName,
            };

            if (contact == null)
            {
                Contact newContact = mapper.Map<Contact>(newContactDto);
                newContact.Account = account;
                await contactRepositry.Add(newContact);
            }
            else
            {
                mapper.Map<CreateContactDto, Contact>(newContactDto, contact);
                contact.Account = account;
                await contactRepositry.Update(contact);
            }

            Incident incident = new Incident() { Desciption = incidentDto.IncidentDescription };
            incident.Accounts.Add(account);

            await incidentRepositry.Add(incident);

            var incidentRead = mapper.Map<ReadIncidentDto>(incident);

            return CreatedAtRoute(nameof(GetIncident), new { incidentId = incident.IncidentId }, incidentRead);
        }

        [HttpDelete("{incidentId}")]
        public async Task<IActionResult> DeleteIncident(Guid incidentId)
        {
            var incident = await incidentRepositry.Read(incidentId);
            if (incident == null)
            {
                return NotFound(new ResponseModel() { ResponseMessage = "Incident is not found" });
            }

            await incidentRepositry.Delete(incident);

            return Ok(new ResponseModel { ResponseMessage = "Incident is deleted" });
        }
    }
}