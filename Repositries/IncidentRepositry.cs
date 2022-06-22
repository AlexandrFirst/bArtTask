using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using testWork.Data;
using testWork.Data.Domain;

namespace testWork.Repositries
{
    public class IncidentRepositry : BaseRepositry<Incident, IncidentRepositry>
    {
        public IncidentRepositry(DataContext context, ILogger<IncidentRepositry> logger)
        : base(context, logger) { }

        public override async Task<Incident> Read(Guid id)
        {
            var incident = await context.Incidents
                .Include(i => i.Accounts)
                .ThenInclude(c => c.Contacts)
                .FirstOrDefaultAsync(a => a.IncidentId == id);
            return incident;
        }
    }
}