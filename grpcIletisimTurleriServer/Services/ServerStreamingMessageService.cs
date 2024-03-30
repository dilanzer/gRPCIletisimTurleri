using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using grpcServerStreamingIletisimServer;

namespace grpcIletisimTurleriServer.Services
{
    public class ServerStreamingMessageService : ServerStreamingMessage.ServerStreamingMessageBase
    {
        private readonly ILogger<ServerStreamingMessageService> _logger;
        public ServerStreamingMessageService(ILogger<ServerStreamingMessageService> logger)
        {
            _logger = logger;
        }

        public override async Task SendServerStreamingMessage(ServerStreamingMessageRequest request, IServerStreamWriter<ServerStreamingMessageResponse> responseStream, ServerCallContext context)
        {
            Console.WriteLine($"Message: {request.Message} | Name: {request.Name}");

            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(new ServerStreamingMessageResponse{
                    Message = "Merhaba " + i
                });
            }
        }
    }
}