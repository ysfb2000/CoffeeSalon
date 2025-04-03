using System.ComponentModel.DataAnnotations;

namespace UsersApp.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "User Name is required.")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
