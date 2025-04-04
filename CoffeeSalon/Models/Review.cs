using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeSalon.Models
{


    public class Review
    {
        public Review()
        {
        }

        [Key]
        public int ReviewId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User? User { get; set; }

        [Required]
        [MaxLength(200)]
        public required string ItemName { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(2000)]
        public required string ReviewText { get; set; }

        public DateTime DatePosted { get; set; } = DateTime.Now;

        public byte[]? Image { get; set; }  // Stores a single image
    }

}
