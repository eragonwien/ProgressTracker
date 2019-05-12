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
    [IsActive] bit NOT NULL DEFAULT 0,
    CONSTRAINT [PK_PTUser] PRIMARY KEY ([Id])
);
go

CREATE TABLE [PTProject] (
    [Id] int NOT NULL IDENTITY,
	[PTUserId] int NOT NULL,
	[Name] nvarchar(50),
    [Description] nvarchar(100),
	[Status] nvarchar(20),
    [IsActive] bit NOT NULL DEFAULT 0,
    CONSTRAINT [PK_PTProject] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_PTProject_PTUser_UserId] FOREIGN KEY ([PTUserId]) REFERENCES [PTUser] ([Id])
);
go

CREATE TABLE [PTObjective] (
    [Id] int NOT NULL IDENTITY,
	[PTProjectId] int NOT NULL,
    [Description] nvarchar(50),
	[IsCompleted] bit NOT NULL DEFAULT 0,
    [IsActive] bit NOT NULL DEFAULT 0,
    CONSTRAINT [PK_PTObjective] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_PTObjective_PTProject_PTProjectId] FOREIGN KEY ([PTProjectId]) REFERENCES [PTProject] ([Id])
);
go

--CREATE TABLE [PTTask] (
--    [Id] int NOT NULL IDENTITY,
--	[PTObjectiveId] int NOT NULL,
--    [Description] nvarchar(50),
--	[IsCompleted] bit NOT NULL DEFAULT 0,
--    [IsActive] bit NOT NULL DEFAULT 0,
--    CONSTRAINT [PK_PTTask] PRIMARY KEY ([Id]),
--	CONSTRAINT [FK_PTTask_PTObjective_PTObjectiveId] FOREIGN KEY ([PTObjectiveId]) REFERENCES [PTObjective] ([Id])
--);
--go