using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VentingHere.Domain.Entities;

namespace VentingHere.Infra.EntityConfig
{
    public class AnswerConfig : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(x => new { x.Id });
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.ReplyDateTime).HasColumnType("datetime");

            //one to many: Vent and Answer
            builder.HasOne(v => v.Vent)
                .WithMany(a => a.ListReplies)
                .HasForeignKey(x => x.VentId)
                .IsRequired(false);

            //one to many -> User and Answer
            builder.HasOne(x => x.User)
                .WithMany(x => x.ListReplies)
                .HasForeignKey(x => x.UserId)
                .IsRequired(false);
        }
    }
}
