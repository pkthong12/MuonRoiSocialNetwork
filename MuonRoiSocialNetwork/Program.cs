using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using MuonRoiSocialNetwork.Domains.Interfaces;
using MuonRoiSocialNetwork.Infrastructure;
using MuonRoiSocialNetwork.Infrastructure.Map.Users;
using MuonRoiSocialNetwork.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

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
    var filePath = Path.Combine(AppContext.BaseDirectory, "MuonRoiAPI.xml");
    x.IncludeXmlComments(filePath);
});
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddDbContext<MuonRoiSocialNetworkDbContext>(opt =>
{
    opt.UseSqlServer("Server=.;Database=MuonRoi;Trusted_Connection=True;");
});
var mapperCfg = new MapperConfiguration(x =>
{
    x.AddProfile(new UserProfile());
});
IMapper mapper = mapperCfg.CreateMapper();
builder.Services.AddSingleton(mapper);
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
