using CoffeeSalon.Data;

namespace CoffeeSalon.Services
{
    public class UserServices : IUsersServices
    {
        private readonly AppDbContext _context;

        public UserServices(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }



        public bool Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return false;
            }

            // Check if the password encrypted with md5 is correct
            var passwordHash = Utilities.GetMd5Hash(password);

            if (user.PasswordHash != passwordHash)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Register(string username, string password)
        {
            // Check if the user already exists
            var existingUser = _context.Users.FirstOrDefault(u => u.Username == username);
            if (existingUser != null)
            {
                return false; // User already exists
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
            return true;
        }

    }
}
