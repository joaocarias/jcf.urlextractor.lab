using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppForm.Client
{
    public class ChromeWebSocketClient
    {
        private static readonly Uri WebSocketUri = new Uri("ws://localhost:19019/");
        private static readonly int BufferSize = 1024;

        public async Task<string> GetActiveTabUrlAsync()
        {
            using (ClientWebSocket webSocket = new ClientWebSocket())
            {
                try
                {
                    // Conectando ao WebSocket
                    await webSocket.ConnectAsync(WebSocketUri, CancellationToken.None);
                    Console.WriteLine("Conectado ao WebSocket.");

                    // Enviando solicitação de URL
                    string requestMessage = "getUrl";
                    byte[] requestBuffer = Encoding.UTF8.GetBytes(requestMessage);
                    await webSocket.SendAsync(new ArraySegment<byte>(requestBuffer), WebSocketMessageType.Text, true, CancellationToken.None);
                    Console.WriteLine("Mensagem enviada: " + requestMessage);

                    // Recebendo a resposta
                    byte[] responseBuffer = new byte[BufferSize];
                    WebSocketReceiveResult responseResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(responseBuffer), CancellationToken.None);

                    string url = Encoding.UTF8.GetString(responseBuffer, 0, responseResult.Count);
                    Console.WriteLine("URL recebida: " + url);

                    return url;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao se comunicar com o WebSocket: " + ex.Message);
                    return null;
                }
            }
        }
    }
}
