using Application.Interfaces.Contexts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comments.Queries
{
    public class GetCommentOfCatalogItemRequest : IRequest<List<GetCommentDto>>
    {
        public int CatalogItemId { get; set; }
    }

    public class GetCommentOfCatalogItemHandler : IRequestHandler<GetCommentOfCatalogItemRequest, List<GetCommentDto>>
    {
        private readonly IDataBaseContext _context;

        public GetCommentOfCatalogItemHandler(IDataBaseContext context)
        {
            this._context = context;
        }
        public Task<List<GetCommentDto>> Handle(GetCommentOfCatalogItemRequest request, CancellationToken cancellationToken)
        {
           var comments =  _context.CatalogItemComments.Where(x => x.CatalogItemId == request.CatalogItemId).
                Select(x => new GetCommentDto()
                {
                    Comment = x.Comment,
                    Id = x.Id,
                    Title = x.Title,
                }).ToList();
                return Task.FromResult(comments);
        }
    }

    public class GetCommentDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
    }
}
