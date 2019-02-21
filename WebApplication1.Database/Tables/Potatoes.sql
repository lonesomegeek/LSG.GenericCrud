CREATE TABLE [dbo].[Potatoes] (
    [Id]           INT           NOT NULL,
    [Brand]        NVARCHAR (50) NULL,
    [Model]        NVARCHAR (50) NULL,
    [CreatedDate]  DATETIME      NULL,
    [ModifiedDate] DATETIME      NULL,
    [CreatedBy]    NVARCHAR (50) NULL,
    [ModifiedBy]   NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

