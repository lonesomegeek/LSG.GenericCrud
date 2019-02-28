CREATE TABLE [dbo].[HistoricalEvents] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [EntityId]    NVARCHAR(50) NOT NULL,
    [EntityName]  NVARCHAR (50)    NOT NULL,
    [Action]      NVARCHAR (50)    NOT NULL,
    [CreatedDate]  DATETIME         NULL,
    [CreatedBy]    NVARCHAR (50)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

