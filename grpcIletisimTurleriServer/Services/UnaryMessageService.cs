using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using grpcUnaryIletisimServer;

namespace grpcIletisimTurleriServer.Services
{
    public class UnaryMessageService : UnaryMessage.UnaryMessageBase
    {
        private readonly ILogger<UnaryMessageService> _logger;
        public UnaryMessageService(ILogger<UnaryMessageService> logger)
        {
            _logger = logger;
        }

        public override async Task<UnaryMessageResponse> SendUnaryMessage(UnaryMessageRequest request, ServerCallContext context)
        {
            Console.WriteLine($"Message: {request.Message} | Name: {request.Name}");
            return new UnaryMessageResponse {
                Message = "Unary mesaj basariyla alinmistir..."
            };
        }
    }
}