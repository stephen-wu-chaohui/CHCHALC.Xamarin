using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CHCHALC.Services
{
    public interface ICloudStorage
    {
        Task<bool> UploadFileAsync(string fileId, byte[] content);
        Task<bool> DownloadAsync(string destFile, string fileId);
    }
}
