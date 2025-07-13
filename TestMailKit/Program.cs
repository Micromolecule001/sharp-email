using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

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

try
{
    // === CREATE MESSAGE ===
    var message = new MimeMessage();
    message.From.Add(MailboxAddress.Parse(fromEmail));
    message.To.Add(MailboxAddress.Parse(toEmail));
    message.Subject = "MailKit SMTP Test";
    message.Body = new TextPart("plain")
    {
        Text = "Hello from MailKit SMTP!"
    };

    // === SMTP CLIENT ===
    using var client = new SmtpClient();
    client.Connect(smtpHost, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
    client.Authenticate(smtpUser, smtpPass);
    client.Send(message);
    client.Disconnect(true);
    Console.WriteLine("✅ MailKit SMTP email sent.");
}
catch (Exception ex)
{
    Console.WriteLine("❌ SMTP Error: " + ex.Message);
}

