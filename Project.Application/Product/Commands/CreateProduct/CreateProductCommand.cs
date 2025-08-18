using ErrorOr;
using MediatR;
using Project.Application.DTOs;

namespace Project.Application.Product.Commands.CreateProduct
{
    public record CreateProductCommand(AddProductDto AddProductDto) : IRequest<ErrorOr<Domain.Product>>;
}
