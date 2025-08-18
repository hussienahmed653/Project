namespace Project.Application.Common.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Domain.ViewClasses.ViewProductData>> GetAllProductsAsync();
        Task<List<Domain.ViewClasses.ViewProductData>> GetProductByGuidAsync(int id);
        Task<Domain.Product> GetProduct(int? id);
        Task<int> GetMaxId();
        Task AddProductAsync(Domain.Product product);
        Task<bool> FindAsync(int productid);
        Task DeleteProductAsync();
    }
}
