using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using grpcBidirectionalStreamingIletisimServer;

namespace grpcIletisimTurleriServer.Services
{
    public class BidirectionalStreamingMessageService : BidirectionalStreamingMessage.BidirectionalStreamingMessageBase
    {
        private readonly ILogger<BidirectionalStreamingMessageService> _logger;

        public BidirectionalStreamingMessageService(ILogger<BidirectionalStreamingMessageService> logger)
        {
            _logger = logger;
        }

        public override async Task SendBidirectionalStreamingMessage(IAsyncStreamReader<BidirectionalStreamingMessageRequest> requestStream, IServerStreamWriter<BidirectionalStreamingMessageResponse> responseStream, ServerCallContext context)
        {
            var task1 = Task.Run(async () => {
                while (await requestStream.MoveNext(context.CancellationToken))
                {
                    Console.WriteLine($"Message: {requestStream.Current.Message} | Name: {requestStream.Current.Name}");
                }
            });

            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(new BidirectionalStreamingMessageResponse {Message = "Mesaj " + i});
            }

            await task1;
        }
    }
}