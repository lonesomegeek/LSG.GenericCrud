USE [MySampleDb]
GO
	/****** Object:  Table [dbo].[Accounts]    Script Date: 2020-04-05 12:43:18 PM ******/
SET
	ANSI_NULLS ON
GO
SET
	QUOTED_IDENTIFIER ON
GO
	CREATE TABLE [dbo].[Accounts](
		[Id] [uniqueidentifier] NOT NULL,
		[Name] [nvarchar](50) NULL,
		[Description] [nvarchar](50) NULL,
		[CreatedDate] [datetime] NULL,
		[ModifiedDate] [datetime] NULL,
		[CreatedBy] [nvarchar](50) NULL,
		[ModifiedBy] [nvarchar](50) NULL,
		[Status] [nvarchar](50) NULL,
		PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
			PAD_INDEX = OFF,
			STATISTICS_NORECOMPUTE = OFF,
			IGNORE_DUP_KEY = OFF,
			ALLOW_ROW_LOCKS = ON,
			ALLOW_PAGE_LOCKS = ON
		) ON [PRIMARY]
	) ON [PRIMARY]
GO
	/****** Object:  Table [dbo].[Contacts]    Script Date: 2020-04-05 12:43:18 PM ******/
SET
	ANSI_NULLS ON
GO
SET
	QUOTED_IDENTIFIER ON
GO
	CREATE TABLE [dbo].[Contacts](
		[Id] [uniqueidentifier] NOT NULL,
		[Name] [nvarchar](50) NULL,
		[Phone] [nvarchar](50) NULL,
		PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
			PAD_INDEX = OFF,
			STATISTICS_NORECOMPUTE = OFF,
			IGNORE_DUP_KEY = OFF,
			ALLOW_ROW_LOCKS = ON,
			ALLOW_PAGE_LOCKS = ON
		) ON [PRIMARY]
	) ON [PRIMARY]
GO
	/****** Object:  Table [dbo].[HistoricalChangesets]    Script Date: 2020-04-05 12:43:18 PM ******/
SET
	ANSI_NULLS ON
GO
SET
	QUOTED_IDENTIFIER ON
GO
	CREATE TABLE [dbo].[HistoricalChangesets](
		[Id] [uniqueidentifier] NOT NULL,
		[EventId] [uniqueidentifier] NOT NULL,
		[ObjectData] [nvarchar](max) NULL,
		[ObjectDelta] [nvarchar](max) NULL,
		[CreatedDate] [datetime] NULL,
		[CreatedBy] [nvarchar](255) NULL,
		CONSTRAINT [PK_HistoricalChangesets] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
			PAD_INDEX = OFF,
			STATISTICS_NORECOMPUTE = OFF,
			IGNORE_DUP_KEY = OFF,
			ALLOW_ROW_LOCKS = ON,
			ALLOW_PAGE_LOCKS = ON
		) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
	/****** Object:  Table [dbo].[HistoricalEvents]    Script Date: 2020-04-05 12:43:18 PM ******/
SET
	ANSI_NULLS ON
GO
SET
	QUOTED_IDENTIFIER ON
GO
	CREATE TABLE [dbo].[HistoricalEvents](
		[Id] [uniqueidentifier] NOT NULL,
		[EntityId] [nvarchar](255) NOT NULL,
		[EntityName] [nvarchar](255) NOT NULL,
		[Action] [nvarchar](50) NOT NULL,
		[CreatedDate] [datetime] NULL,
		[CreatedBy] [nvarchar](255) NULL,
		CONSTRAINT [PK_HistoricalEvents] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
			PAD_INDEX = OFF,
			STATISTICS_NORECOMPUTE = OFF,
			IGNORE_DUP_KEY = OFF,
			ALLOW_ROW_LOCKS = ON,
			ALLOW_PAGE_LOCKS = ON
		) ON [PRIMARY]
	) ON [PRIMARY]
GO
	/****** Object:  Table [dbo].[Items]    Script Date: 2020-04-05 12:43:19 PM ******/
SET
	ANSI_NULLS ON
GO
SET
	QUOTED_IDENTIFIER ON
GO
	CREATE TABLE [dbo].[Items](
		[Id] [uniqueidentifier] NOT NULL,
		[Name] [nvarchar](50) NULL,
		[CreatedDate] [datetime] NULL,
		[ModifiedDate] [datetime] NULL,
		[CreatedBy] [nvarchar](50) NULL,
		[ModifiedBy] [nvarchar](50) NULL,
		PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
			PAD_INDEX = OFF,
			STATISTICS_NORECOMPUTE = OFF,
			IGNORE_DUP_KEY = OFF,
			ALLOW_ROW_LOCKS = ON,
			ALLOW_PAGE_LOCKS = ON
		) ON [PRIMARY]
	) ON [PRIMARY]
GO
ALTER TABLE
	[dbo].[HistoricalChangesets]
ADD
	DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE
	[dbo].[HistoricalEvents]
ADD
	DEFAULT (newid()) FOR [Id]
GO
	USE [master]
GO
	ALTER DATABASE [MySampleDb]
SET
	READ_WRITE
GO


CREATE TABLE [dbo].[Objects](
		[Id] [uniqueidentifier] NOT NULL,
		[Name] [nvarchar](50) NULL,
		[Description] [nvarchar](max) NULL,
		PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
			PAD_INDEX = OFF,
			STATISTICS_NORECOMPUTE = OFF,
			IGNORE_DUP_KEY = OFF,
			ALLOW_ROW_LOCKS = ON,
			ALLOW_PAGE_LOCKS = ON
		) ON [PRIMARY])

CREATE TABLE [dbo].[Shares](
		[Id] [uniqueidentifier] NOT NULL,
		[ContactId] [uniqueidentifier] NOT NULL,
		[ObjectId] [uniqueidentifier] NOT NULL,
		[Description] [nvarchar](max) NULL,
		[SharingReminder] datetime2 NULL,
		PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
			PAD_INDEX = OFF,
			STATISTICS_NORECOMPUTE = OFF,
			IGNORE_DUP_KEY = OFF,
			ALLOW_ROW_LOCKS = ON,
			ALLOW_PAGE_LOCKS = ON
		) ON [PRIMARY])


CREATE TABLE [dbo].[Users](
		[Id] [uniqueidentifier] NOT NULL,
		[Name] nvarchar(50) NOT NULL,
		PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
			PAD_INDEX = OFF,
			STATISTICS_NORECOMPUTE = OFF,
			IGNORE_DUP_KEY = OFF,
			ALLOW_ROW_LOCKS = ON,
			ALLOW_PAGE_LOCKS = ON
		) ON [PRIMARY])