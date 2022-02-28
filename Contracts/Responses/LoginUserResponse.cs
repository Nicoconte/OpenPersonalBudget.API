namespace OpenPersonalBudget.API.Contracts.Responses
{
    public class LoginUserResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }
    }
}
