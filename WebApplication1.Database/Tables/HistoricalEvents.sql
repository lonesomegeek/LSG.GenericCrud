CREATE TABLE [dbo].[HistoricalEvents] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [EntityId]    UNIQUEIDENTIFIER NOT NULL,
    [EntityName]  NVARCHAR (50)    NOT NULL,
    [Action]      NVARCHAR (50)    NOT NULL,
    [CreatedDate]  DATETIME2         NULL,
    [CreatedBy]    NVARCHAR (50)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

