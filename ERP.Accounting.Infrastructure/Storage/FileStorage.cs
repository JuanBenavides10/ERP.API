using ERP.Accounting.Application.Interfaces.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Accounting.Infrastructure.Storage
{
    public class FileStorageOptions
    {
        public string RootPath { get; set; } = "uploads";
    }

    public class FileStorage : IFileStorage
    {

        private readonly FileStorageOptions _options;

        public FileStorage(IOptions<FileStorageOptions> options)
        {
            _options = options.Value;
        }

        public async Task<string> SaveAsync(IFormFile file, string folder, CancellationToken ct)
        {      
            var root = Path.GetFullPath(_options.RootPath); 
            var targetDir = Path.Combine(root, folder);
            Directory.CreateDirectory(targetDir);

            var ext = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid():N}{ext}";
            var absolutePath = Path.Combine(targetDir, fileName);

            await using var fs = new FileStream(absolutePath, FileMode.Create);
            await file.CopyToAsync(fs, ct);

            return Path.Combine(folder, fileName).Replace("\\", "/");
        }


        public Task DeleteAsync(string? relativePath, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return Task.CompletedTask;

            var root = Path.GetFullPath(_options.RootPath);

            var cleanedRelative = relativePath.Replace("/", Path.DirectorySeparatorChar.ToString()).TrimStart(Path.DirectorySeparatorChar);

            var fullPath = Path.GetFullPath(Path.Combine(root, cleanedRelative));

            if (!fullPath.StartsWith(root, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Ruta inválida para eliminación.");

            if (File.Exists(fullPath))
                File.Delete(fullPath);

            return Task.CompletedTask;
        }

    }
}
