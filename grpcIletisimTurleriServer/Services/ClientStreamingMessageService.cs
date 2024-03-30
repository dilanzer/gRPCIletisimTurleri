using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using grpcClientStreamingIletisimServer;

namespace grpcIletisimTurleriServer.Services
{
    public class ClientStreamingMessageService : ClientStreamingMessage.ClientStreamingMessageBase
    {
        private readonly ILogger<ClientStreamingMessageService> _logger;

        public ClientStreamingMessageService(ILogger<ClientStreamingMessageService> logger)
        {
            _logger = logger;
        }

        public override async Task<ClientStreamingMessageResponse> SendClientStreamingMessage(IAsyncStreamReader<ClientStreamingMessageRequest> requestStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext(context.CancellationToken))
            {
                Console.WriteLine($"Message: {requestStream.Current.Message} | Name: {requestStream.Current.Name}");
            }

            return new ClientStreamingMessageResponse{
                Message = "client veri alinmistir..."
            };
        }
    }
}