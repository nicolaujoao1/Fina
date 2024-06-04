using Fina.Api.Common.Api;
using Fina.Core;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Endpoints.Transactions
{
    public class GetAllTransactionsEndpoint:IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", HandleAsync)
                 .WithName("Transaction:Get Transaction")
                 .WithSummary("Recupera todas as Transações")
                 .WithDescription("Recupera todas as Transações")
                 .WithOrder(5)
                 .Produces<PagedResponse<List<Transaction?>>>();
        }
        private static async Task<IResult> HandleAsync(ITransactionHandler transactionHandler,
                                                       [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
                                                       [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetAllTransactionRequest
            {
                UserId = ApiConfiguration.UserId,
                PageNumber = pageNumber,
                PageSize = pageSize
            };


            var response = await transactionHandler.GetAllAsync(request);

            return response.IsSuccess
                ? TypedResults.Ok(response)
                : TypedResults.BadRequest(response);
        }
    }
}
