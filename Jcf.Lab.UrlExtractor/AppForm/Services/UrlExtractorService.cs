using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppForm.Services
{
    public class UrlExtractorService
    {
        public async Task Start()
        {
            HttpListener httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://localhost:19019/");

            httpListener.Start();
            Console.WriteLine("Servidor WebSocket iniciado em ws://localhost:19019/");

            while (true)
            {
                HttpListenerContext context = await httpListener.GetContextAsync();

                if (context.Request.IsWebSocketRequest)
                {
                    HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);
                    WebSocket webSocket = webSocketContext.WebSocket;

                    Console.WriteLine("Conexão WebSocket estabelecida.");

                    // Processar mensagens recebidas e enviar respostas
                    await ProcessWebSocketMessages(webSocket);
                }
                else
                {
                    context.Response.StatusCode = 400;
                    context.Response.Close();
                    Console.WriteLine("Requisição WebSocket inválida.");
                }
            }
        }

        public async Task ProcessWebSocketMessages(WebSocket webSocket)
        {
            byte[] buffer = new byte[1024];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (result.MessageType != WebSocketMessageType.Close)
            {
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine("Mensagem recebida: " + receivedMessage);

                if (receivedMessage == "getUrl")
                {
                    string messageToSend = "Solicitação de URL recebida";
                    byte[] messageBytes = Encoding.UTF8.GetBytes(messageToSend);
                    await webSocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
                    Console.WriteLine("Mensagem enviada: " + messageToSend);
                }

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Fechando conexão", CancellationToken.None);
            Console.WriteLine("Conexão WebSocket fechada.");
        }
    }
}


