using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.API.Domain.Interfaces
{
    public interface IFileService
    {
        bool Exists(string path, string fileName);
        void Remove(string path, string fileName);
        Task<string> Create(IFormFile file);
        void MoveProcessed(string sourcePath, string fileName);
        StreamReader CreateSreatemReader(string destinePath);
    }
}
