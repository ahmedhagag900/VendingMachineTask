using FlapKap.Core.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlapKap.Infrastructure.PipeLines
{
    internal class TransactionPipeLine<TRequest,TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly VendingMachieneContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public TransactionPipeLine(VendingMachieneContext context,IUnitOfWork unitOfWork)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                TResponse response = default(TResponse);
                var stratagy = _context.Database.CreateExecutionStrategy();

                await stratagy.ExecuteAsync(async () =>
                {
                    await _context.Database.BeginTransactionAsync();
                    response = await next();
                    await _unitOfWork.CompleteAsync(cancellationToken);
                    await _context.Database.CommitTransactionAsync();
                });

                return response;
            }
            catch (Exception)
            {
                await _context.Database.RollbackTransactionAsync();
                throw;
            }

        }
    }
}
