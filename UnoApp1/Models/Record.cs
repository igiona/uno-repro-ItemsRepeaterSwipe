using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoApp1.Models;

public record FilteredRecordsGroup(string Filter, IImmutableList<Record> Records, bool IsExpanded);

[ImplicitKeys(IsEnabled = false)]
public record Record
{
    public int Id { get; init; }

    public DateTime StartTime { get; init; }
    public DateTime? EndTime { get; init; }

    public TimeSpan? Duration => EndTime.HasValue ? EndTime - StartTime : null;
}
