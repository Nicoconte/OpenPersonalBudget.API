using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenPersonalBudget.API.Contracts.Requests;
using OpenPersonalBudget.API.Contracts.Responses;
using OpenPersonalBudget.API.Helpers;
using OpenPersonalBudget.API.Interfaces;
using OpenPersonalBudget.API.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OpenPersonalBudget.API.Controllers.V1
{
    [Route("api/v1/operations")]
    [ApiController]
    [Authorize]
    public class OperationController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IAccountBalanceService _accountBalanceService;
        private readonly IOperationService _operationService;
        private readonly IAppMsgService _appMsgService;

        public OperationController(
            IUserService userService, 
            IOperationService operationService, 
            IAppMsgService appMsgService,
            IAccountBalanceService accountBalanceService
        )
        {
            _userService = userService;
            _operationService = operationService;
            _appMsgService = appMsgService;
            _accountBalanceService = accountBalanceService;
        }


        [HttpPost]
        [Route("")]
        public async Task<ActionResult<CreateOperationResponse>> CreateOperation(CreateOperationRequest operationRequest)
        {

            var user = await _userService.GetUserFromClaims(User);

            if (user == null)
                return BadRequest(new CreateOperationResponse()
                {
                    Success = false,
                    Message = _appMsgService.GetMessage("InvalidUser")
                });

            var operation = new OperationModel()
            {
                User = user,
                Description = operationRequest.Description,
                Amount = operationRequest.Amount,
                OperationType = EnumHelper.GetEnumFromString<OperationTypeEnum>(operationRequest.OperationType),
                Category = EnumHelper.GetEnumFromString<OperationCategoryEnum>(operationRequest.Category),
                PaymentType = EnumHelper.GetEnumFromString<OperationPaymentTypeEnum>(operationRequest.PaymentType)
            };    

            var newOp = await _operationService.CreateOperation(operation);

            if (newOp == null)
                return BadRequest(new CreateOperationResponse()
                {
                    Success = false,
                    Message = _appMsgService.GetMessage("CannotCreateOp")
                });

            await _accountBalanceService.UpdateAmountFromUserAccount(user, newOp);

            return Ok(new CreateOperationResponse()
            {
                Success = true,
                Message = _appMsgService.GetMessage("OpCreatedSuccessfully")
            });
        }

    }
}
