CREATE TABLE IntegrationEventLog (
    EventId UNIQUEIDENTIFIER NOT NULL Primary key,
    Content nvarchar(MAX),
    CreationTime DateTime,
    State int,
    TimesSent int,
    EventTypeName nvarchar(MAX),
    TransactionId nvarchar(MAX)
);