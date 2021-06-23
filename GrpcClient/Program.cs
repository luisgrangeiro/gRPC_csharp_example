using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("==== Realizando operação de pagamento via gRPC =====");

            var request = new PaymentRequest()
            {
                Id = 1,
                Amount = 50,
                Reference = "or_1234"
            };

            var result = GetPaymentResult(request).GetAwaiter().GetResult();

            Console.WriteLine($"Resultado do pagamento: \nValor da Transação: {request.Amount} \nPaymentId: {request.Id} \nStaus: {result}");

            Console.ReadKey();
        }

        static async Task<string> GetPaymentResult(PaymentRequest request)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");

            var client = new Payment.PaymentClient(channel);

            var reply = await client.DoPaymentAsync(request);

            return reply.Status;
        }
    }
}
