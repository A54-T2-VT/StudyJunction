using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using StudyJunction.Infrastructure.Exceptions;
using System.Net;

namespace StudyJunction.Core.ExternalApis
{
    public class CloudinaryService
    {
        private Cloudinary cloudinary;

        public CloudinaryService()
        {
            var keys = ReadApiKeysFromFile();

            Account account = new Account(
              keys.Cloud,
              keys.ApiKey,
              keys.ApiSecret);

            cloudinary = new Cloudinary(account);


        }


        public string[] UploadPdfToCloudinary(IFormFile pdfFile, string publicId = null)
        {
            try
            {
                var uploadParams = new RawUploadParams
                {
                    File = new FileDescription(pdfFile.FileName, pdfFile.OpenReadStream()),
                    PublicId = publicId,
                };

                var uploadResult = cloudinary.Upload(uploadParams);

                var result = new string[] { uploadResult.PublicId, uploadResult.SecureUrl.ToString() };



                Console.WriteLine($"PDF uploaded to Cloudinary. Public ID: {uploadResult.PublicId}");
                Console.WriteLine($"PDF URL: {uploadResult.SecureUri}");

                //var result = cloudinary.GetResourceByAssetId("04d2877623961049b41b4925869b760c");

                return result;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during PDF upload: {ex.Message}");

                throw new CloudinaryFileUploadException(ex.Message);
                // Handle the error as needed
            }
        }

        public string[] UploadProfilePicToCloudinary()
        {
            throw new NotImplementedException();
        }

        public string[] UploadVideoToCloudinary()
        {
            throw new NotImplementedException ();
        }

        private CloudinaryKeys ReadApiKeysFromFile()
        {
            try
            {
                const string fileName = "CloudinaryApiKeys.json";
                // Get the full path to the JSON file in the same directory as the executable
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                // Read the JSON file
                string jsonContent = File.ReadAllText(filePath);

                // Deserialize JSON to object
                var apiKeys = JsonConvert.DeserializeObject<CloudinaryKeys>(jsonContent);

                return apiKeys;
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., file not found, invalid JSON, etc.)
                Console.WriteLine($"Error reading API keys: {ex.Message}");
                return null;
            }
        }
    }
}
