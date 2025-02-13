namespace LogConverter.Core.Interfaces;
using System.Collections.Generic;
using IEnumerable = System.Collections.Generic.IEnumerable<string>;
public interface IFileService
{
    IEnumerable<string> ReadLines(string path);
    void WriteLines(string path, IEnumerable<string> lines);
}