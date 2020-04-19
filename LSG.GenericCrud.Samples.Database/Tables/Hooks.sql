CREATE TABLE [dbo].[Hooks]
(
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL

	-- constraints
	CONSTRAINT PK_Hooks PRIMARY KEY (Id)
)
