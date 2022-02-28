using System;
using System.ComponentModel.DataAnnotations;

namespace PersonalBudget.API.Models
{
    public class UserModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
