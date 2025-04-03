using CoffeeSalon.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeSalon.Services
{
    public interface IUsersServices
    {
        public Result<List<User>> GetUserList();

        public Result Login(string username, string password);

        public Result Register(string username, string password);

        public Result SetAsAdmin(string userId);

        public Result SetAsUser(string userId);

        public Result DeleteUser(string userId);
    }
}
