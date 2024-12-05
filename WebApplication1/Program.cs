using Microsoft.AspNetCore.Components.Web;
using System.Net;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<HtmlRenderer>();

builder.Services.AddScoped((s) => new SmtpClient
{
    Host = "ssl0.ovh.net",
    Port = 587,
    EnableSsl = true,
    UseDefaultCredentials = false,
    Credentials = new NetworkCredential { UserName = "test@khunly.be", Password = "test1234=" }
});

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
