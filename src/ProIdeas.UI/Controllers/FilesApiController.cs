using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProIdeas.Files.Contracts;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Net;
using Microsoft.Net.Http.Headers;

namespace ProIdeas.UI.Controllers
{    
    [Route("api/files")]
    [Authorize]
    public class FilesApiController : Controller
    {
        private readonly IFileStorage _fileStorage;

        public FilesApiController(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        [HttpPost]        
        [Produces("application/json")]
        async public Task<IActionResult> Upload(ICollection<IFormFile> files)
        {
            var addedFiles = new List<Guid>();
            foreach (var file in files)
            {
                var uploadedFileId = await _fileStorage.AddFileAsync(file.FileName, file.OpenReadStream(), new { });

                addedFiles.Add(uploadedFileId);
            }

            return Json(addedFiles);
        }

        [HttpGet]
        [Route("{fileName}")]
        [AllowAnonymous]
        async public Task<HttpResponseMessage> Serve(string fileName)
        {
            var bytes = await _fileStorage.GetFileBytesAsync(fileName);

            var content = new ByteArrayContent(bytes);
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = content
            };
            

            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
            response.Content.Headers.ContentLength = bytes.Length;
            return response;
        }

    }
}