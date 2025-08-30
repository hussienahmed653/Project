using ErrorOr;
using Project.Application.Common.Interfaces;
using Project.Application.Common.MediatorInterfaces;

namespace Project.Application.Product.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandlerRepository<DeleteProductCommand, ErrorOr<Deleted>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork,
            IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteProductCommand request)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                if(!await _productRepository.FindAsync(request.id))
                    return Error.NotFound("Product Not Found", "The product with the specified ID does not exist.");
                var product = await _productRepository.GetProduct(request.id);

                product.IsDeleted = true;
                product.DeletedOn = DateTime.UtcNow;
                await _productRepository.DeleteProductAsync();
                await _unitOfWork.CommitAsync();
                return Result.Deleted;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return Error.Failure("Can't Delete Product");
            }
        }
    }
}
