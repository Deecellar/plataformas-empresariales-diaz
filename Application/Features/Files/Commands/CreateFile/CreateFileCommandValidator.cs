using System.Diagnostics.Tracing;
using System.Data;
using System.Threading;
using System;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using FluentValidation;
using Application.Interfaces;

namespace Application.Features.Files.Commands.CreateFile
{
    public class CreateFileCommandValidator : AbstractValidator<CreateFileCommand>
    {
        private readonly IFileServiceRepositoryAsync _fileServiceRepository;
        public CreateFileCommandValidator(IUnitOfWork unitOfWork)
        {
            _fileServiceRepository = unitOfWork.FileServiceRepository;

            RuleFor(f => f.FileName)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(100).WithMessage("{PropertyName} need to be less than 100 chars");

            RuleFor(f => f.FileServiceId)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MustAsync(ValidateService).WithMessage("{PropertyName} does not exist");

            RuleFor(f => f.FileUrl)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull();

            RuleFor(f => f.FileLenght)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .LessThanOrEqualTo(53687063712).WithMessage("File need to be less than 50GB");
        }

        private Task<bool>  ValidateService(int service,CancellationToken token){
            return _fileServiceRepository.CheckIfServiceExist(service);
        }
    }
}
