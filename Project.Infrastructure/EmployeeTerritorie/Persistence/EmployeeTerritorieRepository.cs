using Dapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Interfaces;
using Project.Infrastructure.DBContext;
using System.Data;

namespace Project.Infrastructure.EmployeeTerritorie.Persistence
{
    internal class EmployeeTerritorieRepository : IEmployeeTerritorieRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeTerritorieRepository(ApplicationDbContext context,
                    IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddTerritoryToEmployee(Guid empguid, int terid)
        {
            return await _unitOfWork.connection.QueryFirstOrDefaultAsync<int>("AddTerritoryToEmployee", new { empguid, terid }, _unitOfWork.transaction, commandType: CommandType.StoredProcedure);
        }

        public async Task<int> RemoveTerritoryFromEmployee(Guid empguid, int terid)
        {
            return await _unitOfWork.connection.QueryFirstOrDefaultAsync<int>("RemoveTerritoryFromEmployee", new { empguid, terid }, _unitOfWork.transaction, commandType: CommandType.StoredProcedure);
        }
    }
}
