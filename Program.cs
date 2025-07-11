﻿// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder();

IConfiguration configuration = builder.SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json ")
    .Build();

// initialize variable from config
int port = int.Parse(configuration["ServerConfig:Port"]);
int messageDelay = int.Parse(configuration["ServerConfig:MessageDelay"]);
int bufferSize = int.Parse(configuration["ServerConfig:BufferSize"]);
string codeNotPresent = configuration["ServerConfig:CodeNotPresentMessage"];
string AES_Key = configuration["ServerConfig:AES_Key"];
string AES_IV = configuration["ServerConfig:AES_IV"];

Dictionary<string, List<Dictionary<string, int>>> collections = new DataCollections().Collections;

TcpListener tcpListener = new TcpListener(IPAddress.Any, port);
Console.WriteLine($"Inititaling server with endpoints - {tcpListener.LocalEndpoint}");


Validation validation = new();
EncryptDecrypt en = new EncryptDecrypt(AES_Key ,AES_IV);
// start the server
tcpListener.Start();

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
        var buffer = new byte[bufferSize];
        int received = await networkStream.ReadAsync(buffer, 0, buffer.Length);
        // remove padding
        byte[] actualData = buffer[..received];

        string message = en.Decrypt(actualData);

        // await networkStream.WriteAsync(Encoding.Default.GetBytes(message), 0, received);
        int loop = validation.ValidateMessage(message);
        if (loop == -1)
        {
            byte[] sendMessage = en.Encrypt($"{codeNotPresent}");
            await networkStream.WriteAsync(sendMessage);
        }
        else
        {
            for (int i = 0; i < loop; i++)
            {
                byte[] sendMessage = en.Encrypt(DateTime.Now.ToString("dd-MM-yy HH:mm:ss"));

                await networkStream.WriteAsync(sendMessage);
                await Task.Delay(messageDelay);
            }
        }
        tcpClient.Close();
    }
    
    catch (System.IO.IOException IOException)
    {
        Console.Write($"Unable to read data {IOException}");
    }
    catch (Exception e)
    {
        Console.Write(e);
    }
}
