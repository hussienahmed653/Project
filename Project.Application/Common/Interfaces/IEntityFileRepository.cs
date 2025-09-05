using Microsoft.AspNetCore.Http;
using Project.Domain;

namespace Project.Application.Common.Interfaces
{
    public interface IEntityFileRepository
    {
        public Task<string> UploadFileAsync(IFormFile file, Guid guid);
        public Task DeleteFileAsync(Guid guid);
        public Task<List<FilePath>> GetAllFilesAsync(List<Guid> guids);
        public Task<bool> FileIsExistAsync(Guid guid);
    }
}
