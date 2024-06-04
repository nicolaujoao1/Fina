using Fina.Api.Data;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers
{
    public class CategoryHandler(AppDbContext context) : ICategoryHandler
    {
        public async Task<Response<Category?>> CreateAsync(CreateCategoryRequest request)
        {
            try
            {
                var category = new Category
                {
                    UserId = request.UserId,
                    Title = request.Title,
                    Description = request.Description
                };

                await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                return new Response<Category?>(category, code: 201, message: "Categoria criada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response<Category?>(null, code: 500, message: "Não foi possivel criar a categoria!");
            }

        }

        public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

                if (category is null)
                    return new Response<Category?>(null, code: 404, message: "Categorian não encontrada!");

                context.Categories.Remove(category);

                await context.SaveChangesAsync();

                return new Response<Category?>(category, message: "Categoria excluida com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response<Category?>(null, code: 500, message: "Não foi possivel criar a categoria!");
            }
        }

        public async Task<PagedResponse<List<Category>?>> GetAllAsync(GetAllCategoryRequest request)
        {
            try
            {
                var query = context.Categories
                                                 .AsNoTracking()
                                                 .Where(x => x.UserId == request.UserId)
                                                 .OrderBy(x => x.Title);


                var categories = await query
                                      .Skip((request.PageNumber - 1) * request.PageSize)
                                      .Take(request.PageSize)
                                      .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Category>?>(categories, count, request.PageNumber, request.PageSize);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Response<Category?>> GetByIdAsync(GetCategoryByIdRequest request)
        {
            try
            {
                var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

                return (category is null)
                         ? new Response<Category?>(null, code: 404, message: "Categoria não encontrada!")
                         : new Response<Category?>(category);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

                if (category is null)
                    return new Response<Category?>(null, code: 404, message: "Categoria não encontrada!");

                category.Title = request.Title;
                category.Description = request.Description;

                context.Categories.Update(category);
                await context.SaveChangesAsync();
                return new Response<Category?>(category, message: "Categoria actualizada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response<Category?>(null, code: 500, message: "Não foi possivel criar a categoria!");
            }
        }
    }
}
