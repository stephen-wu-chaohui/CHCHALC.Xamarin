using System.IO;
using System.Threading.Tasks;

namespace CHCHALC.Services
{
    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }
}
