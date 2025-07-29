using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project.Application.Common.Interfaces;
using Project.Domain;
using Project.Infrastructure.DBContext;

namespace Project.Infrastructure.FilePaths.Persistence
{
    internal class EntityFileRepository : IEntityFileRepository
    {
        private readonly string wwwroot = "wwwroot/Uplodes";
        private readonly ApplicationDbContext _context;

        public EntityFileRepository(ApplicationDbContext context)
        {
            _context = context;
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

        public async Task<List<FilePath>> GetAllFilesAsync(List<Guid> guids)
        {
            return await _context.FilePaths
                .Where(f => guids.Contains(f.EntityGuid))
                .ToListAsync();
        }

        public async Task<string> UpdateFilePath(string path, string newfirstname)
        {
            var guidfilename = Path.GetFileNameWithoutExtension(path);
            var extention = Path.GetExtension(path);
            var newfilename = $"{guidfilename}{extention}";

            var newfolder = Path.Combine(wwwroot, newfirstname);
            if(!Directory.Exists(newfolder))
                Directory.CreateDirectory(newfolder);

            var newpath = Path.Combine(newfolder, newfilename);

            if(File.Exists(path))
                File.Move(path, newpath);

            var filepath = await _context.FilePaths
                .FirstOrDefaultAsync(f => f.Path == path);
            _context.FilePaths.Remove(filepath);
            await _context.SaveChangesAsync();

            var newFilePath = new FilePath
            {
                EntityGuid = filepath.EntityGuid,
                Path = newpath,
            };

            _context.Add(newFilePath);
            await _context.SaveChangesAsync();

            return newpath;
        }

        public Task<string> UploadFileAsync(IFormFile file, FilePath entity, string directorypath)
        {
            var path = Path.Combine(wwwroot, directorypath);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var extension = Path.GetExtension(file.FileName);
            var guid = Guid.NewGuid();
            var fileName = $"{guid}{extension}";
            var fullpath = Path.Combine(path, fileName);

            var memorystream = new MemoryStream();
            file.CopyTo(memorystream);
            memorystream.Position = 0;

            entity.Path = fullpath;

            _context.FilePaths.Add(entity);
            _context.SaveChanges();

            using var filestream = new FileStream(fullpath, FileMode.Create);
            memorystream.Position = 0;
            memorystream.CopyTo(filestream);


            return Task.FromResult(fullpath);
        }
    }
}
