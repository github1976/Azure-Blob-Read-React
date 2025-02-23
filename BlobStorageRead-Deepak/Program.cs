// See https://aka.ms/new-console-template for more information
    // using System;
    // using System.IO;
    // using System.Threading.Tasks;
    // using Azure.Storage.Blobs;
    // using Azure.Storage.Blobs.Models;




    // namespace BlobStorageExample
    // {
    //     class Program
    //     {
    //         // private const string BlobServiceEndpoint = "https://testdeepak4.blob.core.windows.net/?";
    //         // private const string SasToken = "sv=2022-11-02&ss=bfqt&srt=sco&sp=rwdlacupiytfx&se=2025-02-20T10:12:48Z&st=2025-02-20T02:12:48Z&spr=https&sig=RR8%2BGQDjrWsFNGMHNxbFxm2rZCF%2FPhxVqWFy3YA5nYg%3D";
    //         // private const string ContainerName = "test1234";
    //         // private const string BlobName = "sample1.json";

    //         private const string BlobServiceEndpoint = "https://nmrkpidev.blob.core.windows.net/";
    //         private const string SasToken = "?sp=r&st=2024-10-28T10:35:48Z&se=2025-10-28T18:35:48Z&spr=https&sv=2022-11-02&sr=b&sig=bdeoPWtefikVgUGFCUs4ihsl22ZhQGu4%2B4cAfoMwd4k%3D";
    //         private const string ContainerName = "dev-test";
    //         private const string BlobName = "dev-test.json";

    //         static async Task Main(string[] args)
    //         {
    //             // Create a BlobServiceClient using the SAS token
    //             BlobServiceClient blobServiceClient = new BlobServiceClient(new Uri($"{BlobServiceEndpoint}{SasToken}"));

    //             // Get a reference to the container
    //             BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

    //             // Get a reference to the blob
    //             BlobClient blobClient = containerClient.GetBlobClient(BlobName);

    //             // Download the blob content
    //             BlobDownloadInfo blobDownloadInfo = await blobClient.DownloadAsync();

    //             // Read the content as a stream and convert to string
    //             using (StreamReader reader = new StreamReader(blobDownloadInfo.Content, leaveOpen: true))
    //             {
    //                 string jsonContent = await reader.ReadToEndAsync();
    //                 Console.WriteLine("JSON Content:");
    //                 Console.WriteLine(jsonContent);
    //             }
    //         }
    //     }
        
    // }

using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BlobStorageReadDeepak
{
    class Program
    {
        private const string BlobServiceEndpoint = "https://nmrkpidev.blob.core.windows.net/";
        private const string SasToken = "?sp=r&st=2024-10-28T10:35:48Z&se=2025-10-28T18:35:48Z&spr=https&sv=2022-11-02&sr=b&sig=bdeoPWtefikVgUGFCUs4ihsl22ZhQGu4%2B4cAfoMwd4k%3D";
        private const string ContainerName = "dev-test";
        private const string BlobName = "dev-test.json";

        static async Task Main(string[] args)
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

            // Output the properties to verify deserialization
            foreach (var property in properties)
            {
                Console.WriteLine($"Property ID: {property.PropertyId}");
                Console.WriteLine($"Property Name: {property.PropertyName}");
                Console.WriteLine("Features: " + string.Join(", ", property.Features));
                Console.WriteLine("Highlights: " + string.Join(", ", property.Highlights));
                Console.WriteLine("Transportation Options:");
                if (property.TransportationOptions != null)
                {
                    foreach (var transport in property.TransportationOptions)
                    {
                        Console.WriteLine($"  Type: {transport.Type}, Line: {transport.Line}, Distance: {transport.Distance}");
                    }
                }
                else
                {
                    Console.WriteLine("  None");
                }
                Console.WriteLine("Spaces:");
                foreach (var space in property.Spaces)
                {
                    Console.WriteLine($"  Space ID: {space.SpaceId}, Space Name: {space.SpaceName}");
                    foreach (var rentRoll in space.RentRoll)
                    {
                        Console.WriteLine($"    Month: {rentRoll.Month}, Rent: {rentRoll.Rent}");
                    }
                }
                Console.WriteLine();
            }
        }
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
