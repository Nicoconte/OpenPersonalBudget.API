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
using System.Collections.Generic;
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

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GetSingleOperationResponse>> GetSingleUserOperation([FromRoute] string id)
        {
            if (id == null)
                return BadRequest(new GetSingleOperationResponse()
                {
                    Success = false,
                    Message = _appMsgService.GetMessage("InvalidId"),
                    Data = null
                });

            if (!(await _operationService.Exists(id)))
                return BadRequest(new GetSingleOperationResponse()
                {
                    Success = false,
                    Message = _appMsgService.GetMessage("OperationNotFound")
                });


            var op = await _operationService.GetOperation(id);

            return Ok(new GetSingleOperationResponse()
            {
                Success = true,
                Message = string.Empty,
                Data    = new
                {
                    Id = op.Id,
                    Description = op.Description,
                    Amount = op.Amount,
                    CreatedAt = op.CreatedAt,
                    OperationType = EnumHelper.GetStringFromEnum<OperationTypeEnum>((int)op.OperationType),
                    PaymentType = EnumHelper.GetStringFromEnum<OperationPaymentTypeEnum>((int)op.PaymentType),
                    Category = EnumHelper.GetStringFromEnum<OperationCategoryEnum>((int)op.Category)
                }
            });
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<GetAllOperationsResponse>> ListUserOperations()
        {

            var user = await _userService.GetUserFromClaims(User);

            var ops = await _operationService.GetAllUserOperations(user);
            
            var mappedOps = new List<object>(); 

            ops.ForEach(op =>
            {
                mappedOps.Add(new
                {
                    Id = op.Id,
                    Description = op.Description,
                    Amount = op.Amount,
                    CreatedAt = op.CreatedAt,
                    OperationType = EnumHelper.GetStringFromEnum<OperationTypeEnum>((int)op.OperationType),
                    PaymentType = EnumHelper.GetStringFromEnum<OperationPaymentTypeEnum>((int)op.PaymentType),
                    Category = EnumHelper.GetStringFromEnum<OperationCategoryEnum>((int)op.Category)
                });
            });

            return Ok(new GetAllOperationsResponse()
            {
                Success = true, 
                Message = string.Empty,
                Data = mappedOps
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<DeleteOperationResponse>> DeleteUserOperation([FromRoute] string id)
        {
            if (id == null)
                return BadRequest(new GetSingleOperationResponse()
                {
                    Success = false,
                    Message = _appMsgService.GetMessage("InvalidId"),
                });

            if (!(await _operationService.Exists(id)))
                return BadRequest(new GetSingleOperationResponse()
                {
                    Success = false,
                    Message = _appMsgService.GetMessage("OperationNotFound")
                });

            var deleted = await _operationService.DeleteOperation(id);

            return Ok(new DeleteOperationResponse()
            {
                Success = deleted,
                Message = deleted ? _appMsgService.GetMessage("OpDeletedSuccessfully") : _appMsgService.GetMessage("CannotDeleteOp")
            });
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<UpdateOperationResponse>> UpdateUserOperation([FromRoute] string id, UpdateOperationRequest opRequest)
        {

            if (id == null)
                return BadRequest(new GetSingleOperationResponse()
                {
                    Success = false,
                    Message = _appMsgService.GetMessage("InvalidId"),
                });

            if (!(await _operationService.Exists(id)))
                return BadRequest(new GetSingleOperationResponse()
                {
                    Success = false,
                    Message = _appMsgService.GetMessage("OperationNotFound")
                });

            var op = await _operationService.UpdateOperation(new OperationModel()
            {
                Id = id,
                Description = opRequest.Description,
                Amount = opRequest.Amount,
                OperationType = EnumHelper.GetEnumFromString<OperationTypeEnum>(opRequest.OperationType),
                Category = EnumHelper.GetEnumFromString<OperationCategoryEnum>(opRequest.Category),
                PaymentType = EnumHelper.GetEnumFromString<OperationPaymentTypeEnum>(opRequest.PaymentType)
            });

            return Ok(new UpdateOperationResponse()
            {
                Success = true,
                Message = string.Empty,
                Data = new
                {
                    Id = op.Id,
                    Description = op.Description,
                    Amount = op.Amount,
                    CreatedAt = op.CreatedAt,
                    OperationType = EnumHelper.GetStringFromEnum<OperationTypeEnum>((int)op.OperationType),
                    PaymentType = EnumHelper.GetStringFromEnum<OperationPaymentTypeEnum>((int)op.PaymentType),
                    Category = EnumHelper.GetStringFromEnum<OperationCategoryEnum>((int)op.Category)
                }
            });
        }
    }
}
