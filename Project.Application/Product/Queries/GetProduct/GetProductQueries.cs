using ErrorOr;
using Project.Application.Common.MediatorInterfaces;
using Project.Application.DTOs;

namespace Project.Application.Product.Queries.GetProduct
{
    public record GetProductQueries(int id) : IRequestRepository<ErrorOr<List<ProductResponseDto>>>;
}
