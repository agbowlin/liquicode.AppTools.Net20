USE [XmlObjectStore];
GO


DROP TABLE [dbo].[XmlObjects];
GO


CREATE TABLE [dbo].[XmlObjects]
(
	  [RowID]		[int] IDENTITY(1,1) NOT NULL
	, [ObjectID]		[varchar](100) NOT NULL
	, [ObjectType]	[varchar](100) NOT NULL
	, [ObjectName]	[varchar](100) NOT NULL
	, [ObjectXml]		[xml] NULL
	, CONSTRAINT [PK_XmlObjects_RowID] PRIMARY KEY CLUSTERED 
	(
		[RowID] ASC
	) WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE UNIQUE NONCLUSTERED INDEX [IX_XmlObjects_ObjectID] ON [dbo].[XmlObjects] 
(
	[ObjectID] ASC
) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO


CREATE UNIQUE NONCLUSTERED INDEX [IX_XmlObjects_ObjectTypeAndName] ON [dbo].[XmlObjects] 
(
	  [ObjectType] ASC
	, [ObjectName] ASC
) WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO

