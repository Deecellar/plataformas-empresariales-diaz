using System;
using System.Threading.Tasks;
using Application.DTOs.File;

namespace Application.Interfaces
{
    public interface IFileService
    {
        Task UploadFile(FileUploadRequest file );
    }
}
