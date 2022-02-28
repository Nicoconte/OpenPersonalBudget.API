using System;
using System.ComponentModel.DataAnnotations;

namespace OpenPersonalBudget.API.Models
{
    public class OperationModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public virtual UserModel User { get; set; }
        public string Description { get; set; }
        public float Amount { get; set; }
        public Nullable<OperationTypeEnum> OperationType { get; set; }
        public Nullable<OperationPaymentTypeEnum> PaymentType { get; set; }
        public Nullable<OperationCategoryEnum> Category { get; set; }
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
