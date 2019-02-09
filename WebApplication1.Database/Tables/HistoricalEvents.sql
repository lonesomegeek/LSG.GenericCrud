CREATE TABLE [dbo].[HistoricalEvents] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [EntityId]    UNIQUEIDENTIFIER NOT NULL,
    [EntityName]  NVARCHAR (50)    NOT NULL,
    [Action]      NVARCHAR (50)    NOT NULL,
    [Changeset]   NVARCHAR (MAX)   NULL,
	[OriginalObject]      NVARCHAR (MAX)   NULL,
    [CreatedDate]  DATETIME         NULL,
    [ModifiedDate] DATETIME         NULL,
    [CreatedBy]    NVARCHAR (50)    NULL,
    [ModifiedBy]   NVARCHAR (50)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

