using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Product.Queries.GetProduct;

namespace Project.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        ISender _mediator;

        public ProductsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllProductsAsync")]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var result = await _mediator.Send(new GetProductQueries(0));
            return ProblemOr(result);
        }
        [HttpGet("GetSingleProductsAsync")]
        public async Task<IActionResult> GetSingleProductsAsync(int id)
        {
            var result = await _mediator.Send(new GetProductQueries(id));
            return ProblemOr(result);
        }
    }
}
