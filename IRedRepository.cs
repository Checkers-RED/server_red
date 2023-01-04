using System.Collections.Generic;

namespace server_red
{
    public interface IRedRepository
    {
        // Авторизация
        List<string> SignIn(string username, string password);
    }
}
