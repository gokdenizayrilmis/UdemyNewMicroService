using System;
using System.Collections.Generic;
using System.Text;
using UdemyNewMicroService.Order.Application.Contracts.UnitOfWorks;

namespace UdemyNewMicroService.Order.Persistence.UnitOfWork
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        public Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return context.SaveChangesAsync(cancellationToken);
        }
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            await context.Database.BeginTransactionAsync(cancellationToken);
        }
        public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            return context.Database.CommitTransactionAsync(cancellationToken);
        }

    }
}
