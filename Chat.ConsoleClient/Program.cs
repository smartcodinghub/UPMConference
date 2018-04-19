using System;
using System.Net;
using System.Net.Http;
using System.Text;
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
                            .WithCookie(LogIn(name, "none"))
                            .Build();

            connection.On<string, DateTime, string>("Received", (from, time, m) =>
            {
                WriteReceivedMessage(from, time, m);
            });

            await connection.StartAsync();

            /* Message Sending */
            string message = null;
            WriteMessageBlock();
            while((message = Console.ReadLine()) != @":q")
            {
                WriteSentMessage(message);
                await connection.SendAsync("Send", other, message);
                WriteMessageBlock();
            }
        }

        private static void WriteReceivedMessage(string userName, DateTime time, string m)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine($"[{time}] {userName}: {m,-80}");
            WriteMessageBlock();
        }

        private static void WriteSentMessage(string message)
        {
            Console.SetCursorPosition(0, Console.CursorTop - 2);
            Console.WriteLine($"Tú:    {message,93}");
        }

        private static void WriteMessageBlock()
        {
            Console.WriteLine(new string('-', 100));
            Console.Write("Tú: ");
        }

        private static Cookie LogIn(string user, string password)
        {
            var uri = new Uri("http://localhost:8080");
            var cookieContainer = new CookieContainer();
            using(var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using(var client = new HttpClient(handler) { BaseAddress = uri })
            {
                cookieContainer.Add(uri, new Cookie("CookieName", "cookie_value"));
                StringContent content = new StringContent($@"{{ ""user"" : ""{user}"", ""password"" : ""{password}"" }}", Encoding.UTF8, "application/json");
                var result = client.PostAsync("/Account/Login", content).Result;
                result.EnsureSuccessStatusCode();
            }

            return cookieContainer.GetCookies(uri)[".AspNetCore.Cookies"];
        }
    }
}
