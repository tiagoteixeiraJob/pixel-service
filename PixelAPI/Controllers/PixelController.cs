using Microsoft.AspNetCore.Mvc;
using PixelAPI.Models;
using PixelAPI.Services;

namespace PixelAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PixelController : ControllerBase
    {
        private readonly IPixelService pixelService;

        public PixelController(IPixelService pixelService)
        {
            this.pixelService = pixelService;
        }

        [HttpGet]
        [Route("/track")]
        public ActionResult Track()
        {
            try
            {
                string referrer = this.Request.Headers.Referer.ToString();
                string userAgent = this.Request.Headers.UserAgent.ToString();
                string ipAddress = this.pixelService.GetLocalIPAddress();

                var collectedInformation = new Info(referrer, userAgent, ipAddress);

                if (collectedInformation != null)
                {
                    this.pixelService.SendToStorageService(collectedInformation);
                }

                string gif1X1 = "R0lGODlhAQABAIAAAP///wAAACH5BAEAAAAALAAAAAABAAEAAAICRAEAOw==";
                return new FileContentResult(Convert.FromBase64String(gif1X1), "image/gif");
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception [{ex.Message}] in PixelController > Track()", ex);
            }
        }
    }
}