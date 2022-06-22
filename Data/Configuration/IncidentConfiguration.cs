using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using testWork.Data.Domain;

namespace testWork.Data.Configuration
{
    public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
    {
        public void Configure(EntityTypeBuilder<Incident> builder)
        {
            builder.HasKey(k => k.IncidentId);
            builder.Property(k => k.IncidentId).ValueGeneratedOnAdd();

            builder.HasMany(c => c.Accounts).WithOne(a => a.Incident).OnDelete(DeleteBehavior.SetNull);
            
        }
    }
}