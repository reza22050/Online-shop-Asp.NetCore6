using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace StaticFile.EndPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public ImagesController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Post(string apiKey)
        {
            if(apiKey != "mysecretkey")
            {
                return BadRequest();
            }

            try
            {
                var files = Request.Form.Files;
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(),folderName);
                if (files != null)
                {
                    //upload
                    return Ok(UploadFile(files));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
                throw new Exception("upload image error", ex);
                
            }

        }

        private UploadDto UploadFile(IFormFileCollection files)
        {
            string newName = Guid.NewGuid().ToString();
            var date = DateTime.Now;
            string folder = $@"Resources\Images\{date.Year}\{date.Month}-{date.Day}\";
            var UploadsRootFolder = Path.Combine(hostingEnvironment.WebRootPath, folder);
            if(!Directory.Exists(UploadsRootFolder))
            {
                Directory.CreateDirectory(UploadsRootFolder); 
            }

            List<string> address = new List<string>();
            foreach (var file in files)
            {
                if(file != null && file.Length > 0)
                {
                    string fileName = newName + file.FileName;
                    var filePath = Path.Combine(UploadsRootFolder,fileName);
                    using (var fileStram = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStram);
                    }
                    address.Add(folder+fileName);
                }
            }
            return new UploadDto()
            {
                FileNameAddress = address,
                Status = true
            };
        }
    }


    public class UploadDto
    {
        public bool Status { get; set; }    
        public List<string> FileNameAddress { get; set; }
    }

} 
