using Microsoft.AspNetCore.Mvc;
using Project.Application.Common.MediatorInterfaces;
using Project.Application.DTOs;
using Project.Application.Product.Commands.CreateProduct;
using Project.Application.Product.Commands.DeleteProduct;
using Project.Application.Product.Queries.GetProduct;

namespace Project.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        IMediatorRepository _mediator;

        public ProductsController(IMediatorRepository mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(AddProductDto addProductDto)
        {
            var result = await _mediator.Send(new CreateProductCommand(addProductDto));
            return ProblemOr(result);
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
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            return ProblemOr(result);
        }
    }
}
