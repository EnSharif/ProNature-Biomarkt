CREATE TABLE [dbo].[Products] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (50) NOT NULL,
    [Brand]    NVARCHAR (50) NOT NULL,
    [Categorz] NVARCHAR (50) NOT NULL,
    [Price]    FLOAT (53)    NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

