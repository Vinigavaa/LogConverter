using System.Collections.Generic;
using System.IO;
using LogConverter.Core.Interfaces;
using IFileService = LogConverter.Core.Interfaces.IFileService;
namespace LogConverter.Core.Services;

public class FileService : IFileService
{
    public IEnumerable<string> ReadLines(string path) => File.ReadLines(path);
    
    public void WriteLines(string path, IEnumerable<string> lines) => 
        File.WriteAllLines(path, lines);
}