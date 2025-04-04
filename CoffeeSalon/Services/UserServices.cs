using CoffeeSalon.Data;
using CoffeeSalon.Models;

namespace CoffeeSalon.Services
{
    public class UserServices : IUsersServices
    {
        private readonly AppDbContext _context;

        public UserServices(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public Result<List<User>> GetUserList() 
        {
            var list = _context.Users.ToList();
            var result = new Result<List<User>>();

            result.Value = list;

            return result;
        }

        public Result SetAsAdmin(string userId)
        {
            var id = int.Parse(userId);

            var result = new Result();
            // Find the user by ID
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                result.IsSuccess = false;
                return result; // User not found
            }
            // Set the user's role to "admin"
            user.Role = "admin";
            // Save changes to the database
            _context.SaveChanges();
            result.IsSuccess = true;
            return result;
        }

        public Result SetAsUser(string userId)
        {
            var id = int.Parse(userId);
            var result = new Result();
            // Find the user by ID
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                result.IsSuccess = false;
                return result; // User not found
            }
            // Set the user's role to "user"
            user.Role = "user";
            // Save changes to the database
            _context.SaveChanges();
            result.IsSuccess = true;
            return result;
        }


        public Result DeleteUser(string userId)
        {
            var id = int.Parse(userId);
            var result = new Result();
            // Find the user by ID
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                result.IsSuccess = false;
                return result; // User not found
            }
            // Remove the user from the database
            _context.Users.Remove(user);
            _context.SaveChanges();
            result.IsSuccess = true;
            return result;
        }


        public Result<User> Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            var result = new Result<User>();

            if (user == null)
            {
                result.IsSuccess = false;
                return result;
            }

            // Check if the password encrypted with md5 is correct
            var passwordHash = Utilities.GetMd5Hash(password);

            if (user.PasswordHash != passwordHash)
            {
                result.IsSuccess = false;
                return result;
            }

            result.Value = user;

            return result;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Result Register(string username, string password)
        {
            var result = new Result();

            // Check if the user already exists
            var existingUser = _context.Users.FirstOrDefault(u => u.Username == username);
            if (existingUser != null)
            {
                result.IsSuccess = true;
                return result; // User already exists
            }
            // Create a new user
            var newUser = new Models.User()
            {
                Username = username,
                PasswordHash = Utilities.GetMd5Hash(password),
                // Set default role to "user"
                Role = "user"
            };

            // Add the new user to the database
            _context.Users.Add(newUser);

            _context.SaveChanges();
            return result;
        }

    }
}
