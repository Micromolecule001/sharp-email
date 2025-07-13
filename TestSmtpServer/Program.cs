using System.Buffers;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SmtpServer;
using SmtpServer.Protocol;
using SmtpServer.Storage;

// === SETUP SERVICES ===
var services = new ServiceCollection()
    .AddSingleton<IMessageStore, SampleMessageStore>()
    .BuildServiceProvider();

// === CONFIGURE SMTP SERVER ===
var options = new SmtpServerOptionsBuilder()
    .ServerName("localhost")
    .Port(2525, false) // unencrypted SMTP
    .Build();

// === START SERVER ===
var server = new SmtpServer.SmtpServer(options, services);

Console.WriteLine("✅ SMTP Server running on port 2525...");
await server.StartAsync(CancellationToken.None);

// === CUSTOM STORE ===
public class SampleMessageStore : MessageStore
{
    public override Task<SmtpResponse> SaveAsync(
        ISessionContext context,
        IMessageTransaction transaction,
        ReadOnlySequence<byte> buffer,
        CancellationToken cancellationToken)
    {
        Console.WriteLine("📨 Received Email:");
        string message = Encoding.UTF8.GetString(buffer.ToArray());
        Console.WriteLine(message);
        return Task.FromResult(SmtpResponse.Ok);
    }
}

