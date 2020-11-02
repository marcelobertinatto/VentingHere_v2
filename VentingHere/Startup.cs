using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;
using VentingHere.Application;
using VentingHere.Application.Interface;
using VentingHere.AutoMapper;
using VentingHere.Domain;
using VentingHere.Domain.Entities;
using VentingHere.Domain.Repository.Interfaces;
using VentingHere.Domain.Repository.Services;
using VentingHere.Infra;
using VentingHere.Infra.Repository;
using VentingHere.ModelView;

namespace VentingHere
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<VentingContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:VentingContext"]));

            #region IDENTITY CONFIGS
            IdentityBuilder builder = services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 6;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<VentingContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();
            #endregion

            #region JWT CONFIGS
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            #endregion

            #region AUTOMAPPER CONFIG
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            #region MVC CONFIG
            services.AddMvc(opt =>
            {
                var policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                  .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            #endregion

            #region ANSWER
            services.AddScoped<IRepositoryAnswer, RepositoryAnswer>();
            services.AddScoped<IServiceAnswer, ServiceAnswer>();
            services.AddScoped<IServiceAppAnswer, ServiceAppAnswer>();
            #endregion

            #region COMPANY
            services.AddScoped<IRepositoryCompany, RepositoryCompany>();
            services.AddScoped<IServiceCompany, ServiceCompany>();
            services.AddScoped<IServiceAppCompany, ServiceAppCompany>();
            #endregion

            #region COMPANY RATE
            services.AddScoped<IRepositoryCompanyRate, RepositoryCompanyRate>();
            services.AddScoped<IServiceCompanyRate, ServiceCompanyRate>();
            services.AddScoped<IServiceAppCompanyRate, ServiceAppCompanyRate>();
            #endregion

            #region RATE
            services.AddScoped<IRepositoryRate, RepositoryRate>();
            services.AddScoped<IServiceRate, ServiceRate>();
            services.AddScoped<IServiceAppRate, ServiceAppRate>();
            #endregion

            #region USER
            services.AddScoped<IRepositoryUser, RepositoryUser>();
            services.AddScoped<IServiceUser, ServiceUser>();
            services.AddScoped<IServiceAppUser, ServiceAppUser>();
            #endregion

            #region VENT
            services.AddScoped<IRepositoryVent, RepositoryVent>();
            services.AddScoped<IServiceVent, ServiceVent>();
            services.AddScoped<IServiceAppVent, ServiceAppVent>();
            #endregion

            #region CONTACT
            services.AddScoped<IRepositoryContact, RepositoryContact>();
            services.AddScoped<IServiceContact, ServiceContact>();
            services.AddScoped<IServiceAppContact, ServiceAppContact>();
            #endregion

            #region HANDLE MESSAGE
            services.AddScoped<IHandleMessage<UserDetailsDTO>, HandleMessage<UserDetailsDTO>>();
            services.AddScoped<IHandleMessage<CompanySubjectTellUsDTO>, HandleMessage<CompanySubjectTellUsDTO>>();
            services.AddScoped<IHandleMessage<List<CompanyDTO>>, HandleMessage<List<CompanyDTO>>>();
            services.AddScoped<IHandleMessage<List<SubjectDTO>>, HandleMessage<List<SubjectDTO>>>();
            services.AddScoped<IHandleMessage<List<SubjectIssueDTO>>, HandleMessage<List<SubjectIssueDTO>>>();
            services.AddScoped<IHandleMessage<UserSummary>, HandleMessage<UserSummary>>();
            #endregion

            #region ROLE
            services.AddScoped<IRepositoryRole, RepositoryRole>();
            services.AddScoped<IServiceRole, ServiceRole>();
            services.AddScoped<IServiceAppRole, ServiceAppRole>();
            #endregion

            #region SUBJECT
            services.AddScoped<IRepositorySubject, RepositorySubject>();
            services.AddScoped<IServiceSubject, ServiceSubject>();
            services.AddScoped<IServiceAppSubject, ServiceAppSubject>();
            #endregion

            #region SUBJECTISSUE
            services.AddScoped<IRepositorySubjectIssue, RepositorySubjectIssue>();
            services.AddScoped<IServiceSubjectIssue, ServiceSubjectIssue>();
            services.AddScoped<IServiceAppSubjectIssue, ServiceAppSubjectIssue>();
            #endregion

            #region COMPANYSUBJECTISSUE
            services.AddScoped<IRepositoryCompanySubjectIssue, RepositoryCompanySubjectIssue>();
            services.AddScoped<IServiceCompanySubjectIssue, ServiceCompanySubjectIssue>();
            services.AddScoped<IServiceAppCompanySubjectIssue, ServiceAppCompanySubjectIssue>();
            #endregion

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    //spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
