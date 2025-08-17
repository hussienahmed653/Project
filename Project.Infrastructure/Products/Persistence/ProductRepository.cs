using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Interfaces;
using Project.Domain;
using Project.Infrastructure.DBContext;

namespace Project.Infrastructure.Products.Persistence
{
    internal class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Domain.ViewClasses.ViewProductData>> GetAllProductsAsync()
        {
            return await _context.viewProductDatas.ToListAsync();
        }

        public async Task<int> GetMaxId()
        {
            return await _context.viewProductDatas
                .AnyAsync() ? await _context.viewProductDatas.MaxAsync(p => p.ProductID) : 0;
        }

        public async Task<List<Domain.ViewClasses.ViewProductData>> GetProductByGuidAsync(int id)
        {
            return await _context.viewProductDatas
                .Where(p => p.ProductID == id)
                .ToListAsync();
        }
    }
}
