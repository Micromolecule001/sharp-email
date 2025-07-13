using FluentEmail.Core;
using FluentEmail.SendGrid;
using Microsoft.Extensions.Configuration;

// === LOAD CONFIG ===
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("../appconfig.json")
    .Build();

// === GET VALUES ===
string sendgridApiKey = config["SendGrid:ApiKey"];
string fromEmail = config["Smtp:From"];
string toEmail = config["Smtp:To"];

// === SETUP SENDGRID SENDER ===
var sender = new SendGridSender(sendgridApiKey);
Email.DefaultSender = sender;

// === SEND EMAIL ===
var response = await Email
    .From(fromEmail)
    .To(toEmail)
    .Subject("FluentEmail SendGrid Test")
    .Body("Hello from FluentEmail using SendGrid!")
    .SendAsync();

Console.WriteLine(response.Successful ? "✅ Email sent." : "❌ Email failed.");

