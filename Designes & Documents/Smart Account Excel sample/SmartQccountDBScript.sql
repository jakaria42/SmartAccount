USE [SmartAccount]
GO
/****** Object:  Table [dbo].[Type]    Script Date: 12/21/2011 08:18:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Type](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NULL,
 CONSTRAINT [PK_Type] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Type] ON
INSERT [dbo].[Type] ([ID], [Name]) VALUES (1, CONVERT(TEXT, N'Fixed Asset'))
INSERT [dbo].[Type] ([ID], [Name]) VALUES (2, CONVERT(TEXT, N'Capital'))
INSERT [dbo].[Type] ([ID], [Name]) VALUES (3, CONVERT(TEXT, N'Revenue'))
SET IDENTITY_INSERT [dbo].[Type] OFF
/****** Object:  Table [dbo].[Category]    Script Date: 12/21/2011 08:18:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NULL,
	[CategoryName] [varchar](500) NOT NULL,
	[IsActive] [bit] NULL,
	[Value] [int] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (1, NULL, CONVERT(TEXT, N'CBSG-2010'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (2, NULL, CONVERT(TEXT, N'Operating revenue'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (3, NULL, CONVERT(TEXT, N'Bank Interest'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (4, NULL, CONVERT(TEXT, N'Profit on Investment'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (5, NULL, CONVERT(TEXT, N'Advance Realized'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (6, NULL, CONVERT(TEXT, N'Investment encashment'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (7, NULL, CONVERT(TEXT, N'Salaries & Remuneration'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (8, NULL, CONVERT(TEXT, N'Printing & Stationery'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (9, NULL, CONVERT(TEXT, N'Material & Handouts'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (10, NULL, CONVERT(TEXT, N'Staff Energizer'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (11, NULL, CONVERT(TEXT, N'Venue rentals/ Equipment Rental'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (12, NULL, CONVERT(TEXT, N'Photography'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (13, NULL, CONVERT(TEXT, N'Transportation'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (14, NULL, CONVERT(TEXT, N'Perdiem'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (15, NULL, CONVERT(TEXT, N'Accommodation'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (16, NULL, CONVERT(TEXT, N'Food and Refreshment'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (17, NULL, CONVERT(TEXT, N'Miscellaneous '), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (18, NULL, CONVERT(TEXT, N'Payment to Resource Person'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (19, NULL, CONVERT(TEXT, N'Payment to occasional staff'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (20, NULL, CONVERT(TEXT, N'Office rent'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (21, NULL, CONVERT(TEXT, N'Utilities (Gas, water and electricity)'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (22, NULL, CONVERT(TEXT, N'Communications (Telephone, FAX, Internet, Courier)'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (23, NULL, CONVERT(TEXT, N'VAT and Taxes'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (24, NULL, CONVERT(TEXT, N'Insurance'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (25, NULL, CONVERT(TEXT, N'Office Running Cost'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (26, NULL, CONVERT(TEXT, N'Promotion and Entertainment '), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (27, NULL, CONVERT(TEXT, N'Repair/Maintenance '), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (28, NULL, CONVERT(TEXT, N'Audit Fee'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (29, NULL, CONVERT(TEXT, N'Bank Charges'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (30, NULL, CONVERT(TEXT, N'Advance '), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (31, NULL, CONVERT(TEXT, N'Investment Account'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (32, NULL, CONVERT(TEXT, N'Office Equipment'), 1, 15)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (33, NULL, CONVERT(TEXT, N'Furniture & Fixture'), 1, 20)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (34, NULL, CONVERT(TEXT, N'Cash in Hand'), 1, NULL)
INSERT [dbo].[Category] ([ID], [ParentID], [CategoryName], [IsActive], [Value]) VALUES (35, NULL, CONVERT(TEXT, N'Cash at Bank'), 1, NULL)
SET IDENTITY_INSERT [dbo].[Category] OFF
/****** Object:  Table [dbo].[Project]    Script Date: 12/21/2011 08:18:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Project](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IsActive] [bit] NULL,
	[Name] [varchar](500) NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Project] ON
INSERT [dbo].[Project] ([ID], [IsActive], [Name]) VALUES (1, 1, CONVERT(TEXT, N'CBSG-2010'))
SET IDENTITY_INSERT [dbo].[Project] OFF
/****** Object:  Table [dbo].[ProjectCategory]    Script Date: 12/21/2011 08:18:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NULL,
	[CategoryID] [int] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_ProjectCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ProjectCategory] ON
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (1, 1, 1, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (2, 1, 2, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (3, 1, 3, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (4, 1, 4, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (5, 1, 5, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (6, 1, 6, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (7, 1, 7, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (8, 1, 8, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (9, 1, 9, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (10, 1, 10, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (11, 1, 11, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (12, 1, 12, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (13, 1, 13, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (14, 1, 14, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (15, 1, 15, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (16, 1, 16, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (17, 1, 17, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (18, 1, 18, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (19, 1, 19, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (20, 1, 20, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (21, 1, 21, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (22, 1, 22, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (23, 1, 23, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (24, 1, 24, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (25, 1, 25, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (26, 1, 26, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (27, 1, 27, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (28, 1, 28, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (29, 1, 29, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (30, 1, 30, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (31, 1, 31, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (32, 1, 32, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (33, 1, 33, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (34, 1, 34, 1)
INSERT [dbo].[ProjectCategory] ([ID], [ProjectID], [CategoryID], [IsActive]) VALUES (35, 1, 35, 1)
SET IDENTITY_INSERT [dbo].[ProjectCategory] OFF
/****** Object:  Table [dbo].[CategoryType]    Script Date: 12/21/2011 08:18:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[TypeID] [int] NOT NULL,
 CONSTRAINT [PK_CategoryType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CategoryType] ON
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (1, 1, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (2, 2, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (3, 3, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (4, 4, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (5, 5, 2)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (6, 6, 2)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (7, 7, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (8, 8, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (9, 9, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (10, 10, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (11, 11, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (12, 12, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (13, 13, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (14, 14, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (15, 15, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (16, 16, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (17, 17, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (18, 18, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (19, 19, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (20, 20, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (21, 21, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (22, 22, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (23, 23, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (24, 24, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (25, 25, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (26, 26, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (27, 27, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (28, 28, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (29, 29, 3)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (30, 30, 2)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (31, 31, 2)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (32, 32, 1)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (33, 33, 1)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (34, 34, 2)
INSERT [dbo].[CategoryType] ([ID], [CategoryID], [TypeID]) VALUES (35, 35, 2)
SET IDENTITY_INSERT [dbo].[CategoryType] OFF
/****** Object:  Table [dbo].[LedgerRecord]    Script Date: 12/21/2011 08:18:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LedgerRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RecordID] [int] NOT NULL,
 CONSTRAINT [PK_Ledger_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[LedgerRecord] ON
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (1, 1)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (2, 2)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (3, 3)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (4, 4)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (5, 5)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (6, 6)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (7, 7)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (8, 8)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (9, 9)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (10, 10)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (11, 11)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (12, 12)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (13, 13)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (14, 14)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (15, 15)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (16, 16)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (17, 17)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (18, 18)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (19, 19)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (20, 20)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (21, 21)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (22, 22)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (23, 23)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (24, 24)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (25, 25)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (26, 26)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (27, 27)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (28, 28)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (29, 29)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (30, 30)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (31, 31)
INSERT [dbo].[LedgerRecord] ([ID], [RecordID]) VALUES (32, 32)
SET IDENTITY_INSERT [dbo].[LedgerRecord] OFF
/****** Object:  Table [dbo].[CashRecord]    Script Date: 12/21/2011 08:18:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CashRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RecordID] [int] NOT NULL,
 CONSTRAINT [PK_CashBook] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CashRecord] ON
INSERT [dbo].[CashRecord] ([ID], [RecordID]) VALUES (1, 42)
SET IDENTITY_INSERT [dbo].[CashRecord] OFF
/****** Object:  Table [dbo].[BankRecord]    Script Date: 12/21/2011 08:18:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BankRecord](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RecordID] [int] NOT NULL,
	[ChequeNo] [varchar](500) NULL,
 CONSTRAINT [PK_BankBook] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[BankRecord] ON
INSERT [dbo].[BankRecord] ([ID], [RecordID], [ChequeNo]) VALUES (1, 43, NULL)
SET IDENTITY_INSERT [dbo].[BankRecord] OFF
/****** Object:  ForeignKey [FK_ProjectCategory_Category]    Script Date: 12/21/2011 08:18:22 ******/
ALTER TABLE [dbo].[ProjectCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProjectCategory_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectCategory] CHECK CONSTRAINT [FK_ProjectCategory_Category]
GO
/****** Object:  ForeignKey [FK_ProjectCategory_Project]    Script Date: 12/21/2011 08:18:22 ******/
ALTER TABLE [dbo].[ProjectCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProjectCategory_Project] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectCategory] CHECK CONSTRAINT [FK_ProjectCategory_Project]
GO
/****** Object:  ForeignKey [FK_CategoryType_Category]    Script Date: 12/21/2011 08:18:22 ******/
ALTER TABLE [dbo].[CategoryType]  WITH CHECK ADD  CONSTRAINT [FK_CategoryType_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CategoryType] CHECK CONSTRAINT [FK_CategoryType_Category]
GO
/****** Object:  ForeignKey [FK_CategoryType_Type]    Script Date: 12/21/2011 08:18:22 ******/
ALTER TABLE [dbo].[CategoryType]  WITH CHECK ADD  CONSTRAINT [FK_CategoryType_Type] FOREIGN KEY([TypeID])
REFERENCES [dbo].[Type] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CategoryType] CHECK CONSTRAINT [FK_CategoryType_Type]
GO
/****** Object:  ForeignKey [FK_LedgerRecord_Record]    Script Date: 12/21/2011 08:18:22 ******/
ALTER TABLE [dbo].[LedgerRecord]  WITH CHECK ADD  CONSTRAINT [FK_LedgerRecord_Record] FOREIGN KEY([RecordID])
REFERENCES [dbo].[Record] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LedgerRecord] CHECK CONSTRAINT [FK_LedgerRecord_Record]
GO
/****** Object:  ForeignKey [FK_CashRecord_Record]    Script Date: 12/21/2011 08:18:22 ******/
ALTER TABLE [dbo].[CashRecord]  WITH CHECK ADD  CONSTRAINT [FK_CashRecord_Record] FOREIGN KEY([RecordID])
REFERENCES [dbo].[Record] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CashRecord] CHECK CONSTRAINT [FK_CashRecord_Record]
GO
/****** Object:  ForeignKey [FK_BankRecord_Record]    Script Date: 12/21/2011 08:18:22 ******/
ALTER TABLE [dbo].[BankRecord]  WITH CHECK ADD  CONSTRAINT [FK_BankRecord_Record] FOREIGN KEY([RecordID])
REFERENCES [dbo].[Record] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BankRecord] CHECK CONSTRAINT [FK_BankRecord_Record]
GO
