namespace Project.Application.Common.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Domain.ViewClasses.ViewProductData>> GetAllProductsAsync();
        Task<List<Domain.ViewClasses.ViewProductData>> GetProductByGuidAsync(int id);
    }
}
