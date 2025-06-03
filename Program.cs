// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder();

IConfiguration configuration = builder.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("ApplicationSettings.json ")
    .Build();


int port = int.Parse(configuration["ServerConfig:Port"]);
int delay = int.Parse(configuration["ServerConfig:MessageDelay"]);
Dictionary<string, List<Dictionary<string, int>>> collections = new DataCollections().Collections;

TcpListener tcpListener = new TcpListener(IPAddress.Any, port);
Console.WriteLine($"inititaling server with endpoints{tcpListener.LocalEndpoint}");
// start the server
tcpListener.Start();
Validation validation = new Validation();
while (true)
{

    TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
    _ = Task.Run(async () => await HandleClientAndSendMessage(tcpClient, validation));
}


async Task HandleClientAndSendMessage(TcpClient tcpClient, Validation validation)
{
    try
    {
        NetworkStream networkStream = tcpClient.GetStream();
        var buffer = new byte[2048];

        int received = await networkStream.ReadAsync(buffer, 0, buffer.Length);
        string message = Encoding.UTF8.GetString(buffer, 0, received);

        Console.WriteLine($"Received - {message}");

        await networkStream.WriteAsync(Encoding.Default.GetBytes(message), 0, received);
        int loop = validation.ValidateMessage(message);
        if (loop == -1)
        {
            await networkStream.WriteAsync(Encoding.Default.GetBytes("EMPTY"));
        }
        else
        {
            for (int i = 0; i <= loop; i++)
            {
                await networkStream.WriteAsync(Encoding.Default.GetBytes(DateTime.Now.ToString()));
                await Task.Delay(delay);
            }
        }
    }
    catch (Exception e)
    {
        Console.Write(e);
    }
}
