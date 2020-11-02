using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentingHere.Domain.Entities;

namespace VentingHere.Infra.EntityConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            //builder.HasKey(x => new { x.Id });
            //builder.Property(x => x.Id).ValueGeneratedOnAdd();

            ////one to many: User and Answer
            //builder.HasMany(x => x.ListReplies)
            //    .WithOne(x => x.User)
            //    .HasForeignKey(x => x.UserId);

            ////one to many: User and Rate
            //builder.HasMany(x => x.ListRates)
            //    .WithOne(x => x.User)
            //    .HasForeignKey(x => x.UserId);
        }
    }
}
