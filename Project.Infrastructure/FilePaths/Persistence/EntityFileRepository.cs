using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Interfaces;
using Project.Domain;
using Project.Infrastructure.DBContext;
using System.Data;

namespace Project.Infrastructure.FilePaths.Persistence
{
    internal class EntityFileRepository : IEntityFileRepository
    {
        private readonly string wwwroot = "wwwroot/Uplodes";
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public EntityFileRepository(ApplicationDbContext context,
                                    IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public Task DeleteFileAsync(Guid guid)
        {
            var path = _context.FilePaths.FirstOrDefault(p => p.EntityGuid == guid);
            if (File.Exists(path.Path))
            {
                File.Delete(path.Path);
                _context.FilePaths.Remove(path);
                _context.SaveChanges();
                return Task.CompletedTask;
            }
            throw new FileNotFoundException("This path is not Exist");
        }

        public async Task<bool> FileIsExistAsync(Guid guid)
        {
            return await _context.FilePaths
                .AsNoTracking()
                .AnyAsync(f => f.EntityGuid == guid);
        }

        public async Task<List<FilePath>> GetAllFilesAsync(List<Guid> guids)
        {
            return await _context.FilePaths
                .Where(f => guids.Contains(f.EntityGuid))
                .ToListAsync();
        }
        public async Task<string> UploadFileAsync(IFormFile file, Guid guid)
        {
            var path = Path.Combine(wwwroot, guid.ToString());
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var extension = Path.GetExtension(file.FileName);
            var newguid = Guid.NewGuid();
            var fileName = $"{newguid}{extension}";
            var fullpath = Path.Combine(path, fileName);

            var memorystream = new MemoryStream();
            file.CopyTo(memorystream);
            memorystream.Position = 0;

            var pathreturn = await _unitOfWork.connection.QueryFirstOrDefaultAsync<string>("UploadFileAsync", new { @empguid = guid, fullpath }, _unitOfWork.transaction, commandType: CommandType.StoredProcedure);
            if(pathreturn is null)
                return null;

            using var filestream = new FileStream(fullpath, FileMode.Create);
            memorystream.Position = 0;
            memorystream.CopyTo(filestream);


            return pathreturn;

            /*
            
                alter procedure UploadFileAsync @empguid uniqueidentifier, @fullpath nvarchar(max)
                as
                    begin
	                    if exists(select 1 from Employees where EmployeeGuid = @empguid and IsDeleted = 0)
	                    begin
		                    insert into FilePaths (EntityGuid, Path)
		                    values (@empguid, @fullpath);
		                    select @fullpath
	                    end
	                    else
	                    begin
		                    return null;
	                    end
                    end
            
            */
        }
    }
}
