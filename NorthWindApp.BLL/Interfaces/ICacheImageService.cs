using System.Threading.Tasks;

namespace NorthWindApp.BLL.Interfaces
{
    public interface ICacheImageService
    {
        Task<byte[]> GetImage(string key);
        Task SetImage(string key, byte[] image);
        Task ClearAsync();
    }
}
