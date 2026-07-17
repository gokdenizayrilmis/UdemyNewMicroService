using Asp.Versioning.Builder;
using UdemyNewMicroService.Payment.Api.Features.GetAllPaymentsByUserId;
using UdemyNewMicroService.Payment.Api.Features.Payments.Create;

namespace UdemyNewMicroService.Payment.Api.Features.Payment
{
    public static class PaymentEndpointExt
    {
        public static void AddPaymentGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet) 
        {
            app.MapGroup("api/v{version:apiVersion}/payments")
                .WithTags("Payments")
                .WithApiVersionSet(apiVersionSet)
                .CreatePaymentGroupItemEndpoint()
                .GetAllPaymentByUserIdGroupItemEndpoint();
        }
    }
}