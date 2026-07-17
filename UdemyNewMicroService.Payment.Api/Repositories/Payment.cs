using MassTransit;

namespace UdemyNewMicroService.Payment.Api.Repositories
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string OrderCode { get; set; }
        public DateTime Created { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }

        public Payment(Guid userId, string orderCode, decimal amount)
        {
            Create(userId, orderCode, amount);
        }

        public void Create(Guid userId, string orderCode, decimal amount) 
        {
            this.Id = NewId.NextSequentialGuid();
            this.UserId = userId;
            this.OrderCode = orderCode;
            this.Created = DateTime.UtcNow;
            this.Amount = amount;
            this.Status = PaymentStatus.Pending;
        }

        public void SetStatus(PaymentStatus status)
        {
            this.Status = status; 
        }

    }

    public enum PaymentStatus
    {
        Success = 1,
        Pending = 3,
        Failed = 2
    }
}
