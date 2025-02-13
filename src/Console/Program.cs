using System;
using System.IO;
using System.Linq;
using LogConverter.Core.Interfaces;
using LogConverter.Core.Services;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddTransient<ILogParser, LogParser>();
services.AddTransient<ILogConverter, LogConverterService>();
services.AddTransient<IFileService, FileService>();

var serviceProvider = services.BuildServiceProvider();
var parser = serviceProvider.GetRequiredService<ILogParser>();
var converter = serviceProvider.GetRequiredService<ILogConverter>();
var fileService = serviceProvider.GetRequiredService<IFileService>();

try
{
    // Validação de argumentos
    if (args.Length != 2)
    {
        Console.WriteLine("Usage: dotnet run <inputFilePath> <outputFilePath>");
        Console.WriteLine("Example: dotnet run input.txt output.log");
        return 1;
    }

    var inputPath = args[0];
    var outputPath = args[1];

    // Validação do arquivo de entrada
    if (!File.Exists(inputPath))
    {
        Console.WriteLine($"Error: Input file not found at '{Path.GetFullPath(inputPath)}'");
        return 2;
    }

    // Validação da extensão do arquivo de entrada
    if (Path.GetExtension(inputPath).ToLower() != ".txt")
    {
        Console.WriteLine("Error: Input file must be a .txt file");
        return 3;
    }

    // Validação/Criação do diretório de saída
    var outputDirectory = Path.GetDirectoryName(outputPath);
    if (!Directory.Exists(outputDirectory) && !string.IsNullOrEmpty(outputDirectory))
    {
        Directory.CreateDirectory(outputDirectory);
    }

    // Processamento do arquivo
    var inputLines = fileService.ReadLines(inputPath).ToList();
    
    if (!inputLines.Any())
    {
        Console.WriteLine("Warning: Input file is empty. No output generated.");
        return 0;
    }

    var outputLines = inputLines
        .Select((line, index) =>
        {
            try
            {
                return converter.Convert(parser.Parse(line));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing line {index + 1}: {line}");
                Console.WriteLine($"Details: {ex.Message}");
                return null;
            }
        })
        .Where(entry => entry != null)
        .Select(entry => $"{entry.HttpMethod} {entry.StatusCode} {entry.UriPath} {entry.TimeTaken} {entry.ResponseSize} {entry.CacheStatus}");

    fileService.WriteLines(outputPath, outputLines);

    Console.WriteLine($"Successfully converted {inputLines.Count} lines");
    Console.WriteLine($"Output file created at: {Path.GetFullPath(outputPath)}");
    return 0;
}
catch (Exception ex)
{
    Console.WriteLine($"Critical error: {ex.Message}");
    Console.WriteLine("Stack trace:");
    Console.WriteLine(ex.StackTrace);
    return 99;
}