using MimeKit;
using Microsoft.Extensions.Configuration;

// === LOAD CONFIG ===
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("../appconfig.json")
    .Build();

// === GET VALUES ===
string fromEmail = config["Smtp:From"];
string toEmail = config["Smtp:To"];

// === CREATE MIME MESSAGE ===
var message = new MimeMessage();
message.From.Add(MailboxAddress.Parse(fromEmail));
message.To.Add(MailboxAddress.Parse(toEmail));
message.Subject = "MimeKit MIME Example";

var builder = new BodyBuilder
{
    TextBody = "This is the plain text body.",
    HtmlBody = "<p><b>This is the HTML body.</b></p>"
};

// === ADD ATTACHMENT ===
builder.Attachments.Add("Program.cs");

message.Body = builder.ToMessageBody();

// === OUTPUT TO CONSOLE ===
Console.WriteLine("✅ MIME Message created:");
Console.WriteLine(message.ToString());

