using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Storage.Net;
using Storage.Net.Blobs;
using System.Linq;
using Application.DTOs.File;
using Application.Interfaces;
using System.Collections.Generic;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class FileUploadController : BaseApiController
    {
        readonly IFileService _fileService;

        public FileUploadController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost,DisableRequestSizeLimit]
        public async Task<ActionResult> UploadFiles()
        {
            var files = this.Request.Form.Files;
            long size = files.AsParallel().Sum(f => f.Length);
            int failed = 0;
            if (files.Count <= 0)
            {
                BadRequest("No hay archivos subidos");
            }

            var fileUploadRequests = files.Select(x => new FileUploadRequest()
            {
                FileName = x.FileName,
                FileContent =  x.OpenReadStream(),
                Length = x.Length
            });
            
            
            var tasks = new List<Task>();
            try
            {
                Parallel.ForEach(fileUploadRequests, x => tasks.Add(_fileService.UploadFile(x)));
                
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException e)
            {
                failed = e.InnerExceptions.Count();
            }

            await Task.FromResult(true);
            return Content($"{files.Count - failed} - {size}");


        }
    }
}
