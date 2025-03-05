using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using sps.BLL.Services.Implementations;
using sps.BLL.Services.Interfaces;
using sps.BLL.SMS;
using sps.DAL.DataModel;
using sps.DAL.Repos.Implementations;
using sps.DAL.Repos.Interfaces;
using sps.Domain.Model.Services;
using System.Text;

namespace sps.BLL
{
    /// <summary>
    /// Extension methods for setting up services in the business logic layer and setting up repositories in the DAL layer.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the business logic layer services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The service collection to add services to.</param>
        /// <param name="configuration">The configuration instance.</param>
        /// <returns>The updated service collection.</returns>
        /// <exception cref="InvalidOperationException">Thrown when a required configuration value is missing.</exception>
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // Register SpsDbContext for SPS domain entities
            var spsConnectionString = configuration.GetConnectionString("SpsDbConnection")
                ?? throw new InvalidOperationException("Connection string 'SpsDbConnection' not found.");

            services.AddDbContext<SpsDbContext>(options =>
                options.UseSqlServer(spsConnectionString));

            // Register SpsIdentityDbContext for Identity tables
            var identityConnectionString = configuration.GetConnectionString("IdentityConnection")
                ?? throw new InvalidOperationException("Connection string 'IdentityConnection' not found.");

            services.AddDbContext<SpsIdentityDbContext>(options =>
                options.UseSqlServer(identityConnectionString));

            // Register Identity services with SpsIdentityDbContext
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
            .AddEntityFrameworkStores<SpsIdentityDbContext>() // Use SpsIdentityDbContext for identity stores
            .AddDefaultTokenProviders();

            // Configure JWT authentication
            var secretKey = configuration["Jwt:SecretKey"]
                ?? throw new InvalidOperationException("JWT Secret Key is not configured.");
            var key = Encoding.ASCII.GetBytes(secretKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // Set to true in production
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Register repositories (DAL)
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

            // Register services (BLL)
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

            // Register Twilio SMS service - keeping SMS services as requested
            services.Configure<TwilioSettings>(configuration.GetSection("Twilio"));
            services.AddTransient<ISMSService, SMSService>();

            // Register Email Sender Service - keeping email services as requested
            services.AddTransient<IEmailSender<IdentityUser>, BrevoEmailSender>();

            // Register encryption service
            services.AddScoped<IEncryptionService, AESEncryptionService>();

            return services;
        }
    }
}