using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenPersonalBudget.API.Contracts.Requests;
using OpenPersonalBudget.API.Contracts.Responses;
using OpenPersonalBudget.API.Interfaces;
using OpenPersonalBudget.API.Models;
using System;
using System.Threading.Tasks;

namespace OpenPersonalBudget.API.Controllers.V1
{
    [Route("api/v1/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IAccountBalanceService _accountBalanceService;
        private readonly IAppMsgService _appMsgService;

        public UserController(IUserService userService, IAppMsgService appMsgService, IAccountBalanceService accountBalanceService)
        {
            _userService = userService;
            _appMsgService = appMsgService;
            _accountBalanceService = accountBalanceService;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<CreateUserResponse>> CreateUser(CreateUserRequest userRequest)
        {

            if (await _userService.VerifyIfUserExists(userRequest.Username))
            {
                return BadRequest(new CreateUserResponse()
                {
                    Success = false,
                    Message = string.Format(_appMsgService.GetMessage("UserIsAlreadyTaken"), userRequest.Username)
                });
            }

            var user = await _userService.CreateUser(new UserModel()
            {
                Id = Guid.NewGuid().ToString(),
                Username = userRequest.Username,
                Password = userRequest.Password,
                Email = userRequest.Email,
                CreatedAt = DateTime.Now,
            });

            var account = await _accountBalanceService.CreateAccountBalance(new AccountBalanceModel()
            {
                Id = Guid.NewGuid().ToString(),
                User = user.Id,
                Amount = 0.0f,
                CreatedAt = DateTime.Now,
            });


            if (user == null || account == null)
            {
                return BadRequest(new CreateUserResponse()
                {
                    Success = false,
                    Message = _appMsgService.GetMessage("CannotCreateUser")
                });
            }

            return Ok(new CreateUserResponse()
            {
                Success = true,
                Message = _appMsgService.GetMessage("UserCreated")
            });

        }

    }
}
