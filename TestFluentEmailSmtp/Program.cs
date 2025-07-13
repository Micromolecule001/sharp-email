using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

// === LOAD CONFIG ===
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("../appconfig.json")
    .Build();

// === GET VALUES ===
string smtpHost = config["Smtp:Host"];
int smtpPort = int.Parse(config["Smtp:Port"]);
string smtpUser = config["Smtp:User"];
string smtpPass = config["Smtp:Password"];
string fromEmail = config["Smtp:From"];
string toEmail = config["Smtp:To"];

// === SETUP SMTP SENDER ===
var sender = new SmtpSender(() => new SmtpClient(smtpHost)
{
    Credentials = new NetworkCredential(smtpUser, smtpPass),
    EnableSsl = true,
    Port = smtpPort
});

Email.DefaultSender = sender;

// === SEND EMAIL ===
var response = await Email
    .From(fromEmail)
    .To(toEmail)
    .Subject("FluentEmail SMTP Test")
    .Body("Hello from FluentEmail SMTP!")
    .SendAsync();

Console.WriteLine(response.Successful ? "✅ Email sent." : "❌ Email failed.");

