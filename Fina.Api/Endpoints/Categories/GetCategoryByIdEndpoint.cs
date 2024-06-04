using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Categories
{
    public class GetCategoryByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", HandleAsync)
                 .WithName("Categories:Get by Id")
                 .WithSummary("Recupera uma categoria")
                 .WithDescription("Recupera uma categoria")
                 .WithOrder(4)
                 .Produces<Response<Category?>>();
        }
        private static async Task<IResult> HandleAsync(ICategoryHandler categoryHandler, long id)
        {
            var request = new GetCategoryByIdRequest
            {
                UserId = ApiConfiguration.UserId,
                Id = id
            };

            var response = await categoryHandler.GetByIdAsync(request);

            return response.IsSuccess
                ? TypedResults.Ok(response)
                : TypedResults.BadRequest(response);
        }
    }
}
