using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VentingHere.Domain.Entities;
using VentingHere.Infra.EntityConfig;

namespace VentingHere.Infra
{
    public class VentingContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
                                                    UserRole, IdentityUserLogin<int>,
                                                    IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public VentingContext(DbContextOptions<VentingContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Rate> Rate { get; set; }
        public DbSet<Sector> Sector { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<Vent> Vent { get; set; }
        public DbSet<Contact> Contact{ get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<SubjectIssue> SubjectIssue { get; set; }
        public DbSet<CompanySubjectIssue> CompanySubjectIssue { get; set; }
        public DbSet<CompanyRate> CompanyRate { get; set; }
        public DbSet<CompanySector> CompanySector { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //added for avoiding errors related to FK: "Specify ON DELETE NO ACTION or ON UPDATE NO ACTION, or modify other FOREIGN KEY constraints"
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AnswerConfig());
            modelBuilder.ApplyConfiguration(new CompanyConfig());
            modelBuilder.ApplyConfiguration(new RateConfig());
            modelBuilder.ApplyConfiguration(new SectorConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new UserRoleConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());
            modelBuilder.ApplyConfiguration(new VentConfig());
            modelBuilder.ApplyConfiguration(new ContactConfig());
            modelBuilder.ApplyConfiguration(new SubjectConfig());
            modelBuilder.ApplyConfiguration(new SubjectIssueConfig());
            modelBuilder.ApplyConfiguration(new CompanyRateConfig());
            modelBuilder.ApplyConfiguration(new CompanySubjectIssueConfig());
        }
    }
}
