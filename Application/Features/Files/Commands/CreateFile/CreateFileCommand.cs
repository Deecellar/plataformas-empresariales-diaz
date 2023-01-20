using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Interfaces;

namespace Application.Features.Files.Commands.CreateFile
{
    public class CreateFileCommand : IRequest<Response<Guid>>
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public long FileLenght { get; set; }

        public int FileServiceId { get; set; }
    }
    public class CreateFileHandler : IRequestHandler<CreateFileCommand, Response<Guid>>
    {
        private readonly IFileRepositoryAsync _fileRepository;
        private readonly IMapper _mapper;

        public CreateFileHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _fileRepository = unitOfWork.FileRepository;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var File = _mapper.Map<File>(request);
                File.Id = Guid.NewGuid();
                var result = await _fileRepository.AddAsync(File);
                return new Response<Guid>(result.Id, "Se ha creado con exito el archivo");
            }
            catch (AggregateException ex)
            {
                var response = new Response<Guid>("Han ocurrido multiples errores")
                {
                    Errors = ex.InnerExceptions.Select(x => x.Message).ToList()
                };
                return response;
            }
            catch (Exception e)
            {
                var response = new Response<Guid>(e.Message)
                {
                    Errors = new List<string>()
                };
                response.Errors.Add(e.Message);
                return response;
            }
        }
    }
}
