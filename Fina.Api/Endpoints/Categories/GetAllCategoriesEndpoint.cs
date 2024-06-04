using Fina.Api.Common.Api;
using Fina.Core;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Endpoints.Categories
{
    public class GetAllCategoriesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", HandleAsync)
                 .WithName("Categories:Get All")
                 .WithSummary("Recupera todas as categorias")
                 .WithDescription("Recupera todas as categorias")
                 .WithOrder(5)
                 .Produces<PagedResponse<List<Category?>>>();
        }
        private static async Task<IResult> HandleAsync(ICategoryHandler categoryHandler, 
                                                       [FromQuery] int pageNumber=Configuration.DefaultPageNumber,
                                                       [FromQuery] int pageSize=Configuration.DefaultPageSize)
        {
            var request = new GetAllCategoryRequest
            {
                UserId = ApiConfiguration.UserId,
                PageNumber = pageNumber,    
                PageSize = pageSize 
            };

            
            var response = await categoryHandler.GetAllAsync(request);

            return response.IsSuccess
                ? TypedResults.Ok(response)
                : TypedResults.BadRequest(response);
        }
    }
}
