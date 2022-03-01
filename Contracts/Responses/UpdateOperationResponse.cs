namespace OpenPersonalBudget.API.Contracts.Responses
{
    public class UpdateOperationResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
