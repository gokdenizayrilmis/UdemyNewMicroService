using Microsoft.EntityFrameworkCore;
using UdemyNewMicroService.Order.Application.Contracts.Repositories;

namespace UdemyNewMicroService.Order.Persistence.Repositories
{
    public class OrderRepository(AppDbContext context) :GenericRepository<Guid, Domain.Entities.Order>(context) , IOrderRepository
    { 
        public Task<List<Domain.Entities.Order>> GetOrderByBuyerId(Guid buyerId)
        {
            return context.Orders.Include(o => o.OrderItems).Where(x => x.BuyerId == buyerId).OrderByDescending(o => o.Created).ToListAsync(); 
        }   
    }
}
