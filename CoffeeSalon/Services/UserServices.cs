using CoffeeSalon.Data;

namespace CoffeeSalon.Services
{
    public class UserServices : IUsersServices
    {
        private readonly AppDbContext  _context;

        public UserServices(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }



        public bool Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == email);
            
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
    }
}
