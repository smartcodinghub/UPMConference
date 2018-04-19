namespace Chat.Server
{
    public interface IUserRepository
    {
        void Register(string name, string connectionId);
        string Find(string name);
        void Remove(string connectionId);
    }
}
