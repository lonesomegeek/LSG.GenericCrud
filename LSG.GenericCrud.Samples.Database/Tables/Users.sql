CREATE TABLE [dbo].[Users]
(
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL

	-- constraints
	CONSTRAINT PK_Users PRIMARY KEY (Id), 
    [SomeSecretValue] NVARCHAR(MAX) NULL
)
