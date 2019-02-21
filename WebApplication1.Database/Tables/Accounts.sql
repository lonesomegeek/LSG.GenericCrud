CREATE TABLE [dbo].[Accounts] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [Name]         NVARCHAR (50)    NULL,
	[Description]  NVARCHAR (50)    NULL,
	[CreatedDate]  DATETIME         NULL,
    [ModifiedDate] DATETIME         NULL,
    [CreatedBy]    NVARCHAR (50)    NULL,
    [ModifiedBy]   NVARCHAR (50)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

