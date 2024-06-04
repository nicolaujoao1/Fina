using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Transactions
{
    public class UpdateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
          => app.MapPost("/{id}", HandleAsync)
                .WithName("Transaction:Update")
                .WithSummary("Actualizar Transaction")
                .WithDescription("Actualizar Transaction")
                .WithOrder(3)
                .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandleAsync(ITransactionHandler transactionHandler, UpdateTransactionRequest request)
        {
            request.UserId = ApiConfiguration.UserId;

            var response = await transactionHandler.UpdateAsync(request);

            return response.IsSuccess
                ? TypedResults.Ok(response)
                : TypedResults.BadRequest(response);
        }
    }
}
