using Microsoft.Extensions.Options;
using PixelAPI.Models;
using PixelAPI.Services;
using Xunit;

namespace PixelAPI.Tests.Services
{
    public class PixelServiceTest
    {
        private PixelService pixelService;
        private readonly IOptions<AppParameters> options;
        private MemoryStream memoryStream;

        public PixelServiceTest()
        {
            AppParameters appParameters = new AppParameters();
            options = Options.Create(appParameters);
        }

        [Fact]
        public void GetLocalIPAddress_Success()
        {
            //Arrange
            pixelService = new PixelService(options);

            //Act
            var result = pixelService.GetLocalIPAddress();

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void SendToStorageService_Error()
        {
            //Arrange
            options.Value.StorageServiceWriteLogUrl = "http://localhost/write";
            Info info = new Info("https://localhost", "PixelApi.Tests", "127.0.0.1");

            //Act
            pixelService = new PixelService(options);

            try
            {
                pixelService.SendToStorageService(info);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.NotNull(ex);
            }
        }
    }
}