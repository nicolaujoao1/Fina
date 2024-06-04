using Fina.Api.Common.Api;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Api.Endpoints.Categories
{
    public class DeleteCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
       => app.MapDelete("/{id}", HandleAsync)
             .WithName("Categories:Delete")
             .WithSummary("Exclui uma categoria")
             .WithDescription("Exclui uma categoria")
             .WithOrder(2)
             .Produces<Response<Category?>>();

        private static async Task<IResult> HandleAsync(ICategoryHandler categoryHandler, long id)
        {
            var request=new DeleteCategoryRequest 
            {
                UserId=ApiConfiguration.UserId,
                Id = id
            };   

            var response = await categoryHandler.DeleteAsync(request);

            return response.IsSuccess
                ? TypedResults.Created($"v1/categories/{response.Data?.Id}", response)
                : TypedResults.BadRequest(response);
        }
    }
}
