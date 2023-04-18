using AutoMapper;
using MuonRoi.Social_Network.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MuonRoiSocialNetwork.Common.Models;
using MuonRoiSocialNetwork.Common.Settings.Appsettings;
using MuonRoiSocialNetwork.Domains.DomainObjects.Groups;
using MuonRoiSocialNetwork.Infrastructure;
using MuonRoiSocialNetwork.Infrastructure.Extentions.Mail;
using MuonRoiSocialNetwork.Infrastructure.Map.Users;
using MuonRoiSocialNetwork.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Autofac;
using BaseConfig.BaseStartUp;
using Autofac.Extensions.DependencyInjection;
using MuonRoiSocialNetwork.Infrastructure.Repositories.Token;
using MuonRoiSocialNetwork.Infrastructure.Repositories.Users;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands.Users;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands.Token;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries.Users;
using MuonRoiSocialNetwork.Application.Queries.Users;
using MuonRoiSocialNetwork.Infrastructure.Map.GroupAndRoles;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands.GroupAndRoles;
using MuonRoiSocialNetwork.Infrastructure.Repositories.GroupAndRoles;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries.GroupAndRoles;
using MuonRoiSocialNetwork.Application.Queries.GroupAndRoles;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region mediatR
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));
#endregion

#region redis setup
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "MemoryMuonroi_";
});
#endregion

#region swagger
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MuonRoiAPI",
        Description = "Include Story and Social Network API",
        Contact = new OpenApiContact
        {
            Name = "Phi Le",
            Email = "muonroi@outlook.com"
        }
    });
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    var filePath = Path.Combine(AppContext.BaseDirectory, $"{typeof(Program).Assembly.GetName().Name}.xml");
    x.IncludeXmlComments(filePath);
});
#endregion

#region Configuration
IConfiguration configuration = new ConfigurationBuilder().AddJsonFile($"{NameAppSetting.APPSETTINGS}.json").Build();
#endregion

#region mapper
var mapperCfg = new MapperConfiguration(x =>
{
    x.AddProfile(new UserProfile());
    x.AddProfile(new GroupAndRoleProfile());
});
IMapper mapper = mapperCfg.CreateMapper();
#endregion

#region transient
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserQueries, UserQueries>();
builder.Services.AddTransient<IRefreshtokenRepository, RefreshTokenRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IGroupRepository, GroupRepository>();
builder.Services.AddTransient<IRoleQueries, RoleQueries>();
builder.Services.AddTransient<IGroupQueries, GroupQueries>();
#endregion

#region scoped
builder.Services.AddScoped<IEmailService, MailService>();
#endregion

#region Singleton
builder.Services.AddSingleton(mapper);
#endregion

#region db
builder.Services.AddIdentity<AppUser, AppRole>()
       .AddEntityFrameworkStores<MuonRoiSocialNetworkDbContext>()
       .AddDefaultTokenProviders();
builder.Services.AddDbContext<MuonRoiSocialNetworkDbContext>(opt =>
{
    opt.UseSqlServer(configuration[ConstAppSettings.CONNECTIONSTRING]);
});
#endregion

#region Mail
builder.Services.Configure<SMTPConfigModel>(builder.Configuration.GetSection($"{NameAppSetting.SMTPCONFIG}"));
#endregion

#region logger
ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});
#endregion

#region config authentication
SymmetricSecurityKey symmetricKey = new(Convert.FromBase64String(configuration[ConstAppSettings.APPLICATIONSERECT]));
string? myIssuer = configuration[ConstAppSettings.ENV_SERECT];
string? myAudience = configuration[ConstAppSettings.APPLICATIONAPPDOMAIN];
TokenValidationParameters validationParameters = new()
{
    ValidateIssuerSigningKey = true,
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    IssuerSigningKey = symmetricKey,
    ValidIssuer = myIssuer,
    ValidAudience = myAudience,
    ClockSkew = TimeSpan.Zero
};
builder.Services.AddAuthentication(delegate (AuthenticationOptions x)
{
    x.DefaultAuthenticateScheme = "Bearer";
    x.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(delegate (JwtBearerOptions x)
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = validationParameters;
    x.Events = new JwtBearerEvents()
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Request.Headers.Add("Token-Expired", "true");
            }
            return Task.CompletedTask;
        }
    };
});
ContainerBuilder containerBuilder = new();
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AuthContextModule()));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
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
