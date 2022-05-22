using Application.Catalogs.CatalogItems.GetCatalogItemPDP;
using Application.Catalogs.CatalogItems.GetCatalogItemPLP;
using Application.Comments.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.EndPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
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


        [HttpGet]
        public IActionResult Get([FromQuery] CatalogPLPRequestDto request)
        {
            return Ok(_getCatalogItemPLPService.Execute(request));
        }

        [HttpGet]
        [Route("PDP")]
        public IActionResult Get([FromQuery] string Slug)
        {
            return Ok(_getCatalogItemPDPService.Execute(Slug));
        }


        [HttpPost]
        public IActionResult Post([FromBody] CommentDto commmentDto)
        {
            SendCommentCommad sendComment = new SendCommentCommad(commmentDto);
            var result = _mediator.Send(sendComment).Result;
            return Ok(result);
        }


    }
}
