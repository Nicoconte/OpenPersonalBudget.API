using EasyCrudNET.Mappers.DataAnnotation;
using System;
using System.ComponentModel.DataAnnotations;

namespace OpenPersonalBudget.API.Models
{
    public class OperationModel
    {

        public string Id { get; set; }
        
        [ColumnMapper("UserId")]
        public virtual string User { get; set; }
        
        public string Description { get; set; }
        public float Amount { get; set; }
        public OperationTypeEnum OperationType { get; set; }
        public OperationPaymentTypeEnum PaymentType { get; set; }
        public OperationCategoryEnum Category { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public enum OperationTypeEnum
    {
        CashIncome,
        CashOutflow,
    }

    public enum OperationCategoryEnum
    {
        Health,
        Bills,
        Car,
        Clothes,
        Communication,
        Food,
        Gift,
        Sport,
        Transport,
        EatingOut,
        Entertaiment,
        Shoping,
        ECommerce,
        Other
    }

    public enum OperationPaymentTypeEnum
    {
        CreditCard,
        DebitCard,
        Cash,
        Checks,
        WireTransfer,
        MobilePhone,
        Other
    }

}
