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

        public async Task<IActionResult> ViewReport(string fileName, string containerName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            var blobDownloadInfo = await blobClient.DownloadAsync();

            var contentType = blobDownloadInfo.Value.ContentType ?? GetContentType(fileName);

            if (contentType.StartsWith("text/") || contentType.StartsWith("image/"))
            {
                using (var streamReader = new StreamReader(blobDownloadInfo.Value.Content))
                {
                    var fileContent = await streamReader.ReadToEndAsync();
                    return Content(fileContent, contentType);
                }
            }
            else if (contentType == "application/pdf")
            {
                var memoryStream = new MemoryStream();
                await blobDownloadInfo.Value.Content.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                return File(memoryStream, contentType, fileName);
            }

            return Content("Unsupported file type.");
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

        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".htm" or ".html" => "text/html",
                ".txt" => "text/plain",
                ".md" => "text/markdown",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".pdf" => "application/pdf",
                _ => "application/octet-stream",
            };
        }
    }
}
