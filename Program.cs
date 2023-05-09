using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ContinentalBackend.Data;
using ContinentalBackend.Authentication;
using FirebaseAdmin;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using ContinentalBackend.Broker;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ContinentalBackendContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ContinentalBackendContext") ?? throw new InvalidOperationException("Connection string 'ContinentalBackendContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton(FirebaseApp.Create());

builder.Services.AddScoped<AlertBroker>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddScheme<AuthenticationSchemeOptions, FirebaseAuthenticationHandler>(JwtBearerDefaults.AuthenticationScheme, (o) => {});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
