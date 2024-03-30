using Grpc.Net.Client;
using grpcBidirectionalStreamingIletisimClient;
using grpcClientStreamingIletisimClient;
using grpcServerStreamingIletisimClient;
using grpcUnaryIletisimClient;

class Program
{
    static async Task Main(string[] args)
    {
        var channel = GrpcChannel.ForAddress("http://localhost:5208");

        // Unary
        // --------------------------------------------------------------------
        // var unaryMessageClient = new UnaryMessage.UnaryMessageClient(channel);

        // UnaryMessageResponse response = await unaryMessageClient.SendUnaryMessageAsync(new UnaryMessageRequest{
        //     Message = "Merhaba",
        //     Name = "Unary Mesaj"
        // });

        // Console.WriteLine(response.Message);

        // Server Streaming
        // --------------------------------------------------------------------
        // var serverStreamingMessageClient = new ServerStreamingMessage.ServerStreamingMessageClient(channel);

        // var response = serverStreamingMessageClient.SendServerStreamingMessage(new ServerStreamingMessageRequest{
        //     Message = "Merhaba",
        //     Name = "Server Streaming Mesaj"
        // });

        // CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        // while (await response.ResponseStream.MoveNext(cancellationTokenSource.Token))
        // {
        //     Console.WriteLine(response.ResponseStream.Current.Message);
        // }

        // Client Streaming
        // --------------------------------------------------------------------
        // var clientStreamingMessageClient = new ClientStreamingMessage.ClientStreamingMessageClient(channel);

        // var request = clientStreamingMessageClient.SendClientStreamingMessage();
        
        // for (int i = 0; i < 10; i++)
        // {
        //     await Task.Delay(1000);
        //     await request.RequestStream.WriteAsync(new ClientStreamingMessageRequest{
        //         Name = "Gelen client veriler",
        //         Message = "Mesaj " + i
        //     });
        // }

        // // Stream datanin sonlandigini ifade eder
        // await request.RequestStream.CompleteAsync();

        // Console.WriteLine((await request.ResponseAsync).Message);

        // Bi-Directional Streaming
        var bidirectionalStreamingMessageClient = new BidirectionalStreamingMessage.BidirectionalStreamingMessageClient(channel);

        var request = bidirectionalStreamingMessageClient.SendBidirectionalStreamingMessage();
        
        var task1 = Task.Run(async () => {
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(1000);
                request.RequestStream.WriteAsync(new BidirectionalStreamingMessageRequest{
                    Name = "Gönderilecek veriler",
                    Message = "Mesaj " + i
                });
            }
        });

        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        while (await request.ResponseStream.MoveNext(cancellationTokenSource.Token))
        {
            Console.WriteLine(request.ResponseStream.Current.Message);
        }

        await task1;
        await request.RequestStream.CompleteAsync();
        
    }
}