using ErrorOr;
using MediatR;
using Project.Application.DTOs;

namespace Project.Application.Product.Queries.GetProduct
{
    public record GetProductQueries(int id) : IRequest<ErrorOr<List<ProductResponseDto>>>;
}
