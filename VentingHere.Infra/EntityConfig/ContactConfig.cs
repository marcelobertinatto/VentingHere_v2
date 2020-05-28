using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentingHere.Domain.Entities;

namespace VentingHere.Infra.EntityConfig
{
    public class ContactConfig : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(x => new { x.Id });
            builder.Property(x => x.Id).ValueGeneratedOnAdd();           
        }
    }
}
