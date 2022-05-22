using Application.Interfaces.Contexts;
using Domain.Catalogs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Comments.Commands
{
    public class CommentDto
    {
        public string Title { get; set; }
        public string Comment { get; set; }
        public string Email { get; set; }
        public int CatalogItemId { get; set; }
    }

    public class SendCommentCommad:IRequest<SendCommentResponseDto>
    {
        public SendCommentCommad(CommentDto CommentDto)
        {
            Comment = CommentDto;
        }
        public CommentDto Comment { get; set; }
    }

    public class SendCommentResponseDto
    {
        public int Id { get; set; }
    }

    public class SendCommentHandler : IRequestHandler<SendCommentCommad, SendCommentResponseDto>
    {
        private readonly IDataBaseContext _context;

        public SendCommentHandler(IDataBaseContext context)
        {
            this._context = context;
        }
        public Task<SendCommentResponseDto> Handle(SendCommentCommad request, CancellationToken cancellationToken)
        {
            var catalogItem = _context.CatalogItems.Find(request.Comment.CatalogItemId);

            CatalogItemComment comment = new CatalogItemComment()
            {
                Comment = request.Comment.Comment,
                Email = request.Comment.Email, 
                Title = request.Comment.Title,
                CatalogItem = catalogItem
            };

            var entity = _context.CatalogItemComments.Add(comment);
            _context.SaveChanges();

            return Task.FromResult(new SendCommentResponseDto()
            {
                Id= entity.Entity.Id
            }); 
        }
    }

}
