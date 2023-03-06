using Bycoders.DesafioDev.API.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.API.Services
{
    public class FileService : IFileService
    {
        public const string DEFAULT_CREATE_PATH = "files/ToProcess";
        public const string DEFAULT_PATH_PROCESSED = "files/Processed";       

        public bool Exists(string path,string fileName) => File.Exists(Path.Combine(Directory.GetCurrentDirectory(), path, fileName)); 
        public void Remove(string path,string fileName) => File.Delete(Path.Combine(Directory.GetCurrentDirectory(), path, fileName));

        public async Task<string> Create(IFormFile file)
        {
            if (Exists(FileService.DEFAULT_PATH_PROCESSED, file.FileName))
            {   
                Remove(FileService.DEFAULT_PATH_PROCESSED, file.FileName);                
            }

            if (Exists(FileService.DEFAULT_CREATE_PATH, file.FileName))
            {
                Remove(FileService.DEFAULT_CREATE_PATH, file.FileName);
            }

            var destinePath = Path.Combine(Directory.GetCurrentDirectory(), DEFAULT_CREATE_PATH, file.FileName);            

            using (var stream = new FileStream(destinePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return destinePath;
        }

        public void MoveProcessed(string sourcePath, string fileName)
        {
            var destinePath = Path.Combine(Directory.GetCurrentDirectory(), DEFAULT_PATH_PROCESSED, fileName);

            File.Move(sourcePath, destinePath);
        }

        public StreamReader CreateSreatemReader(string destinePath)
            => new StreamReader(destinePath);
    }
}
