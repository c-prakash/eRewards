using AdminPortal.Web.Application.Interfaces.Repositories;
using AdminPortal.Web.Domain.Entities.Catalog;
using AdminPortal.Web.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace AdminPortal.Web.Application.Features.Products.Commands.Delete
{
    public class DeleteMemberCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteProductCommandHandler : IRequestHandler<DeleteMemberCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<DeleteProductCommandHandler> _localizer;

        public DeleteProductCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<DeleteProductCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteMemberCommand command, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(command.Id);
            if (product != null)
            {
                await _unitOfWork.Repository<Product>().DeleteAsync(product);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(product.Id, _localizer["Product Deleted"]);
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Product Not Found!"]);
            }
        }
    }
}