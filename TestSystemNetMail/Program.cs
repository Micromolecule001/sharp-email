using System.Net;
using System.Net.Mail;
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
    var message = new MailMessage(fromEmail, toEmail)
    {
        Subject = "System.Net.Mail Test",
        Body = "Hello from System.Net.Mail!"
    };

    // === SMTP CLIENT ===
    var client = new SmtpClient(smtpHost, smtpPort)
    {
        Credentials = new NetworkCredential(smtpUser, smtpPass),
        EnableSsl = true
    };

    // === SEND ===
    client.Send(message);
    Console.WriteLine("✅ Email sent successfully.");
}
catch (Exception ex)
{
    Console.WriteLine("❌ Error sending email: " + ex.Message);
}

