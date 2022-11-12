using FlapKap.Core.UnitOfWork;
using FlapKap.Infrastructure.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlapKap.Infrastructure.PipeLines
{
    internal class UnitOfWorkPipeLine<TIn, TOut> : IPipelineBehavior<TIn, TOut> where TIn:IRequest<TOut>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitOfWorkPipeLine(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<TOut> Handle(TIn request, 
            RequestHandlerDelegate<TOut> next, 
            CancellationToken cancellationToken)
        {
            

            var result = await next();

            //check if the excuting request is command
            if (request is BaseCommand)
            {
                await _unitOfWork.CompleteAsync();
            }

            return result;

        }
    }
}
