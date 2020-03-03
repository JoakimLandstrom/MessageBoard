using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class User : ModelBase
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}