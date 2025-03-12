using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http;
using sps.API;
using sps.API.Middleware;
using sps.BLL;
using sps.BLL.Services.Implementations;
using sps.Domain.Model.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add Business Logic Layer services, including DbContext, Identity, JWT Auth, Repositories, and other services.
builder.Services.AddBusinessLogicLayer(builder.Configuration);
builder.Services.AddScoped<IEncryptionService, AESEncryptionService>();

// Register Mapster mappings
MappingConfig.RegisterMappings();

// LOGGING
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();

//builder.Logging.SetMinimumLevel(LogLevel.Error);
//builder.WebHost.CaptureStartupErrors(true).UseSetting("detailedErrors", "true");

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "sps API", Version = "v1" });

    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
              new OpenApiSecurityScheme
              {
                  Reference = new OpenApiReference
                  {
                      Type = ReferenceType.SecurityScheme,
                      Id = "Bearer"
                  },
                  Scheme = "Bearer",
                  Name = "Bearer",
                  In = ParameterLocation.Header,
              },
              new List<string>()
        }
    });
});

// CORS configuration - Enhanced to work better with NextAuth
builder.Services.AddCors(options =>
{
    options.AddPolicy("NextAuthPolicy",
        policyBuilder =>
        {
            // Development origins
            var allowedOrigins = new List<string> { "http://localhost:3000", "http://localhost:3001", "exp://192.168.153.179:8081" };
            
            // Production origins - read from appsettings
            var prodOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
            if (prodOrigins != null && prodOrigins.Length > 0)
            {
                allowedOrigins.AddRange(prodOrigins);
            }

            policyBuilder.WithOrigins(allowedOrigins.ToArray())
                         .AllowAnyHeader()
                         .AllowAnyMethod()
                         .AllowCredentials(); // Important for cookies
        });
});

// Configure cookie policy to work with NextAuth
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request
    options.CheckConsentNeeded = context => false;
    
    // Configure cookie settings for cross-domain requests
    options.MinimumSameSitePolicy = SameSiteMode.None;
    options.Secure = CookieSecurePolicy.Always;
});

var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await SeedData.InitializeAsync(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
    // If you need MigrationsEndPoint, ensure you have added the necessary services
    // app.UseMigrationsEndPoint();
}
else
{
    // Remove the default exception handler as we're using our custom one
    // app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Register the global exception middleware (should be early in the pipeline)
app.UseCustomExceptionHandler();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Use the enhanced CORS policy
app.UseCors("NextAuthPolicy");

// Use cookie policy
app.UseCookiePolicy();

// Use our custom JWT middleware (before standard auth middlewares)
app.UseJwtMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// No longer using MapIdentityApi as we'll create custom JSON endpoints instead
// app.MapIdentityApi<IdentityUser<Guid>>();

// Removing this as we'll handle registration through our AuthController
// app.MapPost("/register", () => Results.NotFound("Registration is disabled."))
//    .ExcludeFromDescription();

app.Run();