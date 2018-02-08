

CREATE TABLE [dbo].Apps
(
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [ApiToken] NVARCHAR(MAX) NULL, 
    [CreatedDate] DATETIME NULL, 
	CONSTRAINT [PK_dbo.Apps] PRIMARY KEY CLUSTERED ([Name] ASC)
);

CREATE TABLE [dbo].[Messages]
(
	[Id] BIGINT NOT NULL IDENTITY (1, 1),
	[SenderId] NVARCHAR(256) NOT NULL, 
    [ReceiverId] NVARCHAR(256) NOT NULL, 
    [AppName] NVARCHAR(50) NOT NULL, 
    [MessageContentType] NVARCHAR(50) NOT NULL, 
    [MessageBody] NVARCHAR(MAX) NOT NULL, 
    [PostedTime] DATETIME NOT NULL, 
    CONSTRAINT [PK_dbo.Messages] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Activities] (
    [Id]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [ContentType]   NVARCHAR (50)  NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [ActionerId]    NVARCHAR (256) NOT NULL,
    [RelatedUserId] NVARCHAR (256) NOT NULL,
    [OccurenceTime] DATETIME       NOT NULL,
    CONSTRAINT [PK_dbo.Activities] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[ResourceActivities] (
    [Id]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [ResourceId] NVARCHAR (256) NOT NULL,
    [ContentType]   NVARCHAR (50)  NOT NULL,
    [Content]       NVARCHAR (MAX) NOT NULL,
    [ActionerId]    NVARCHAR (256) NOT NULL,
    [ResourceOwnerId] NVARCHAR (256) NOT NULL,
    [OccurenceTime] DATETIME       NOT NULL,
    [AppName] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_dbo.ResourceActivities] PRIMARY KEY CLUSTERED ([Id] ASC)
);