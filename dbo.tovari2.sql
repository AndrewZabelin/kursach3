CREATE TABLE [dbo].[tovari2] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Product]  NVARCHAR (50) NOT NULL,
    [Provider] NVARCHAR (50) NOT NULL,
    [Amount]   INT           NOT NULL,
    [Price]    INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

