using System;

namespace Bot
{
    class Program
    {
        static void Main()
        {
            var socket = new SocketAPI.SocketAPI();
            socket.ReloadData += (accounts) =>
            {
                foreach (var item in accounts)
                {
                    Console.WriteLine($"ID: {item.Id} , SECRET: {item.APISecret} , KEY {item.APIKey}");
                }
            };
            socket.StartListening();
        }
    }
}
