public class CoreVersion
{
    public required DateTime Timestamp { get; init; }
    public required string Action { get; init; }
    public required string Details { get; init; }
}

public class ChangeLog
{
    public required List<CoreVersion> Entries { get; init; }
}