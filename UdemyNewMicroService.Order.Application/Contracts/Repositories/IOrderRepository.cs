using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyNewMicroService.Order.Application.Contracts.Repositories
{
    public interface IOrderRepository : IGenericRepository<Guid, Domain.Entities.Order>
    {
    }
}
