CREATE TABLE [dbo].[Items]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[Value] NVARCHAR(50) NOT NULL,
	[CreatedDate] DATETIME NULL,
	[CreatedBy] NVARCHAR(50) NULL,
	[ModifiedDate] DATETIME NULL,
	[ModifiedBy] NVARCHAR(50) NULL

	-- constraints
	CONSTRAINT PK_Items PRIMARY KEY (Id),

)
