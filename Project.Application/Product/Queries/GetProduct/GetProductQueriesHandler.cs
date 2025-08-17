using ErrorOr;
using MediatR;
using Project.Application.Common.Interfaces;
using Project.Application.DTOs;
using Project.Application.Mapping.Product;

namespace Project.Application.Product.Queries.GetProduct
{
    public class GetProductQueriesHandler : IRequestHandler<GetProductQueries, ErrorOr<List<ProductResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public GetProductQueriesHandler(IUnitOfWork unitOfWork,
            IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<ErrorOr<List<ProductResponseDto>>> Handle(GetProductQueries request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                if(request.id is 0)
                {
                    var listOfProducts = await _productRepository.GetAllProductsAsync();
                    if (listOfProducts.Count is 0)
                        return Error.NotFound("NotFound", "There are no products available");
                    var mappedProducts = listOfProducts.GetAllProductMapper();
                    await _unitOfWork.CommitAsync();
                    return mappedProducts;
                }
                var product = await _productRepository.GetProductByGuidAsync(request.id);
                if (product.Count is 0)
                    return Error.NotFound("NotFound", "There is no product with this id");
                var mappedProduct = product.GetSingleProductMapper();
                await _unitOfWork.CommitAsync();
                return new List<ProductResponseDto> { mappedProduct };
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return Error.Failure("Error while processing the request");
            }
        }
    }
}
