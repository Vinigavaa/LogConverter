using LogConverter.Core.Interfaces;
using LogConverter.Core.Models;

namespace LogConverter.Core.Services;

public class LogParser : ILogParser
{
    public OldLogEntry Parse(string logLine)
    {
        var parts = logLine.Split('|');
        return new OldLogEntry(
            int.Parse(parts[0]),
            int.Parse(parts[1]),
            parts[2],
            parts[3].Trim('"'),
            double.Parse(parts[4], System.Globalization.CultureInfo.InvariantCulture)
        );
    }
}