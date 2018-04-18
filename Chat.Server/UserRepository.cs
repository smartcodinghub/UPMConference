using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Server
{
    public class UserRepository : IUserRepository
    {
        private static ConcurrentDictionary<string, string> usersIndex = new ConcurrentDictionary<string, string>();
        private static ConcurrentDictionary<string, string> connectionIndex = new ConcurrentDictionary<string, string>();

        public string FindNameById(string connectionId)
        {
            return connectionIndex[connectionId];
        }

        public string FindIdByName(string name)
        {
            return usersIndex[name];
        }

        public void Register(string name, string connectionId)
        {
            if(usersIndex.TryAdd(name, connectionId))
            {
                if(!connectionIndex.TryAdd(connectionId, name))
                {
                    usersIndex.TryRemove(name, out connectionId);
                }
            }
        }

        public void RemoveById(string connectionId)
        {
            if(connectionIndex.TryRemove(connectionId, out string name))
            {
                usersIndex.TryRemove(name, out connectionId);
            }
        }
    }
}
