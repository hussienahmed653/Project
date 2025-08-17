namespace Project.Application.Common.Interfaces
{
    public interface ICatecoryRepository
    {
        Task<bool> FindAsync(int? categoryId);
        //Task<List<Domain.Categories>> GetAllCategoriesAsync();
        //Task<Domain.Categories> GetCategoryByIdAsync(int categoryId);
        //Task<int> GetMaxId();
        //Task AddCategoryAsync(Domain.Categories category);
        //Task UpdateCategoryAsync(Domain.Categories category);
        //Task DeleteCategoryAsync(int categoryId);
    }
}
