using System.ComponentModel.DataAnnotations;

using System;
using EasyCrudNET.Mappers.DataAnnotation;

namespace OpenPersonalBudget.API.Models
{
    public class AccountBalanceModel
    {
        public string Id { get; set; }     
        
        [ColumnMapper("UserId")]
        public string User { get; set; }
        
        public float Amount { get; set; } = 0.0f;
        public DateTime CreatedAt { get; set; }
    }
}
