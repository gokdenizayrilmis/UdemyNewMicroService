using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using UdemyNewMicroService.Order.Application.Contracts.Repositories;
using UdemyNewMicroService.Order.Application.Contracts.UnitOfWorks;
using UdemyNewMicroService.Order.Domain.Entities;
using UdemyNewMicroService.Shared;
using UdemyNewMicroService.Shared.Services;

namespace UdemyNewMicroService.Order.Application.Features.Orders.Create
{
    public class CreateOrderCommandHandler(IOrderRepository orderRepository, 
        IGenericRepository<int, Address> addressRepository, IIdentityService identityService, IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            if (!request.Items.Any())
            {
                return ServiceResult.Error("Order Items Not Found", "Order must have at least one item.", System.Net.HttpStatusCode.BadRequest);
            }

            
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            var newAddress = new Address
            {
                Province = request.Address.Province,
                District = request.Address.District,
                Street = request.Address.Street,
                ZipCode = request.Address.ZipCode,
                Line = request.Address.Line
            };

            addressRepository.Add(newAddress);
            await unitOfWork.CommitAsync(cancellationToken);

            //unitOfWork.CommitAsync();

            var order = Domain.Entities.Order.CreateUnPaidOrder(identityService.GetUserId, request.DiscountRate, newAddress.Id);

            foreach (var orderItem in request.Items) 
            {
                order.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.UnitPrice);
            }

            order.Address = newAddress;

            orderRepository.Add(order);
            await unitOfWork.CommitAsync(cancellationToken);


            var paymentId = Guid.Empty;



            order.SetPaymentId(paymentId);
            orderRepository.Update(order);
            await unitOfWork.CommitAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
