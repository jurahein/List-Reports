using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using System.IO;
using System.Collections.Generic;

namespace AzureStorageListFiles.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlobServiceClient _blobServiceClient;

        public HomeController(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> OwaspReports()
        {
            var blobs = await ListBlobsAsync("owaspreports");
            return View(blobs);
        }

        public async Task<IActionResult> SnykReports()
        {
            var blobs = await ListBlobsAsync("snykreports");
            return View(blobs);
        }

        public async Task<IActionResult> SonarReports()
        {
            var blobs = await ListBlobsAsync("sonarreports");
            return View(blobs);
        }

        public async Task<IActionResult> Download(string fileName, string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            var memoryStream = new MemoryStream();
            await blobClient.DownloadToAsync(memoryStream);
            memoryStream.Position = 0;
            return File(memoryStream, "application/octet-stream", fileName);
        }

        private async Task<List<string>> ListBlobsAsync(string containerName)
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
