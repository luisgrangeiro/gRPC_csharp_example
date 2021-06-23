using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GrpcService
{
    public class PaymentService : Payment.PaymentBase
    {
        private readonly ILogger<PaymentService> _logger;
        public PaymentService(ILogger<PaymentService> logger)
        {
            _logger = logger;
        }

        public override Task<PaymentResponse> DoPayment(PaymentRequest request, ServerCallContext context)
        {
            if (request.Amount < 100)
            {
                return Task.FromResult(new PaymentResponse
                {
                    Status = "Authorized"
                });
            }

            return Task.FromResult(new PaymentResponse
            {
                Status = "NotAuthorized"
            });
        }
    }
}
