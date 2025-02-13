using System;
using LogConverter.Core.Interfaces;
using LogConverter.Core.Models;

namespace LogConverter.Core.Services;

public class LogConverterService : ILogConverter
{
    public NewLogEntry Convert(OldLogEntry entry)
    {
        var (method, path) = ParseHttpRequest(entry.HttpRequest);
        return new NewLogEntry(
            method,
            entry.StatusCode,
            path,
            (int)Math.Round(entry.TimeTaken, MidpointRounding.AwayFromZero),
            entry.ResponseSize,
            entry.CacheStatus == "INVALIDATE" ? "REFRESH_HIT" : entry.CacheStatus
        );
    }

    private (string Method, string Path) ParseHttpRequest(string request)
    {
        var parts = request.Split(' ');
        return (parts[0], parts[1]);
    }
}