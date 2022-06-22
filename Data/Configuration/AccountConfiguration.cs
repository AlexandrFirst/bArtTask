using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using testWork.Data.Domain;

namespace testWork.Data.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(k => k.AccountId);
            builder.Property(k => k.AccountId).ValueGeneratedOnAdd();

            builder.HasMany(c => c.Contacts).WithOne(a => a.Account).OnDelete(DeleteBehavior.SetNull);
            builder.HasIndex(n => n.Name).IsUnique();
        }
    }
}