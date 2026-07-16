using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using UdemyNewMicroService.Order.Application.Contracts.Repositories;

namespace UdemyNewMicroService.Order.Persistence.Repositories
{
    public class OrderRepository(AppDbContext context) :GenericRepository<Guid, Domain.Entities.Order>(context) , IOrderRepository
    {
    }
}
