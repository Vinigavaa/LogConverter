using LogConverter.Core.Models;
using LogConverter.Core.Services; // ✅ Namespace correto
using Xunit;

namespace LogConverter.Core.Tests;

public class ConverterTests
{
    [Fact]
    public void Should_Convert_Entry_Correctly()
    {
        // Especifique o namespace completo
        var converter = new LogConverter.Core.Services.LogConverterService(); // ⚠️ Alteração crucial
        
        var oldEntry = new OldLogEntry(
            312, 
            200, 
            "HIT", 
            "GET /test HTTP/1.1", 
            100.2
        );

        var result = converter.Convert(oldEntry);

        Assert.Equal("GET", result.HttpMethod);
        Assert.Equal("/test", result.UriPath);
        Assert.Equal(100, result.TimeTaken);
    }

    [Fact]
    public void Should_Handle_Invalidate_Cache_Status()
    {
        var converter = new LogConverter.Core.Services.LogConverterService(); // ⚠️ Mesma correção
        
        var oldEntry = new OldLogEntry(
            245,
            200,
            "INVALIDATE",
            "POST /api HTTP/1.1",
            150.7
        );

        var result = converter.Convert(oldEntry);

        Assert.Equal("REFRESH_HIT", result.CacheStatus);
    }
}