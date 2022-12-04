using Azure.Core;
using VendingMachine.Core.UnitOfWork;
using VendingMachine.Infrastructure.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Infrastructure.PipeLines
{
    internal class MemoryDBPipeLine<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly VendingMachieneContext _context;
        public MemoryDBPipeLine(IUnitOfWork unitOfWork, VendingMachieneContext context)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _context = context??throw new ArgumentNullException(nameof(context));
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(request is QueryBase)
            {
                return await HandleQueryRequest(next);
            }else
            {
                return await HandelCommandRequest(next, cancellationToken);
            }
        }

        private async Task<TResponse> HandelCommandRequest(RequestHandlerDelegate<TResponse> next,CancellationToken cancellationToken)
        {
            try
            {
                var res = await next();
                await _unitOfWork.CompleteAsync(cancellationToken);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<TResponse> HandleQueryRequest(RequestHandlerDelegate<TResponse> next)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return await next();
        }


    }
}
