using System;
using System.Collections.Generic;
using System.Text;

namespace UdemyNewMicroService.Order.Application.Contracts.UnitOfWorks
{
    public interface IUnitOfWork 
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}
