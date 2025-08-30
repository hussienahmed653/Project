using ErrorOr;
using Project.Application.Common.MediatorInterfaces;
using Project.Application.DTOs;

namespace Project.Application.Product.Commands.CreateProduct
{
    public record CreateProductCommand(AddProductDto AddProductDto) : IRequestRepository<ErrorOr<Domain.Product>>;
}
