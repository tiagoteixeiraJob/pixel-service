namespace PixelAPI.Models
{
    public class Info
    {
        public Info(string referrer, string userAgent, string ipAddress)
        {
            this.Referrer = referrer;
            this.UserAgent = userAgent;
            this.IpAddress = ipAddress;
        }

        public string Referrer { get; set; }
        public string UserAgent { get; set; }
        public string? IpAddress { get; set; }
    }
}