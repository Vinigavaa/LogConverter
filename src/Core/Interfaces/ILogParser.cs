namespace LogConverter.Core.Interfaces;
using OldLogEntry = LogConverter.Core.Models.OldLogEntry;
public interface ILogParser
{
    OldLogEntry Parse(string logLine);
}