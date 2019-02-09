CREATE TABLE [dbo].[EntityUserStatuses] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [UserId]     NVARCHAR (50)    NOT NULL,
    [EntityName] NVARCHAR (50)    NOT NULL,
    [EntityId]   UNIQUEIDENTIFIER NOT NULL,
    [LastViewed] DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
-- TODO Create primary keys
-- TODO Create indexes for EntityId, EntityName, UserId, Id