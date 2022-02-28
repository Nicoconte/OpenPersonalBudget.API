using System;

namespace OpenPersonalBudget.API.Contracts.Requests
{
    public class CreateOperationRequest
    {
        public string Description { get; set; }
        public float Amount { get; set; }
        public string OperationType { get; set; }
        public string PaymentType { get; set; }
        public string Category { get; set; }
    }
}
