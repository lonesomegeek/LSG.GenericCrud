CREATE TABLE [dbo].Carrots
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
	[Color] NVARCHAR(50) NOT NULL,
	[CreatedDate] DATETIME NULL,
	[CreatedBy] NVARCHAR(50) NULL,
	[ModifiedDate] DATETIME NULL,
	[ModifiedBy] NVARCHAR(50) NULL

	-- constraints
	CONSTRAINT PK_Carrots PRIMARY KEY (Id),

)
