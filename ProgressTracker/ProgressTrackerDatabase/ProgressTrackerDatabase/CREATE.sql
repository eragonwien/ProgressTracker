use [PROGRESSTRACKER];
go

drop table if exists [PTTask], [PTObjective], [PTProject], [PTUser];
go

CREATE TABLE [PTUser] (
    [Id] int NOT NULL IDENTITY,
    [Email] nvarchar(50) UNIQUE NOT NULL,
    [Password] nvarchar(max),
	[Name] nvarchar(50),
    [Description] nvarchar(100),
	[GoogleId] nvarchar(max),
    [Active] bit NOT NULL DEFAULT 0,
    CONSTRAINT [PK_PTUser] PRIMARY KEY ([Id])
);
go

CREATE TABLE [PTProject] (
    [Id] int NOT NULL IDENTITY,
	[PTUserId] int NOT NULL,
	[Name] nvarchar(50),
    [Description] nvarchar(100),
	[Status] nvarchar(20),
    [Active] bit NOT NULL DEFAULT 0,
    CONSTRAINT [PK_PTProject] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_PTProject_PTUser_UserId] FOREIGN KEY ([PTUserId]) REFERENCES [PTUser] ([Id])
);
go

CREATE TABLE [PTTask] (
    [Id] int NOT NULL IDENTITY,
	[PTProjectId] int NOT NULL,
    [Description] nvarchar(50),
	[Completed] bit NOT NULL DEFAULT 0,
    [Active] bit NOT NULL DEFAULT 0,
    CONSTRAINT [PK_PTTask] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_PTTask_PTProject_PTProjectId] FOREIGN KEY ([PTProjectId]) REFERENCES [PTProject] ([Id])
);
go