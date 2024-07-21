using Account.AuthAPI.Data;
using Account.AuthAPI.Models.Auth;
using Account.AuthAPI.Models.Common;
using Account.AuthAPI.Repository.GenericRepository;
using Account.AuthAPI.Repository.Repository;
using Account.AuthAPI.Repository.UnitofWork;
using Account.AuthAPI.Service.Auth;
using AccountManagement.API.Factory;
using AccountManagement.API.Repository.BankAccountRepository;
using AccountManagement.API.Services.StatementService;
using BankAccountAPI.Factory;
using BankAccountAPI.Services.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

// Configure JwtOptions
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));

// Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Add the IAuthService implementation
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IJwtTokenGenerator, JWTTokenGenerator>();

// Add the AccountService implementation
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitofWork>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IAccountFactory, AccountFactory>();

//Add the StatementService implementation

builder.Services.AddTransient<IStatementRepository, StatementRepository>();
builder.Services.AddTransient<IStatementService, StatementService>();
builder.Services.AddTransient<IStatementFactory, StatementFactory>();



// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var jwtOptions = builder.Configuration.GetSection("ApiSettings:JwtOptions").Get<JwtOptions>();
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidAudience = jwtOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
    };
});

// Configure authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(AuthorizePolicy.AdminRole, policy => policy.RequireRole(FixedRoles.Admin, FixedRoles.SuperAdmin));
    options.AddPolicy(AuthorizePolicy.SuperAdminRole, policy => policy.RequireRole(FixedRoles.SuperAdmin));
    options.AddPolicy(AuthorizePolicy.CustomerRole, policy => policy.RequireRole(FixedRoles.Customer));
});

// Add Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Add JWT Authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] { }
    }});
});

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed roles and apply migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        DbInitializer.Initialize(services).Wait();
        ApplyMigration(scope);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

void ApplyMigration(IServiceScope scope)
{
    var _db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (_db.Database.GetPendingMigrations().Any())
    {
        _db.Database.Migrate();
    }
}
