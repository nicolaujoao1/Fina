using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Transactions
{
    public class DeleteTransactionEndpoint:IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
     => app.MapDelete("/{id}", HandleAsync)
           .WithName("Transactions:Delete")
           .WithSummary("Exclui uma Transação")
           .WithDescription("Exclui uma Transação")
           .WithOrder(2)
           .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandleAsync(ITransactionHandler transactionHandler, long id)
        {
            var request = new DeleteTransactionRequest
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var response = await transactionHandler.DeleteAsync(request);

            return response.IsSuccess
                ? TypedResults.Created($"v1/transaction/{response.Data?.Id}", response)
                : TypedResults.BadRequest(response);
        }
    }
}
