

using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace StudyJunction.Core.Helpers
{
    public class CloudinaryApi
    {
        private Cloudinary cloudinary;

        public CloudinaryApi()
        {
            CloudinaryDotNet.Account account = new CloudinaryDotNet.Account(
              "dxhiilbyu",
              "876566559519863",
              "WRIbU2JBLvGuAvX0C2KUdGkbHMA");

            cloudinary = new Cloudinary(account);


        }


        public void UploadPdfToCloudinary(IFormFile pdfFile, string publicId = null)
        {
            try
            {
                //var uploadParams = new RawUploadParams
                //{
                //    File = new FileDescription(pdfFile.FileName, pdfFile.OpenReadStream()),
                //    PublicId = publicId,
                //};

                //var uploadResult = cloudinary.Upload(uploadParams);

                //Console.WriteLine($"PDF uploaded to Cloudinary. Public ID: {uploadResult.PublicId}");
                //Console.WriteLine($"PDF URL: {uploadResult.SecureUri}");

                //var result = cloudinary.GetResourceByAssetId("04d2877623961049b41b4925869b760c");
                string cloudinaryUrl = $"https://res.cloudinary.com/dxhiilbyu/image/upload/v1707070713/gnijdkhofedzmhif2hsq.pdf";


                using (WebClient webClient = new WebClient())
                {
                    // Download the PDF file from Cloudinary
                    webClient.DownloadFile(new Uri(cloudinaryUrl), "Downloads");

                    Console.WriteLine($"PDF downloaded from Cloudinary to: Downloads");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during PDF upload: {ex.Message}");
                // Handle the error as needed
            }
        }
    }
}
