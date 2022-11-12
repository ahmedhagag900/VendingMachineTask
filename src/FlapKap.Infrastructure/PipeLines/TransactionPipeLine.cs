using FlapKap.Core.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlapKap.Infrastructure.PipeLines
{
    internal class TransactionPipeLine<TIn,TOut> : IPipelineBehavior<TIn, TOut> where TIn : IRequest<TOut>
    {
        private readonly VendingMachieneContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public TransactionPipeLine(VendingMachieneContext context,IUnitOfWork unitOfWork)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<TOut> Handle(TIn request, RequestHandlerDelegate<TOut> next, CancellationToken cancellationToken)
        {
            try
            {
                TOut response = default(TOut);
                var stratagy = _context.Database.CreateExecutionStrategy();
                
                await stratagy.ExecuteAsync(async () =>
                {
                    await _context.Database.BeginTransactionAsync();
                    response=await next();
                    await _unitOfWork.CompleteAsync();
                    await _context.Database.CommitTransactionAsync();
                });

                return response;
            }catch(Exception)
            {
                await _context.Database.RollbackTransactionAsync();
                throw;
            }

        }
    }
}
