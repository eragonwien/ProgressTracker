use [PROGRESSTRACKER];
go

drop table if exists [Job], [Objective], [Project], [User];
go

CREATE TABLE [User] (
    [Id] int NOT NULL IDENTITY,
    [Email] nvarchar(50) UNIQUE NOT NULL,
    [Password] nvarchar(max),
	[Name] nvarchar(50),
    [Description] nvarchar(100),
    [IsActive] bit NOT NULL DEFAULT 0,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id])
);
go

CREATE TABLE [Project] (
    [Id] int NOT NULL IDENTITY,
	[UserId] int NOT NULL,
	[Name] nvarchar(50),
    [Description] nvarchar(100),
	[Status] nvarchar(20),
    [IsActive] bit NOT NULL DEFAULT 0,
    CONSTRAINT [PK_Project] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Project_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id])
);
go

CREATE TABLE [Objective] (
    [Id] int NOT NULL IDENTITY,
	[ProjectId] int NOT NULL,
    [Description] nvarchar(50),
	[Status] nvarchar(20),
    [IsActive] bit NOT NULL DEFAULT 0,
    CONSTRAINT [PK_Objective] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Objective_Project_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Project] ([Id])
);
go

CREATE TABLE [Job] (
    [Id] int NOT NULL IDENTITY,
	[ObjectiveId] int NOT NULL,
    [Description] nvarchar(50),
	[IsCompleted] bit NOT NULL DEFAULT 0,
    [IsActive] bit NOT NULL DEFAULT 0,
    CONSTRAINT [PK_Job] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Job_Objective_ObjectiveId] FOREIGN KEY ([ObjectiveId]) REFERENCES [Objective] ([Id])
);
go