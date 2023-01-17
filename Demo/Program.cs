// See https://aka.ms/new-console-template for more information

using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;
using Websocket.Client;

string streaming_API_Key = "YOUR_USER_KEY";
var url = new Uri("http://localhost:51373/api/Echo/");
var exitEvent = new ManualResetEvent(false);

using (var client = new WebsocketClient(url))
{

    
    client.ReconnectTimeout = TimeSpan.FromSeconds(30);
    client.ReconnectionHappened.Subscribe(info =>
    {
        Console.WriteLine("recoonection happend, type: " + info.Type);
    });

    client.MessageReceived.Subscribe(msg =>
    {
        Console.Write(msg);
        if (msg.ToString().ToLower() == "connected")
        {
            string data = "{\"userKey\":\"" + streaming_API_Key + "\", \"symbol\":\"EURUSD,GBPUSD,USDJPY\"}";
            client.Send(data);
        }
    });

    Task.Run(() => client.Start());
    exitEvent.WaitOne();
}

Console.WriteLine("Hello, World!");
Console.ReadKey();
