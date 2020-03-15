using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Xamarin.Forms;

namespace CHCHALC.Services
{
    public class AzureBlogStorage : ICloudStorage
    {
        private const string connectionString = "DefaultEndpointsProtocol=https;AccountName=alife;AccountKey=zmmnfHIisHhiDKPSoMR+BKwA9RBiS335oid6gJnVwF3u7JM5Wzy6xrLC6Jxd/uzkHkPnlI1TGSD1R/Za0Kdj6A==;EndpointSuffix=core.windows.net";
        private readonly CloudStorageAccount _cloudStorageAccount;
        private readonly CloudBlobClient _blobClient;

        public AzureBlogStorage()
        {
            _cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            _blobClient = _cloudStorageAccount.CreateCloudBlobClient();
        }

        public async Task<bool> UploadFileAsync(string fileId, byte[] content)
        {
            CloudBlobContainer cloudBlobContainer = _blobClient.GetContainerReference("pictures");
            await cloudBlobContainer.CreateIfNotExistsAsync();

            // Set the permissions so the blobs are public.
            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            await cloudBlobContainer.SetPermissionsAsync(permissions);

            // Get a reference to the blob address, then upload the file to the blob.
            // Use the value of localFileName for the blob name.
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileId);

            var imageResizer = DependencyService.Get<IImageResizer>();
            var byteArray = imageResizer.ResizeImage(content, 480   );
            await cloudBlockBlob.UploadFromByteArrayAsync(byteArray, 0, byteArray.Length);

            return true;
        }

        public async Task<bool> DownloadAsync(string destFile, string fileId)
        {
            CloudBlobContainer cloudBlobContainer = _blobClient.GetContainerReference("pictures");
            await cloudBlobContainer.CreateIfNotExistsAsync();

            // Set the permissions so the blobs are public.
            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            await cloudBlobContainer.SetPermissionsAsync(permissions);

            // Get a reference to the blob address, then upload the file to the blob.
            // Use the value of localFileName for the blob name.
            CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileId);
            await cloudBlockBlob.UploadFromFileAsync(destFile);
            return true;
        }
    }
}
