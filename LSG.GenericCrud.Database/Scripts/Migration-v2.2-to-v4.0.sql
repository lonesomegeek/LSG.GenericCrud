print N'Creating tables'
GO

CREATE TABLE [dbo].[HistoricalChangesets]
(
	[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
	[EventId] [uniqueidentifier] NOT NULL,
	[ObjectData] [nvarchar](max) NULL,
	[ObjectDelta] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](255) NULL,

	-- constraints
	CONSTRAINT PK_HistoricalChangesets PRIMARY KEY (Id)
)
GO
CREATE TABLE [dbo].[HistoricalEvents_v4] 
(
	[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
	[EntityId] [nvarchar](255) NOT NULL,
	[EntityName] [nvarchar](255) NOT NULL,
	[Action] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](255) NULL

	-- constraints
	CONSTRAINT PK_HistoricalEvents_v4 PRIMARY KEY (Id)
)
GO

print N'Copying existing data into new table structure'
GO
INSERT 
    INTO dbo.HistoricalEvents_v4
    SELECT 
            Id,
            EntityId,
            EntityName,
            Action,
            ActionDate CreatedDate,
            TriggeredBy CreatedBy
            FROM dbo.HistoricalEvents

INSERT 
    INTO dbo.HistoricalChangesets
    SELECT 
            NEWID() Id,
            Id EventId,
            Changeset ObjectData,
            NULL ObjectDelta,
            ActionDate CreatedDate,
            TriggeredBy CreatedBy
            FROM dbo.HistoricalEvents

print N'Renaming tables'
exec sp_rename 'dbo.HistoricalEvents', 'HistoricalEvents_old'
exec sp_rename 'dbo.HistoricalEvents_v4', 'HistoricalEvents'

print N'Renaming indices'
exec sp_rename 'dbo.HistoricalEvents_old.PK_HistoricalEvents', 'PK_HistoricalEvents_old', 'INDEX'
exec sp_rename 'dbo.HistoricalEvents.PK_HistoricalEvents_v4', 'PK_HistoricalEvents', 'INDEX'

print N'Done.'