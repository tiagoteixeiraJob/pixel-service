using PixelAPI.Models;

namespace PixelAPI.Services
{
    public interface IPixelService
    {
        string GetLocalIPAddress();

        void SendToStorageService(Info data);
    }
}