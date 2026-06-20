CREATE TABLE EtlSyncLog
(
    Id TEXT NOT NULL PRIMARY KEY,
    JobName TEXT NOT NULL,
    StartedAt TEXT NOT NULL,
    CompletedAt TEXT NULL,
    Success INTEGER NOT NULL,
    ErrorMessage TEXT NULL
);

CREATE TABLE ApiClient
(
    Id TEXT NOT NULL PRIMARY KEY,
    Name TEXT NOT NULL,
    ApiKey TEXT NOT NULL,
    IsActive INTEGER NOT NULL,
    RateLimitMinutes INTEGER NOT NULL
);

CREATE TABLE InboundEventEntry
(
    Id TEXT NOT NULL PRIMARY KEY,
    RawPayload TEXT NOT NULL,
    Status INTEGER NOT NULL,
    ReceivedAt TEXT NOT NULL,
    ProcessedAt TEXT NULL,
    ErrorMessage TEXT NULL,
    AttendanceId TEXT NULL
);