using Dapper;
using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Interfaces;
using Project.Application.DTOs;
using Project.Infrastructure.DBContext;
using System.Data;

namespace Project.Infrastructure.Employee.Persistence
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _appcontext;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeRepository(ApplicationDbContext appcontext,
            IUnitOfWork unitOfWork)
        {
            _appcontext = appcontext;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddEmployeeAsync(AddEmployeeDto employee)
        {
            return await _unitOfWork.connection.QueryFirstOrDefaultAsync<int>("InsertEmployee", employee, _unitOfWork.transaction, commandType: CommandType.StoredProcedure);
            /*
            
                alter procedure InsertEmployee
				@EmployeeID int,
				@EmployeeGuid uniqueidentifier,
				@LastName nvarchar(20),
				@FirstName nvarchar(10),
				@Title nvarchar(30) = null,
				@TitleOfCourtesy nvarchar(25) = null,
				@BirthDate datetime2 = null,
				@HireDate datetime2 = null,
				@Address nvarchar(60) = null,
				@City nvarchar(15) = null,
				@Region nvarchar(15) = null,
				@PostalCode nvarchar(10) = null,
				@Country nvarchar(15) = null,
				@HomePhone nvarchar(24) = null,
				@Extension nvarchar(4) = null,
				@Notes nvarchar(max) = null,
				@ReportsTo int = null
			as
			Begin
				set @EmployeeID = (SELECT isnull(MAX(EmployeeID),0) + 1 FROM Employees)
				if(@ReportsTo is not null)
				begin
					if exists(select 1 from Employees where EmployeeID = @ReportsTo and IsDeleted = 0)
						begin
							insert into Employees 
							(
								EmployeeID,
								EmployeeGuid,
								LastName,
								FirstName,
								Title,
								TitleOfCourtesy,
								BirthDate,
								HireDate,
								Address,
								City,
								Region,
								PostalCode,
								Country,
								HomePhone,
								Extension,
								Notes,
								ReportsTo
							) 
							values 
							(
								@EmployeeID,
								@EmployeeGuid,
								@LastName,
								@FirstName,
								@Title,
								@TitleOfCourtesy,
								@BirthDate,
								@HireDate,
								@Address,
								@City,
								@Region,
								@PostalCode,
								@Country,
								@HomePhone,
								@Extension,
								@Notes,
								@ReportsTo
							)
							select @EmployeeID
						end
					else
					begin
						select -1
					end
				end
				else if(@ReportsTo is null)
				begin
					insert into Employees 
					(
						EmployeeID,
						EmployeeGuid,
						LastName,
						FirstName,
						Title,
						TitleOfCourtesy,
						BirthDate,
						HireDate,
						Address,
						City,
						Region,
						PostalCode,
						Country,
						HomePhone,
						Extension,
						Notes,
						ReportsTo
					) 
					values 
					(
						@EmployeeID,
						@EmployeeGuid,
						@LastName,
						@FirstName,
						@Title,
						@TitleOfCourtesy,
						@BirthDate,
						@HireDate,
						@Address,
						@City,
						@Region,
						@PostalCode,
						@Country,
						@HomePhone,
						@Extension,
						@Notes,
						@ReportsTo
					)
					select @EmployeeID
				end
			end
            
            */
        }


        public async Task<int> DeleteEmployeeAsync(Guid guid)
        {
            return await _unitOfWork.connection.QuerySingleOrDefaultAsync<int>("DeleteEmployee", new { guid }, _unitOfWork.transaction, commandType: CommandType.StoredProcedure);
            /*
				alter procedure DeleteEmployee @guid uniqueidentifier
				as
				begin
					if exists(select 1 from Employees where EmployeeGuid = @guid and IsDeleted = 0)
					begin
						UPDATE Employees SET IsDeleted = 1 , DeletedOn = (select GETDATE()) WHERE  EmployeeGuid = @guid
						select 1
					end
					else
						select 0
				end 
			*/
        }

        public async Task<bool> ExistAsync(Guid guid)
        {
            var sql = "SELECT COUNT(1) FROM Employees WHERE EmployeeGuid = @guid AND IsDeleted = 0";
            return await _unitOfWork.connection.ExecuteScalarAsync<bool>(sql, new { guid }, _unitOfWork.transaction);
        }

        public async Task<List<Domain.ViewClasses.ViewEmployeeData>> GetAllTableViewEmployeesAsync()
        {
            var sql = "SELECT * FROM ViewEmployeeData";
            var employee = await _unitOfWork.connection.QueryAsync<Domain.ViewClasses.ViewEmployeeData>(sql, _unitOfWork.transaction);
            return employee.ToList();
        }

        public async Task<List<Domain.ViewClasses.ViewEmployeeData>> GetTableViewEmployeeByGuIdAsync(Guid? guid)
        {
            var sql = "SELECT * FROM ViewEmployeeData where EmployeeGuid = @guid";
            var employee = await _unitOfWork.connection.QueryAsync<Domain.ViewClasses.ViewEmployeeData>(sql, new { guid }, _unitOfWork.transaction);
            return employee.ToList();
        }

        public Task UpdateEmployeeAsync(Domain.Employee employee)
        {
            _appcontext.Update(employee);
            _appcontext.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<Domain.Employee> GetTableEmployeesAsync(Guid guid)
        {
            var sql = "SELECT * FROM Employees WHERE EmployeeGuid = @guid AND IsDeleted = 0";
            return await _unitOfWork.connection.QuerySingleOrDefaultAsync<Domain.Employee>(sql, new { guid }, _unitOfWork.transaction);
        }
    }
}
