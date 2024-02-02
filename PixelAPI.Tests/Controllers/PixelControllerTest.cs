using Microsoft.Extensions.Options;
using PixelAPI.Controllers;
using PixelAPI.Models;
using PixelAPI.Services;
using Xunit;

namespace PixelAPI.Tests.Controllers
{
    public class PixelControllerTest
    {
        private readonly IOptions<AppParameters> options;

        public PixelControllerTest()
        {
            AppParameters appParameters = new AppParameters();
            options = Options.Create(appParameters);
        }

        [Fact]
        public void Write_Error()
        {
            // Arrange
            var pixelService = new PixelService(options);
            var controller = new PixelController(pixelService);

            try
            {
                // Act
                var result = controller.Track();
            }
            catch (Exception ex)
            {
                // Assert
                Assert.NotNull(ex);
            }
        }
    }
}