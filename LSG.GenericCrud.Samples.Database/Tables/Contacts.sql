CREATE TABLE [dbo].[Contacts]
(
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL

	-- constraints
	CONSTRAINT PK_Contacts PRIMARY KEY (Id)
)
