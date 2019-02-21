CREATE TABLE [dbo].[HistoricalChangesets] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [EventId]     UNIQUEIDENTIFIER NOT NULL,
	[ObjectData]      NVARCHAR (MAX)   NULL,
	[ObjectDelta]      NVARCHAR (MAX)   NULL,
    [CreatedDate]  DATETIME2         NULL,
    [CreatedBy]    NVARCHAR (50)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

