namespace OpenPersonalBudget.API.Contracts.Requests
{
    public class UpdateOperationRequest
    {
        public string Description { get; set; }
        public float Amount { get; set; }
        public string OperationType { get; set; }
        public string PaymentType { get; set; }
        public string Category { get; set; }
    }
}
