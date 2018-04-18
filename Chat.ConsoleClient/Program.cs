using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Chat.ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("¡Bienvenido! ¿Cual es tu nombre? ");
            string name = Console.ReadLine();

            Console.Write("¿Con quién quieres hablar? ");
            string other = Console.ReadLine();

            /* Subscription */
            var connection = new HubConnectionBuilder()
                            .WithUrl("http://localhost:8080/chat")
                            .WithHeader("user", name)
                            .Build();

            connection.On<string, DateTime, string>("Received", (userName, time, m) =>
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine($"[{time}] {userName}: {m,-80}");
                Console.WriteLine(new string('-', 100));
                Console.Write("Tú: ");
            });

            await connection.StartAsync();
            await connection.SendAsync("Register", name);

            /* Message Sending */
            string message = null;
            Console.WriteLine(new string('-', 100));
            Console.Write("Tú: ");
            while((message = Console.ReadLine()) != @"\exit")
            {
                Console.SetCursorPosition(0, Console.CursorTop - 2);
                Console.WriteLine($"Tú:    {message,93}");
                await connection.SendAsync("Send", other, message);
                Console.WriteLine(new string('-', 100));
                Console.Write("Tú: ");
            }
        }
    }
}
