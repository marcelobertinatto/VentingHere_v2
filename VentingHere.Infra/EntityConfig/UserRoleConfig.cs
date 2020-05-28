﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Domain.Entities;

namespace VentingHere.Infra.EntityConfig
{
    class UserRoleConfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            //many to many: UserRoles with Roles
            builder.HasOne(ur => ur.Role)
                .WithMany(r => r.ListUserRoles)
                .HasForeignKey(x => x.RoleId)
                .IsRequired();

            //many to many: UserRoles with Users
            builder.HasOne(ur => ur.User)
              .WithMany(r => r.ListUserRoles)
              .HasForeignKey(x => x.UserId)
              .IsRequired();
        }
    }
}
