CREATE TABLE [dbo].[Contributors]
(
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[GitHubRepository] [nvarchar](50) NULL

	-- constraints
	CONSTRAINT PK_Contributors PRIMARY KEY (Id)
)
