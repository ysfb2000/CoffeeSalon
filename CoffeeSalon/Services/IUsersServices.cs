using CoffeeSalon.Models;

namespace CoffeeSalon.Services
{
    public interface IUsersServices
    {
        public Result Login(string username, string password);

        public Result Register(string username, string password);
    }
}
