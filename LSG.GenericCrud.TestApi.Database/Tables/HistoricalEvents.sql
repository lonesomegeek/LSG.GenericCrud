CREATE TABLE [dbo].[HistoricalEvents]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[EntityId] UNIQUEIDENTIFIER NOT NULL,
	[EntityName] nvarchar(50) NOT NULL,
	[Action] nvarchar(50) NOT NULL,
	[Changeset] nvarchar(max) NULL,
	
	[CreatedDate] DATETIME NULL,
	[CreatedBy] NVARCHAR(50) NULL,
	[ModifiedDate] DATETIME NULL,
	[ModifiedBy] NVARCHAR(50) NULL

	-- constraints
	CONSTRAINT PK_HistoricalEvents PRIMARY KEY (Id),
)
