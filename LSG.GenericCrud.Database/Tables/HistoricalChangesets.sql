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