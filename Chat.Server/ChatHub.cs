using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Server
{
    /// <summary>
    /// Hub para el tiempo real
    /// </summary>
    [Authorize]
    public class ChatHub : Hub<IChatClient>
    {
        /// <summary>
        /// Ruta donde este hub escucha
        /// </summary>
        public const String Path = "/chat";

        private IUserRepository repository;

        public ChatHub(IUserRepository repository)
        {
            this.repository = repository;
        }

        public Task Send(string to, String message)
        {
            var senderName = Context.User.Identity.Name;

            return Clients.Client(repository.Find(to))
                .Received(senderName, DateTime.Now, message);
        }

        public override async Task OnConnectedAsync()
        {
            var senderName = Context.User.Identity.Name;
            repository.Register(senderName, Context.ConnectionId);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            repository.Remove(Context.ConnectionId);
            var context = Context.GetHttpContext();

            await context.SignOutAsync();
            await base.OnDisconnectedAsync(exception);
        }
    }

    /// <summary>
    /// Interfaz para tipar los clientes de un hub
    /// </summary>
    public interface IChatClient
    {
        /// <summary>
        /// Mensaje "Event"
        /// </summary>
        /// <param name="time"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        Task Received(string userName, DateTime time, String message);
    }
}
