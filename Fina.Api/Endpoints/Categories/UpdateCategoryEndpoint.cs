using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Categories
{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
         => app.MapPost("/{id}", HandleAsync)
               .WithName("Categories:Update")
               .WithSummary("Actualizar categoria")
               .WithDescription("Actualizar categoria")
               .WithOrder(3)
               .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(ICategoryHandler categoryHandler, UpdateCategoryRequest request)
        {
            request.UserId = ApiConfiguration.UserId;

            var response = await categoryHandler.UpdateAsync(request);

            return response.IsSuccess
                ? TypedResults.Ok(response)
                : TypedResults.BadRequest(response);
        }
    }
}
