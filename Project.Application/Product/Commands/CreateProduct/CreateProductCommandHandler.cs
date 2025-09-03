using ErrorOr;
using Project.Application.Common.Interfaces;
using Project.Application.Common.MediatorInterfaces;
using Project.Application.Mapping.Product;

namespace Project.Application.Product.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandlerRepository<CreateProductCommand, ErrorOr<Domain.Product>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;
        private readonly ICatecoryRepository _catecoryRepository;
        private readonly ISuppliersRepository _suppliersRepository;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork,
            IProductRepository productRepository,
            ICatecoryRepository catecoryRepository,
            ISuppliersRepository suppliersRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _catecoryRepository = catecoryRepository;
            _suppliersRepository = suppliersRepository;
        }

        public async Task<ErrorOr<Domain.Product>> Handle(CreateProductCommand request)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                if (!await _catecoryRepository.FindAsync(request.AddProductDto.CategoryID) && request.AddProductDto.CategoryID is not null)
                    return Error.NotFound("Validation Type Error", "CategoryID does not exist");

                if (!await _suppliersRepository.FindAsync(request.AddProductDto.SupplierID) && request.AddProductDto.SupplierID is not null)
                    return Error.NotFound("Validation Type Error", "SupplierID does not exist");

                if (request.AddProductDto.UnitPrice is not null && request.AddProductDto.UnitPrice < 0)
                    return Error.Validation("Validation Type Error", "UnitPrice should be greater than or equal to 0");

                var id = await _productRepository.GetMaxId();

                var newproduct = request.AddProductDto.AddNewProductMapper();
                newproduct.ProductID = id + 1;
                await _productRepository.AddProductAsync(newproduct);
                await _unitOfWork.CommitAsync();
                return newproduct;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return Error.Failure("Error while processing the request");
            }
        }
    }
}
