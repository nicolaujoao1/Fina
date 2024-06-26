﻿using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Transactions
{
    public class GetTransactionByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", HandleAsync)
                 .WithName("Transaction:Get by Id")
                 .WithSummary("Recupera uma Transaction")
                 .WithDescription("Recupera uma Transaction")
                 .WithOrder(4)
                 .Produces<Response<Transaction?>>();
        }
        private static async Task<IResult> HandleAsync(ITransactionHandler transactionHandler, long id)
        {
            var request = new GetTransactionByIdRequest
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var response = await transactionHandler.GetByIdAsync(request);

            return response.IsSuccess
                ? TypedResults.Ok(response)
                : TypedResults.BadRequest(response);
        }
    }
}
