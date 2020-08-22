using System.Threading.Tasks;

namespace NorthWindApp.BLL.Interfaces
{
    public interface ICacheImageService
    {
        Task<byte[]> GetImageAsync(string key);
        Task SetImageAsync(string key, byte[] image);
        Task ClearAsync();
    }
}
