using System;
using System.ComponentModel.DataAnnotations;

namespace OpenPersonalBudget.API.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
