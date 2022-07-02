using EasyCrudNET;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenPersonalBudget.API.Data.Repositories.Interfaces;
using OpenPersonalBudget.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenPersonalBudget.API.Data.Repositories.Implementations
{
    public class OperationRepository : IOperationRepository
    {


        private readonly EasyCrud _easyCrud;
        private readonly IConfiguration _configuration;

        public OperationRepository(EasyCrud easyCrud, IConfiguration configuration)
        {
            _easyCrud = easyCrud;
            _configuration = configuration;

            _easyCrud.SetSqlConnection(_configuration.GetSection("TestConnectionString").Value);
        }

        public async Task<bool> Insert(OperationModel operation)
        {
            var saved = await _easyCrud
                .Insert()
                .Into("Operations")
                .Values("@id", "@userId", "@des", "@amount", "@optype", "@paytype", "@cat", "@date")
                .ExecuteAsync(new
                {
                    id = operation.Id,
                    userId = operation.User,
                    des = operation.Description,
                    amount = operation.Amount,
                    optype = (int)operation.OperationType,
                    paytype = (int)operation.PaymentType,
                    cat = (int)operation.Category,
                    date = DateTime.Now,
                });

            return saved != 0;
        }

        public async Task<bool> Delete(object id)
        {
            var deleted = await _easyCrud
                .Delete()
                .From("Operations")
                .Where("Id", "@id").ExecuteAsync(new
                {
                    id = id
                });

            return deleted != 0;
        }

        public async Task<OperationModel> GetById(object id)
        {
            var operation = await _easyCrud
                .Select("*")
                .From("Operations")
                .Where("Id", "@id")
                .ExecuteAsync<OperationModel>(new
                {
                    id = id
                });

            return operation.FirstOrDefault();
        }

        public async Task<OperationModel> Update(OperationModel operation)
        {
            var updated = _easyCrud
                .Update("Operations")
                .Set("Description", "@des")
                .Set("Amount", "@amount")
                .Set("OperationType", "@opType")
                .Set("PaymentType", "@payType")
                .Set("Category", "@cat")
                .Where("Id", "@id")
                .DebugQuery()
                .Execute(new
                {
                    des = operation.Description,
                    amount = operation.Amount,
                    opType = (int)operation.OperationType,
                    payType = (int)operation.PaymentType,
                    cat = (int)operation.Category,
                    id = operation.Id
                });


            OperationModel currentOperation = null;

            if (updated != 0)
            {
                currentOperation = (await _easyCrud
                    .Select("*")
                    .From("Operations")
                    .Where("Id", "@id")
                    .ExecuteAsync<OperationModel>(new
                    {
                        id = operation.Id
                    }))
                    .FirstOrDefault();
            }

            return currentOperation;
        }

        public async Task<IEnumerable<OperationModel>> GetAll()
        {
            return await _easyCrud.Select("*").From("Operations").ExecuteAsync<OperationModel>();
        }

    }
}
