using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Server
{
    public interface IUserRepository
    {
        void Register(string name, string connectionId);
        string FindIdByName(string name);
        string FindNameById(string connectionId);
        void RemoveById(string connectionId);
    }
}
