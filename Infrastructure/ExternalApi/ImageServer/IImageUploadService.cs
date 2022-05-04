using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExternalApi.ImageServer
{
    public interface IImageUploadService
    {
        List<string> Upload(List<IFormFile> files);
    }

    public class ImageUploadService : IImageUploadService
    {
        public List<string> Upload(List<IFormFile> files)
        {
            var options = new RestClientOptions("https://localhost:7196/api/Images?apiKey=mysecretkey")
            {
                Timeout = -1
            };
            var client = new RestClient(options);
            var request = new RestRequest();
            foreach (var item in files)
            {
                byte[] bytes;
                using (var ms = new MemoryStream())
                {
                    item.CopyToAsync(ms);
                    bytes = ms.ToArray(); 

                }
                request.AddFile(item.FileName, bytes, item.FileName,item.ContentType);

            }
            var response = client.ExecutePostAsync(request).Result; 
            UploadDto upload  = JsonConvert.DeserializeObject<UploadDto>(response.Content);

            return upload.FileNameAddress;

        }
    }

    public class UploadDto
    {
        public bool Status { get; set; }
        public List<string> FileNameAddress { get; set; }
    }
}
