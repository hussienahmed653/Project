using ErrorOr;
using Project.Application.Common.MediatorInterfaces;

namespace Project.Application.Product.Commands.DeleteProduct
{
    public record DeleteProductCommand(int id) : IRequestRepository<ErrorOr<Deleted>>;
}
