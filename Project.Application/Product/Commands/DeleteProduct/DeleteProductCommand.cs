using ErrorOr;
using MediatR;

namespace Project.Application.Product.Commands.DeleteProduct
{
    public record DeleteProductCommand(int id) : IRequest<ErrorOr<Deleted>>;
}
