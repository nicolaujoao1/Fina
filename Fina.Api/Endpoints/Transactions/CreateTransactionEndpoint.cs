using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Transactions
{
    public class CreateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
              .WithName("Transation:Create")
              .WithSummary("Criar uma nova Transação")
              .WithDescription("Criar uma nova Transação")
              .WithOrder(1)
              .Produces<Response<Transaction?>>();

        private static async Task<IResult> HandleAsync(ITransactionHandler transactionHandler, CreateTransactionRequest request)
        {
            request.UserId = ApiConfiguration.UserId;

            var response = await transactionHandler.CreateAsync(request);

            return response.IsSuccess
                ? TypedResults.Created($"v1/transaction/{response.Data?.Id}", response)
                : TypedResults.BadRequest(response);
        }
    }
}
