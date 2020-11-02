using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentingHere.Domain.Entities;

namespace VentingHere.Infra.EntityConfig
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.NormalizedName).HasName("RoleNameIndex").IsUnique();
            builder.Property(x => x.ConcurrencyStamp).IsConcurrencyToken();

            builder.HasMany<UserRole>()
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId)
                .IsRequired();

            builder.HasMany<IdentityRoleClaim<int>>()
                .WithOne()
                .HasForeignKey(x => x.RoleId)
                .IsRequired();
        }
    }
}
