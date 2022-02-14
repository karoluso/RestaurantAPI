using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;

namespace RestaurantAPI.Controllers
{
    [Route("file")]
    [Authorize]
    public class FileController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetFile([FromQuery] string filename)
        {
            var rootPath=Directory.GetCurrentDirectory();

            var filePath = $"{rootPath}/PrivateFiles/{filename}";

            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var fileContents = System.IO.File.ReadAllBytes(filePath);


            var contentProvider = new FileExtensionContentTypeProvider();
            contentProvider.TryGetContentType(filePath, out string contentType);

            return File(fileContents, contentType, filename);

        }

        [HttpPost]
        public ActionResult Upload([FromForm]IFormFile file)
        {
            if(file!=null && file.Length > 0)
            {
               var  rootPath = Directory.GetCurrentDirectory();
                var fileName = file.FileName;
                var fullePath = $"{rootPath}/PrivateFiles/{fileName}";
                using (var stream = new FileStream(fullePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Ok();
            }

            return BadRequest();
        }
    }
}
