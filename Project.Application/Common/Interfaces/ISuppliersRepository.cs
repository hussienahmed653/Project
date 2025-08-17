namespace Project.Application.Common.Interfaces
{
    public interface ISuppliersRepository
    {
        Task<bool> FindAsync(int? supplierId);
        //Task<List<Domain.Suppliers>> GetAllSuppliersAsync();
        //Task<Domain.Suppliers> GetSupplierByIdAsync(int supplierId);
        //Task<int> GetMaxId();
        //Task AddSupplierAsync(Domain.Suppliers supplier);
        //Task UpdateSupplierAsync(Domain.Suppliers supplier);
        //Task DeleteSupplierAsync(int supplierId);
    }
}
