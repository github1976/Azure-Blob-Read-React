using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class BlobController : ControllerBase
{
    private const string BlobServiceEndpoint = "https://nmrkpidev.blob.core.windows.net/";
    private const string SasToken = "?sp=r&st=2024-10-28T10:35:48Z&se=2025-10-28T18:35:48Z&spr=https&sv=2022-11-02&sr=b&sig=bdeoPWtefikVgUGFCUs4ihsl22ZhQGu4%2B4cAfoMwd4k%3D";
    private const string ContainerName = "dev-test";
    private const string BlobName = "dev-test.json";

    [HttpGet("read-json")]
    public async Task<IActionResult> ReadJsonFile()
    {
        // Create a BlobServiceClient using the SAS token
        BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri($"{BlobServiceEndpoint}{SasToken}"));

        // Get a reference to the container
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

        // Get a reference to the blob
        BlobClient blobClient = containerClient.GetBlobClient(BlobName);

        // Download the blob content
        BlobDownloadInfo blobDownloadInfo = await blobClient.DownloadAsync();

        // Read the content as a stream and convert to string
        string jsonContent;
        using (StreamReader reader = new StreamReader(blobDownloadInfo.Content, leaveOpen: true))
        {
            jsonContent = await reader.ReadToEndAsync();
        }

        // Deserialize the JSON content into C# model classes
        List<Property> properties = JsonConvert.DeserializeObject<List<Property>>(jsonContent);

        return Ok(properties);
    }

    
    public class Property
    {
        public string PropertyId { get; set; }
        public string PropertyName { get; set; }
        public List<string> Features { get; set; }
        public List<string> Highlights { get; set; }
        public List<Transportation> TransportationOptions { get; set; }
        public List<Space> Spaces { get; set; }  // Add this line
    }

    public class Transportation
    {
        public string Type { get; set; }
        public string Line { get; set; }
        public double Distance { get; set; }
    }

    public class Space
    {
        public string SpaceId { get; set; }
        public string SpaceName { get; set; }
        public List<RentRoll> RentRoll { get; set; }
    }

    public class RentRoll
    {
        public string Month { get; set; }
        public double Rent { get; set; }
    }
    

}

