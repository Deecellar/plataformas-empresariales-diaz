using System.ComponentModel.DataAnnotations;
using System;
using System.Threading.Tasks;
using Application.DTOs.File;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Storage.Net;
using Storage.Net.Blobs;
using MediatR;
using Application.Features.Files.Commands.CreateFile;

namespace Infrastructure.Shared.Services
{
    public class FileService : IFileService
    {
        private readonly IBlobStorage _storage;

        private readonly IMediator _mediatr;
        public FileService(IBlobStorage storage, IMediator mediatr)
        {
            _storage = storage;
            _mediatr = mediatr;
        }

        public async Task UploadFile(FileUploadRequest file)
        {
            file.FileName = CleanString(file.FileName, "_");
            if (file.Length > 53687063712)
            {
                throw new ValidationException("The File is too big");
            }
            if (!file.FileContent.CanRead)
            {
                throw new ArgumentException("The Stream is closed");
            }

            var randomName = System.IO.Path.GetRandomFileName();
            var url = $"filestuff/{randomName}";
            var id = await _mediatr.Send(new CreateFileCommand()
            {
                FileLenght = file.Length,
                FileName = file.FileName,
                FileServiceId = 1,
                FileUrl = url
            });
            if (!id.Succeeded)
            {
                throw new Exception("Ha habido conectandose, intente mas tarde");
            }
            try
            {
                await _storage.WriteAsync(url, file.FileContent);
            }
            catch (Exception)
            {
                throw new Exception("Ha habido un error subiendo el archivo, intente mas tarde");

            }
        }
        private static string CleanString(string input, string replaceWith)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, @"[^a-z^0-9^ ^-^_^\.]", replaceWith, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }
    }
}
