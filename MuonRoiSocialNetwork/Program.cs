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
using MuonRoiSocialNetwork.Infrastructure.Repositories;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands;
using MuonRoiSocialNetwork.Infrastructure.Services;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;
using MuonRoiSocialNetwork.Application.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region mediatR
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));
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
});
IMapper mapper = mapperCfg.CreateMapper();
#endregion

#region transient
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserQueries, UserQueries>();
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
