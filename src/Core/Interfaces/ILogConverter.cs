namespace LogConverter.Core.Interfaces;
using NewLogEntry = LogConverter.Core.Models.NewLogEntry;
using OldLogEntry = LogConverter.Core.Models.OldLogEntry;
public interface ILogConverter
{
    NewLogEntry Convert(OldLogEntry entry);
}