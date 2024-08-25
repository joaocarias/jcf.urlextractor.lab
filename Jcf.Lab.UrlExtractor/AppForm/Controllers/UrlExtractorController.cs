using System;
using System.IO;
using System.Net;

namespace AppForm.Controllers
{
    public class UrlExtractorController
    {
        public void HandleRequest(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;

            if (request.HttpMethod == "POST" && request.Url.AbsolutePath == "/api/urlextractor/url")
            {
                using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    string urlReceived = reader.ReadToEnd();
                    Console.WriteLine($"URL recebida: {urlReceived}");

                    // Aqui você pode fazer o que quiser com a URL recebida, como armazená-la ou exibi-la em uma UI.
                }

                response.StatusCode = (int)HttpStatusCode.OK;
                response.Close();
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.Close();
            }
        }
    }
}
