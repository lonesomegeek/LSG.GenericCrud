CREATE TABLE [dbo].[Hooks]
(
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[URL] [nvarchar](max) NULL

	-- constraints
	CONSTRAINT PK_Hooks PRIMARY KEY (Id), 
    [EntityId] NVARCHAR(MAX) NULL, 
    [EntityName] NVARCHAR(MAX) NULL
)
