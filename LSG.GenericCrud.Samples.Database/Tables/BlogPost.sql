CREATE TABLE [dbo].[BlogPosts]
(
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Text] [nvarchar](max) NULL

	-- constraints
	CONSTRAINT PK_BlogPost PRIMARY KEY (Id)
)
