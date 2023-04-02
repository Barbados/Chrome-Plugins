using System;
using System.Net;

namespace capturerservice.Helpers
{
    public static class HttpHelper
    {
        public static string GetContent(string url)
        {
            var webClient = new WebClient();

            return webClient.DownloadString(url);
        }
    }
}
