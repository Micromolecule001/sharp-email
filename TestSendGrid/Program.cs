using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;

// === LOAD CONFIG ===
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appconfig.json")
    .Build();

// === GET VALUES ===
string sendgridApiKey = config["SendGrid:ApiKey"];
string fromEmail = config["Smtp:From"];
string toEmail = config["Smtp:To"];

// === SETUP CLIENT ===
var client = new SendGridClient(sendgridApiKey);

var from = new EmailAddress(fromEmail, "Sender");
var to = new EmailAddress(toEmail, "Recipient");

var msg = MailHelper.CreateSingleEmail(
    from,
    to,
    "SendGrid SDK Test",
    "Hello plain text.",
    "<p><b>Hello HTML.</b></p>"
);

// === SEND EMAIL ===
var response = await client.SendEmailAsync(msg);
Console.WriteLine($"✅ Status Code: {response.StatusCode}");

