CREATE TABLE [dbo].[Items]
(
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL

	-- constraints
	CONSTRAINT PK_Items PRIMARY KEY (Id)
)
