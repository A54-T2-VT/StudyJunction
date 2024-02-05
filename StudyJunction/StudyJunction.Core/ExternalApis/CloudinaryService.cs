using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using StudyJunction.Infrastructure.Exceptions;
using System.Net;

namespace StudyJunction.Core.ExternalApis
{
    public class CloudinaryService
    {
        private Cloudinary cloudinary;

        public CloudinaryService()
        {
            Account account = new Account(
              "dxhiilbyu",
              "876566559519863",
              "WRIbU2JBLvGuAvX0C2KUdGkbHMA");

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
    }
}
