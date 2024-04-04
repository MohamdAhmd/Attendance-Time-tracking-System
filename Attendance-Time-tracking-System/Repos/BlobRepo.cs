using Azure.Storage.Blobs;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Attendance_Time_tracking_System.Repos
{
    public class BlobRepo : IBlobRepo
    {

        private readonly string _storageConnectionString;
        private const string ContainerName = "imagecontainer";
        private readonly BlobServiceClient blobServiceClient;
        private readonly BlobContainerClient blobContainerClient;

        public BlobRepo(IConfiguration configuration)
        {
            _storageConnectionString = configuration.GetConnectionString("AzureStorageConnectionString") ??
                "DefaultEndpointsProtocol=https;AccountName=storageattendanceiti;AccountKey=05SsoPSpGGzU+90G09IlIhMW15yE6pZe9JTc3jdz/N9sYqTgHr8B2RogWGU6f2pMwXoaBUn/yQtS+AStsyfs4Q==;EndpointSuffix=core.windows.net"; 
            blobServiceClient = new BlobServiceClient(_storageConnectionString);
            blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
        }

        public async Task<string> AddingImage(IFormFile image)
        {
            if (image == null)
            {
                return null;
            }
            try
            {
                await blobContainerClient.CreateIfNotExistsAsync();
                var blobName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                var blobClient = blobContainerClient.GetBlobClient(blobName);
                await blobClient.UploadAsync(image.OpenReadStream(), true);

                var imageUrl = blobClient.Uri.ToString();
                return imageUrl;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> RemoveImage(string imagename)
        {
            if(imagename == null)
            {
                return false;
            }
            var uri = new Uri(imagename);
            var blobname = Path.GetFileName(uri.LocalPath);
            var blobServiceClient = new BlobServiceClient(_storageConnectionString);

            var blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

            // Get the blob client
            var blobClient = blobContainerClient.GetBlobClient(blobname);

            // Delete the blob
            var response = await blobClient.DeleteIfExistsAsync();

            return response != null ? true : false;
            

        }

        public async Task<string> UpdateImage(string oldimage, IFormFile image)
        {
            try
            {
                if (image == null)
                {
                    return oldimage;
                }
                var result  =  await RemoveImage(oldimage);
                
                return await AddingImage(image);
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
