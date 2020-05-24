using QRCodeData;
using QRCodeServices.Interfaces;
using RestSharp;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace QRCodeServices.Services
{
    public class QRService : IQRService
    {
        public string ReadQRCode(string filePath)
        {
            byte[] imageBytes;
            try
            {
                using (Image image = Image.FromFile(filePath))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        imageBytes = m.ToArray();
                    }
                }
            }
            catch
            {
                return "Error reading file";
            }

            var client = new RestClient("http://api.qrserver.com/v1/read-qr-code/");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddParameter("MAX_FILE_SIZE", 1048576);
            request.AddFile("file", imageBytes, "file");
            var restResponse = client.Execute<List<QRResponse>>(request);

            if (restResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                string error = restResponse.ErrorException?.GetBaseException()?.Message ?? restResponse.ErrorMessage ?? restResponse.Content ?? "An error occured";
                return error;
            }

            return restResponse.Data[0].Symbol[0].Data ?? restResponse.Data[0].Symbol[0].Error;
        }
    }
}
