using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalBudget.API.Interfaces;
using PersonalBudget.API.Contracts.Requests;
using PersonalBudget.API.Contracts.Responses;
using System.Threading.Tasks;

namespace PersonalBudget.API.Controllers.V1
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IAuthenticationService _authenticationService;
        private readonly IAppMsgService _appMsgService;

        public AuthenticationController(IAuthenticationService authenticationService, IAppMsgService appMsgService)
        {
            _authenticationService = authenticationService;
            _appMsgService = appMsgService; 
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<LoginUserResponse>> Login(LoginUserRequest userRequest)
        {
            var token = await _authenticationService.AuthenticateUser(userRequest.Username, userRequest.Password);

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new LoginUserResponse()
                {
                    Success = false,
                    Message = _appMsgService.GetMessage("InvalidCredential"),
                    AccessToken = null
                });
            }

            return Ok(new LoginUserResponse()
            {
                Success = true,
                Message = string.Format(_appMsgService.GetMessage("UserLoggedIn"), userRequest.Username),
                AccessToken = token
            });
        }
    }
}
