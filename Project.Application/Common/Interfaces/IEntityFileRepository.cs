using Microsoft.AspNetCore.Http;
using Project.Domain;

namespace Project.Application.Common.Interfaces
{
    public interface IEntityFileRepository
    {
        public Task<string> UploadFileAsync(IFormFile file, FilePath entity, string directorypath);
        public Task DeleteFileAsync(Guid guid);

    }
}
