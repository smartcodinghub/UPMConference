using System.Collections.Concurrent;

namespace Chat.Server
{
    public class UserRepository : IUserRepository
    {
        private static ConcurrentDictionary<string, string> usersIndex = new ConcurrentDictionary<string, string>();

        public string Find(string name) => usersIndex[name];

        public void Register(string name, string connectionId) => usersIndex.TryAdd(name, connectionId);

        public void Remove(string name) => usersIndex.TryRemove(name, out string connectionId);
    }
}
