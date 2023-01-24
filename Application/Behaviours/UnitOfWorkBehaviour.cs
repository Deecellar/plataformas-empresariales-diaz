using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using SqlKata;

namespace Application.Behaviours
{
    public class UnitOfWorkBehaviour<TRequest, TResponse>  : IPipelineBehavior<TRequest, TResponse>  where TRequest : IRequest<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkBehaviour(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();

            await _unitOfWork.Commit();

            return response;
        }

    }
}
