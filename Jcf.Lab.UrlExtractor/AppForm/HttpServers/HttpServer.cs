using AppForm.Controllers;
using System;
using System.Net;
using System.Threading;

namespace AppForm.HttpServers
{
    public class HttpServer
    {
        private readonly HttpListener _httpListener;
        private readonly UrlExtractorController _urlExtractorController;
        private readonly string _urlPrefix;

        public HttpServer(string urlPrefix)
        {
            _httpListener = new HttpListener();
            _urlExtractorController = new UrlExtractorController();
            _urlPrefix = urlPrefix; 

            _httpListener.Prefixes.Add(_urlPrefix);
        }

        public void Start()
        {
            _httpListener.Start();
            Console.WriteLine($"Servidor HTTP iniciado em {_urlPrefix}");

            Thread listenerThread = new Thread(() =>
            {
                while (_httpListener.IsListening)
                {
                    try
                    {
                        var context = _httpListener.GetContext();
                        _urlExtractorController.HandleRequest(context);
                    }
                    catch (HttpListenerException ex)
                    {
                        Console.WriteLine($"Erro: {ex.Message}");
                    }
                }
            });
            listenerThread.Start();
        }

        public void Stop()
        {
            _httpListener.Stop();
            Console.WriteLine("Servidor HTTP parado.");
        }
    }
}
