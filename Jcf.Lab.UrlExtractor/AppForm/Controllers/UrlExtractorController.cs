using System;
using System.IO;
using System.Net;
using AppForm.Model;
using AppForm.Storage;
using Newtonsoft.Json;

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
                    string jsonReceived = reader.ReadToEnd();
                    UrlModel urlModel = JsonConvert.DeserializeObject<UrlModel>(jsonReceived);
                    Console.WriteLine($"URL recebida: {urlModel.Url}");
                    ChromeCache.Url = urlModel.Url;
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
