using Fina.Api.Data;
using Fina.Core.Common;
using Fina.Core.Enums;
using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers
{
    public class TransactionHandler(AppDbContext context) : ITransactionHandler
    {
        public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
        {
            try
            {
                if (request is { Type: ETransactionType.Withdraw, Amount: >= 0 })
                    request.Amount *= -1;

                var transaction = new Transaction
                {
                    CategoryId = request.CategoryId,
                    CreatedAt = DateTime.Now,
                    Amount = request.Amount,
                    PaidOrRecievedAt = request.PaidOrRecievedAt,
                    Title = request.Title,
                    Type = request.Type
                };

                await context.Transactions.AddAsync(transaction);
                await context.SaveChangesAsync();
                return new Response<Transaction?>(transaction, code: 201, message: "Transação criada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response<Transaction?>(null, code: 500, message: "Não foi possivel criar a Transação!");
            }
        }

        public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
        {
            try
            {
                var transaction = await context.Transactions.FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

                if (transaction is null)
                    return new Response<Transaction?>(null, code: 404, message: "Transação não encontrada!");

                context.Transactions.Remove(transaction);

                await context.SaveChangesAsync();

                return new Response<Transaction?>(transaction, message: "Transação excluida com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response<Transaction?>(null, code: 500, message: "Não foi possivel deletar a transação!");
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetAllAsync(GetAllTransactionRequest request)
        {
            try
            {
                var query = context.Transactions
                                                 .AsNoTracking()
                                                 .Where(x => x.UserId == request.UserId)
                                                 .OrderBy(x => x.Title);


                var transactions = await query
                                      .Skip((request.PageNumber - 1) * request.PageSize)
                                      .Take(request.PageSize)
                                      .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(transactions, count, request.PageNumber, request.PageSize);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
        {
            try
            {
                var transaction = await context.Transactions.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

                return (transaction is null)
                         ? new Response<Transaction?>(null, code: 404, message: "Transação não encontrada!")
                         : new Response<Transaction?>(transaction);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<PagedResponse<List<Transaction>?>> GetByPeriodAsync(GetTransactionByPeriodRequest request)
        {
            try
            {
                request.StartDate ??= DateTime.Now.GetFirstDay(null, null);
                request.EndsDate ??= DateTime.Now.GetLastDay(null, null);

                var query = context.Transactions
                                  .AsNoTracking()
                                  .Where(x => (x.PaidOrRecievedAt >= request.StartDate && x.PaidOrRecievedAt <= request.EndsDate) && x.UserId == request.UserId)
                                  .OrderBy(x => x.PaidOrRecievedAt);

                var transactions = await query.Skip((request.PageNumber - 1) * request.PageSize)
                                             .Take(request.PageSize)
                                             .ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Transaction>?>(transactions, count, request.PageNumber, request.PageSize);
            }
            catch (Exception)
            {

                return new PagedResponse<List<Transaction>?>(null, code: 404, message: "Não foi possivel determinar a data de inicio e termino!");
            }
        }

        public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
        {
            try
            {
                if (request is { Type: ETransactionType.Withdraw, Amount: >= 0 })
                    request.Amount *= -1;

                var transaction = await context.Transactions.FirstOrDefaultAsync(c => c.Id == request.Id && c.UserId == request.UserId);

                if (transaction is null)
                    return new Response<Transaction?>(null, code: 404, message: "Transação não encontrada!");

                transaction.CategoryId = request.CategoryId;
                transaction.Amount = request.Amount;
                transaction.PaidOrRecievedAt = request.PaidOrRecievedAt;
                transaction.Title = request.Title;
                transaction.Type = request.Type;

                context.Transactions.Update(transaction);
                await context.SaveChangesAsync();
                return new Response<Transaction?>(transaction, message: "Transação actualizada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response<Transaction?>(null, code: 500, message: "Não foi possivel actualizar a transação!");
            }
        }
    }
}
