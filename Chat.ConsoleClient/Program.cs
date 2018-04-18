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
                            .WithConsoleLogger()
                            .Build();

            connection.On<string, DateTime, string>("Received", (userName, time, m) =>
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.WriteLine($"{userName}: {m}    | {time}");
                Console.Write("Tú:            ");
            });

            await connection.StartAsync();
            await connection.SendAsync("Register", name);

            /*  */
            string message = null;
            Console.Write("Tú:                                    ");
            while((message = Console.ReadLine()) != @"\exit")
            {
                await connection.SendAsync("Send", other, message);
                Console.Write("Tú:                                    ");
            }
        }
    }
}
