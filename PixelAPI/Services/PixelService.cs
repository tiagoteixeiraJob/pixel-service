using System.Net.Sockets;
using System.Net;
using PixelAPI.Models;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace PixelAPI.Services
{
    public class PixelService : IPixelService
    {
        private readonly IOptions<AppParameters> appParameters;

        public PixelService(IOptions<AppParameters> appParameters)
        {
            this.appParameters = appParameters;
        }

        public string GetLocalIPAddress()
        {
            string ip = "127.0.0.1";

            var host = Dns.GetHostEntry(Dns.GetHostName());
            var ipAddress = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            
            if (ipAddress != null) { ip = ipAddress.ToString(); };

            return ip;
        }

        public void SendToStorageService(Info information)
        {
            string url = appParameters.Value.StorageServiceWriteLogUrl;

            string responseFromServer;
            try
            {
                WebRequest request = WebRequest.Create(url);

                request.Method = "POST";

                var data = JsonSerializer.Serialize(information);
                byte[] byteArray = Encoding.UTF8.GetBytes(data);

                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;

                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseFromServer = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception [{ex.Message}] in PixelService > SendToStorageService().", ex);
            }
        }
    }
}