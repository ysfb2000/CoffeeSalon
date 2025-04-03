using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeSalon.Models
{
    public class User
    {

        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Username { get; set; }

        [Required]
        public required string PasswordHash { get; set; }

        [Required]
        [MaxLength(20)]
        public required string Role { get; set; }  // "user" or "admin"
    }

}
