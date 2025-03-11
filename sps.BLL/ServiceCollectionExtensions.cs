using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sps.BLL.Email;  // For BrevoEmailSender
using sps.BLL.Services.Implementations;
using sps.BLL.Services.Interfaces;
using sps.BLL.SMS;
using sps.DAL.DataModel;
using sps.DAL.Repos.Implementations;
using sps.DAL.Repos.Interfaces;
using sps.Domain.Model.Services;

namespace sps.BLL
{
    /// <summary>
    /// Extension methods for setting up services in the business logic layer and repositories in the DAL layer.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the business logic layer services and configures minimal identity (without custom JWT chaining).
        /// </summary>
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // Register SpsDbContext for domain entities.
            var spsConnectionString = configuration.GetConnectionString("SpsDbConnection")
                ?? throw new InvalidOperationException("Connection string 'SpsDbConnection' not found.");
            services.AddDbContext<SpsDbContext>(options =>
                options.UseSqlServer(spsConnectionString));

            // Register SpsIdentityDbContext for Identity tables.
            var identityConnectionString = configuration.GetConnectionString("IdentityConnection")
                ?? throw new InvalidOperationException("Connection string 'IdentityConnection' not found.");
            services.AddDbContext<SpsIdentityDbContext>(options =>
                options.UseSqlServer(identityConnectionString));

            // Configure Identity with minimal identity endpoints.
            services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<SpsIdentityDbContext>()
            .AddDefaultTokenProviders();
            // NOTE: Do NOT chain .AddBearerToken() here—this extension applies to AuthenticationBuilder.

            // Register repositories (DAL).
            services.AddScoped<IStudentRepo, StudentRepo>();
            services.AddScoped<IEducationRepo, EducationRepo>();
            services.AddScoped<ISpsaCaseRepo, SpsaCaseRepo>();
            services.AddScoped<IPeriodRepo, PeriodRepo>();
            services.AddScoped<IPlaceRepo, PlaceRepo>();
            services.AddScoped<IDiagnosisRepo, DiagnosisRepo>();
            services.AddScoped<IEduCategoryRepo, EduCategoryRepo>();
            services.AddScoped<IStudentPaymentRepo, StudentPaymentRepo>();
            services.AddScoped<ISupportingTeacherRepo, SupportingTeacherRepo>();
            services.AddScoped<IOpkvalSupervisionRepo, OpkvalSupervisionRepo>();
            services.AddScoped<IEduStatusRepo, EduStatusRepo>();
            services.AddScoped<IEducationPeriodRateRepo, EducationPeriodRateRepo>();
            services.AddScoped<ITeacherPaymentRepo, TeacherPaymentRepo>();
            services.AddScoped<ISupportTypeRepo, SupportTypeRepo>();

            // Register services (BLL).
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IEducationService, EducationService>();
            services.AddScoped<ISpsaCaseService, SpsaCaseService>();
            services.AddScoped<IPeriodService, PeriodService>();
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<IDiagnosisService, DiagnosisService>();
            services.AddScoped<IEduCategoryService, EduCategoryService>();
            services.AddScoped<IStudentPaymentService, StudentPaymentService>();
            services.AddScoped<ISupportingTeacherService, SupportingTeacherService>();
            services.AddScoped<IOpkvalSupervisionService, OpkvalSupervisionService>();
            services.AddScoped<IEduStatusService, EduStatusService>();
            services.AddScoped<IEducationPeriodRateService, EducationPeriodRateService>();
            services.AddScoped<ITeacherPaymentService, TeacherPaymentService>();
            services.AddScoped<ISupportTypeService, SupportTypeService>();

            // Register Twilio SMS service.
            services.Configure<TwilioSettings>(configuration.GetSection("Twilio"));
            services.AddTransient<ISMSService, SMSService>();

            // Register Email Sender Service.
            services.AddTransient<IEmailSender<IdentityUser<Guid>>, BrevoEmailSender>();

            // Register encryption service.
            services.AddScoped<IEncryptionService, AESEncryptionService>();

            // Register comment service.
            services.AddScoped<ICommentService, CommentService>();

            return services;
        }
    }
}