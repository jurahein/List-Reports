// Services/BlobStorageService.cs
using Azure.Storage.Blobs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureStorageListFiles.Services // Adicione o namespace correto aqui
{
    public class BlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<List<string>> ListBlobsAsync(string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var files = new List<string>();

            await foreach (var blobItem in containerClient.GetBlobsAsync())
            {
                files.Add(blobItem.Name);
            }

            return files;
        }
    }
}
