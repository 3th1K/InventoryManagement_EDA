using System.Collections.Concurrent;

namespace NotificationService.Storage;

public static class EventLogStorage
{
    public static ConcurrentBag<string> EventLogs { get; } = new ConcurrentBag<string>();

    public static Task AddEventLog(string log)
    {
        EventLogStorage.EventLogs.Add(log);
        return Task.CompletedTask;
    }
}