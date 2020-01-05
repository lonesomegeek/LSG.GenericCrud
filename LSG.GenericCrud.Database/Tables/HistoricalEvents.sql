CREATE TABLE [dbo].[HistoricalEvents] 
(
	[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
	[EntityId] [nvarchar](255) NOT NULL,
	[EntityName] [nvarchar](255) NOT NULL,
	[Action] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](255) NULL

	-- constraints
	CONSTRAINT PK_HistoricalEvents PRIMARY KEY (Id)
)