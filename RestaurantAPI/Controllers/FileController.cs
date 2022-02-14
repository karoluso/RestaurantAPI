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
    }
}
