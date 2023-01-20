using System;
using System.IO;

namespace Application.DTOs.File
{
    public class FileUploadRequest
    {
        public long Length { get; set; }
        public string FileName {get;set;}
        public Stream FileContent { get; set; }
    }
}
