using System.ComponentModel.DataAnnotations;

using System;

namespace OpenPersonalBudget.API.Models
{
    public class AccountBalanceModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public virtual UserModel User { get; set; }
        public float Amount { get; set; } = 0.0f;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
