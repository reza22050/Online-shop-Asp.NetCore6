using Application.Catalogs.CatalogItems.GetCatalogItemPDP;
using Application.Catalogs.CatalogItems.GetCatalogItemPLP;
using Application.Comments.Commands;
using Application.Comments.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.EndPoint.Controllers
{
    public class ProductController : Controller
    {
        private readonly IGetCatalogItemPLPService _getCatalogItemPLPService;
        private readonly IGetCatalogItemPDPService _getCatalogItemPDPService;
        private readonly IMediator _mediator;

        public ProductController(IGetCatalogItemPLPService getCatalogItemPLPService, IGetCatalogItemPDPService getCatalogItemPDPService, 
            IMediator mediator)
        {
            _getCatalogItemPLPService = getCatalogItemPLPService;
            this._getCatalogItemPDPService = getCatalogItemPDPService;
            this._mediator = mediator;
        }
        public IActionResult Index(CatalogPLPRequestDto catalogPLPRequestDto)
        {
            var data = _getCatalogItemPLPService.Execute(catalogPLPRequestDto);

            return View(data);
        }

        public IActionResult Details(string Slug)
        {
            var data = _getCatalogItemPDPService.Execute(Slug);

            GetCommentOfCatalogItemRequest itemDto = new GetCommentOfCatalogItemRequest()
            {
                CatalogItemId = data.Id
            };
            var result = _mediator.Send(itemDto);

            return View(data);
        }

        public IActionResult SendComment(CommentDto commentDto, string Slug)
        {
            SendCommentCommad sendComment = new SendCommentCommad(commentDto);
            var result = _mediator.Send(sendComment).Result;
            return RedirectToAction(nameof(Details), new { Slug = Slug });
        }

    }
}
