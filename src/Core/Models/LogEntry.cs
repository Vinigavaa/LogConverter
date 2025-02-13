namespace LogConverter.Core.Models;

public record OldLogEntry(
    int ResponseSize,
    int StatusCode,
    string CacheStatus,
    string HttpRequest,
    double TimeTaken
);

public record NewLogEntry(
    string HttpMethod,
    int StatusCode,
    string UriPath,
    int TimeTaken,
    int ResponseSize,
    string CacheStatus
);