using Dapper;
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
            /*
                alter procedure AddTerritoryToEmployee @empguid uniqueidentifier, @terid int
				as
				begin

					declare @empid int
					select @empid = EmployeeID
					from Employees where EmployeeGuid = @empguid

					if exists(select 1 from Employees where EmployeeGuid = @empguid and IsDeleted = 0)
					begin
						if exists(select 1 from Territorie where TerritoryID = @terid)
						begin
							if not exists(select 1 from EmployeeTerritories where EmployeeID = @empid and TerritoryID = @terid)
							begin
								insert into EmployeeTerritories (EmployeeID, TerritoryID)
								values (@empid, @terid);
								select 1
							end
							else
							begin
								select -2
							end
						end
						else
						begin
							select -1
						end
					end
					else
					begin
						select 0
					end
				end
             */
        }

        public async Task<int> RemoveTerritoryFromEmployee(Guid empguid, int terid)
        {
            return await _unitOfWork.connection.QueryFirstOrDefaultAsync<int>("RemoveTerritoryFromEmployee", new { empguid, terid }, _unitOfWork.transaction, commandType: CommandType.StoredProcedure);
            /*
			
				create procedure RemoveTerritoryFromEmployee @empguid uniqueidentifier, @terid int
				as
				begin
					declare @empid int
					select @empid = EmployeeID
					from Employees where EmployeeGuid = @empguid
					if exists(select 1 from Employees where EmployeeGuid = @empguid and IsDeleted = 0)
					begin
						if exists(select 1 from Territorie where TerritoryID = @terid)
						begin
							if exists(select 1 from EmployeeTerritories where EmployeeID = @empid and TerritoryID = @terid)
							begin
								delete from EmployeeTerritories where EmployeeID = @empid and TerritoryID = @terid
								select 1
							end
							else
							begin
								select -2
							end
						end
						else
						begin
							select -1
						end
					end
					else
					begin
						select 0
					end
				end
			*/
        }
    }
}
