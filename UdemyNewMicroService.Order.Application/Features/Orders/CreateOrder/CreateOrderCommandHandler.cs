using MediatR;
using UdemyNewMicroService.Order.Application.Contracts.Repositories;
using UdemyNewMicroService.Order.Application.Contracts.UnitOfWorks;
using UdemyNewMicroService.Order.Domain.Entities;
using UdemyNewMicroService.Shared;
using UdemyNewMicroService.Shared.Services;

namespace UdemyNewMicroService.Order.Application.Features.Orders.CreateOrder
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

            // 1. Transaction'ı güvenli bir şekilde başlatıyoruz
            await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                // 2. Adres nesnesini oluşturup takibe alıyoruz (DB'ye hemen kaydetmiyoruz)
                var newAddress = new Address
                {
                    Province = request.Address.Province,
                    District = request.Address.District,
                    Street = request.Address.Street,
                    ZipCode = request.Address.ZipCode,
                    Line = request.Address.Line
                };
                addressRepository.Add(newAddress);

                // 3. Siparişi oluşturuyoruz
                var order = Domain.Entities.Order.CreateUnPaidOrder(identityService.GetUserId, request.DiscountRate, newAddress.Id);

                foreach (var orderItem in request.Items)
                {
                    order.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.UnitPrice);
                }

                order.Address = newAddress;

                // Ödeme ID simülasyonu
                var paymentId = Guid.Empty;
                order.SetPaymentId(paymentId);

                // 4. Siparişi repoya ekliyoruz
                orderRepository.Add(order);

                // 5. TEK BİR SEFERDE tüm değişiklikleri (Adres ve Sipariş) SaveChanges ile SQL'e gönderiyoruz
                await unitOfWork.CommitAsync(cancellationToken);

                // 🔴 EN KRİTİK NOKTA: Başlattığımız transaction'ı fiziksel olarak onaylayıp kapatıyoruz!
                await unitOfWork.CommitTransactionAsync(cancellationToken);

                return ServiceResult.SuccessAsNoContent();
            }
            catch (Exception)
            {
                // Bir hata oluşursa yapılan tüm işlemleri (Adres dahil) geri alıyoruz
                await unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
