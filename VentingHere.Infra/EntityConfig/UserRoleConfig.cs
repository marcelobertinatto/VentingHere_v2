using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Domain.Entities;

namespace VentingHere.Infra.EntityConfig
{
    public class UserRoleConfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("AspNetUserRoles");
            builder.HasKey(x => new { x.UserId, x.RoleId });
        }
    }
}
