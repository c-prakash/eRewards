using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AdminPortal.Web.Application.Interfaces.Repositories;
using AdminPortal.Web.Domain.Entities.Misc;
using AdminPortal.Web.Shared.Wrapper;
using MediatR;

namespace AdminPortal.Web.Application.Features.Documents.Queries.GetById
{
    public class GetDocumentByIdQuery : IRequest<Result<GetDocumentByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetDocumentByIdQueryHandler : IRequestHandler<GetDocumentByIdQuery, Result<GetDocumentByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetDocumentByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetDocumentByIdResponse>> Handle(GetDocumentByIdQuery query, CancellationToken cancellationToken)
        {
            var document = await _unitOfWork.Repository<Document>().GetByIdAsync(query.Id);
            var mappedDocument = _mapper.Map<GetDocumentByIdResponse>(document);
            return await Result<GetDocumentByIdResponse>.SuccessAsync(mappedDocument);
        }
    }
}