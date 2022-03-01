using System.Collections.Generic;

namespace OpenPersonalBudget.API.Contracts.Responses
{
    public class GetAllOperationsResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<object> Data { get; set; } = new List<object>();
    }
}
