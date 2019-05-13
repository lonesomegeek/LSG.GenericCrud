CREATE TABLE [dbo].[HistoricalEvents] 
(
	[Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
	[EntityId] [nvarchar](50) NOT NULL,
	[EntityName] [nvarchar](50) NOT NULL,
	[Action] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL

	-- constraints
	CONSTRAINT PK_HistoricalEvents PRIMARY KEY (Id)
)