using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyServerUpload.Models;

namespace MyServerUpload.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {

        [HttpPost("upload")]
        public async Task<ActionResult> Upload(IFormFile file)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/images");
                var stream = new FileStream(path, FileMode.Create);
                file.CopyToAsync(stream);
                return Ok(new { length = file.Length, name = file.Name });
            }
            catch
            {
                return BadRequest();
            }
            
        }


        [HttpPost("uploads")]
        public async Task<IActionResult> Uploads(List<IFormFile>files)
        {
            try
            {
                var result = new List<FileUploadResult>();
                foreach (var file in files)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    var stream = new FileStream(path, FileMode.Create);
                    file.CopyToAsync(stream);
                    result.Add(new FileUploadResult() { Name = file.FileName, Length = file.Length });

                }
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }


    }
}