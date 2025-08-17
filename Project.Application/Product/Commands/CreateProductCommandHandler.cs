using ErrorOr;
using MediatR;
using Project.Application.Common.Interfaces;
using Project.Application.Mapping.Product;

namespace Project.Application.Product.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<Domain.Product>>
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

        public async Task<ErrorOr<Domain.Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var ifprobisempty = await _unitOfWork.IfProbIsEmpty(request.AddProductDto);
                if (ifprobisempty)
                    return Error.Validation("Validation Type Error", "Fields should'nt be Empty");

                if(!await _catecoryRepository.FindAsync(request.AddProductDto.CategoryID))
                    return Error.NotFound("Validation Type Error", "CategoryID does not exist");

                if(!await _suppliersRepository.FindAsync(request.AddProductDto.SupplierID))
                    return Error.NotFound("Validation Type Error", "SupplierID does not exist");

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
