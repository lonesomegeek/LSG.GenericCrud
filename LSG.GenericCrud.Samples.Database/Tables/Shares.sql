CREATE TABLE [dbo].[Shares]
(
	[Id] [uniqueidentifier] NOT NULL,
	[ContactId] [uniqueidentifier] NOT NULL,
	[ItemId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[SharingReminder] datetime2 NULL

	-- constraints
	CONSTRAINT PK_Shares PRIMARY KEY (Id)
)
