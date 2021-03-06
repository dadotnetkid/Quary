USE [master]
GO
/****** Object:  Database [quary]    Script Date: 5/16/2019 9:34:59 PM ******/
CREATE DATABASE [quary]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'quary', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\quary.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'quary_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\quary_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [quary] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [quary].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [quary] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [quary] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [quary] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [quary] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [quary] SET ARITHABORT OFF 
GO
ALTER DATABASE [quary] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [quary] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [quary] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [quary] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [quary] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [quary] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [quary] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [quary] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [quary] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [quary] SET  DISABLE_BROKER 
GO
ALTER DATABASE [quary] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [quary] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [quary] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [quary] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [quary] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [quary] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [quary] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [quary] SET RECOVERY FULL 
GO
ALTER DATABASE [quary] SET  MULTI_USER 
GO
ALTER DATABASE [quary] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [quary] SET DB_CHAINING OFF 
GO
ALTER DATABASE [quary] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [quary] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [quary] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [quary] SET QUERY_STORE = OFF
GO
USE [quary]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [quary]
GO
/****** Object:  Table [dbo].[ControllersActions]    Script Date: 5/16/2019 9:34:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ControllersActions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Controller] [nvarchar](255) NULL,
	[Action] [nvarchar](255) NULL,
 CONSTRAINT [PK_AuthorizedUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Facilities]    Script Date: 5/16/2019 9:34:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facilities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PermiteeId] [int] NULL,
	[FacilityName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Facilities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 5/16/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](150) NOT NULL,
	[UnitCost] [money] NOT NULL,
	[UnitMeasure] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permitees]    Script Date: 5/16/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permitees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TownCityId] [int] NULL,
	[PermiteeTypeId] [int] NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[MiddleName] [nvarchar](50) NULL,
	[CompanyName] [nvarchar](50) NULL,
	[AddressLine1] [nvarchar](150) NULL,
	[AddressLine2] [nvarchar](150) NULL,
	[EntryBy] [nvarchar](128) NULL,
	[LastEditedBy] [nvarchar](128) NULL,
 CONSTRAINT [PK_Permittees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermiteeTypes]    Script Date: 5/16/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermiteeTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PermiteeTypeName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_PermitteeTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuariesInPermitees]    Script Date: 5/16/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuariesInPermitees](
	[PermiteeId] [int] NOT NULL,
	[QuarrieId] [int] NOT NULL,
 CONSTRAINT [PK_QuariesInPermitees] PRIMARY KEY CLUSTERED 
(
	[PermiteeId] ASC,
	[QuarrieId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quarries]    Script Date: 5/16/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quarries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuarryName] [nvarchar](100) NULL,
	[JurisdictionName] [nvarchar](100) NULL,
	[EntryBy] [nvarchar](150) NULL,
	[LastEditedBy] [nvarchar](150) NULL,
 CONSTRAINT [PK_Quarries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionDetails]    Script Date: 5/16/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionId] [nvarchar](128) NOT NULL,
	[ItemId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitCost] [money] NOT NULL,
	[EntryBy] [nvarchar](150) NULL,
	[LastEditedBy] [nvarchar](150) NULL,
 CONSTRAINT [PK_TransactionDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 5/16/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[Id] [nvarchar](128) NOT NULL,
	[PermiteeId] [int] NULL,
	[TransactionDate] [datetime] NULL,
	[TransactionTotal] [money] NULL,
	[EntryBy] [nvarchar](128) NULL,
	[LastEditedBy] [nvarchar](128) NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionVehicles]    Script Date: 5/16/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionVehicles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionId] [nvarchar](128) NULL,
	[VehicleId] [int] NULL,
	[Cost] [money] NULL,
	[isRenew] [bit] NULL,
	[EntryBy] [nvarchar](128) NULL,
	[EntryModifiedBy] [nvarchar](128) NULL,
 CONSTRAINT [PK_TransactionVehicles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClaims]    Script Date: 5/16/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 5/16/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 5/16/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/16/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](128) NULL,
	[EmailConfirmed] [bit] NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](25) NULL,
	[PhoneNumberConfirmed] [bit] NULL,
	[TwoFactorEnabled] [bit] NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NULL,
	[AccessFailedCount] [int] NULL,
	[UserName] [nvarchar](50) NULL,
	[LastUpdatedBy] [nvarchar](150) NULL,
	[LastUpdated] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[MiddleName] [nvarchar](50) NULL,
	[CivilStatus] [nvarchar](12) NULL,
	[Gender] [nvarchar](6) NULL,
	[BirthDate] [datetime] NULL,
	[AddressLine2] [nvarchar](500) NULL,
	[AddressLine1] [nvarchar](500) NULL,
	[TownCity] [int] NULL,
	[Cellular] [nvarchar](25) NULL,
	[Height] [decimal](5, 2) NULL,
	[Weight] [decimal](5, 2) NULL,
	[Religion] [nvarchar](50) NULL,
	[Citizenship] [nvarchar](50) NULL,
	[Languages] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersInControllers]    Script Date: 5/16/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersInControllers](
	[ControllerId] [int] NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_UsersInControllers] PRIMARY KEY CLUSTERED 
(
	[ControllerId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersInRoles]    Script Date: 5/16/2019 9:35:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersInRoles](
	[UserRoles_Id] [nvarchar](128) NOT NULL,
	[Users_Id] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_UsersInRoles] PRIMARY KEY CLUSTERED 
(
	[UserRoles_Id] ASC,
	[Users_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicles]    Script Date: 5/16/2019 9:35:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PermiteeId] [int] NULL,
	[VehicleTypeId] [int] NULL,
	[VehicleName] [nvarchar](50) NULL,
	[PlateNo] [nvarchar](50) NULL,
 CONSTRAINT [PK_Vehicles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VehicleTypes]    Script Date: 5/16/2019 9:35:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VehicleTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VehicleTypeName] [nvarchar](50) NULL,
 CONSTRAINT [PK_VehicleTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ControllersActions] ON 

INSERT [dbo].[ControllersActions] ([Id], [Controller], [Action]) VALUES (2, N'MemberController', N'Users')
INSERT [dbo].[ControllersActions] ([Id], [Controller], [Action]) VALUES (5, N'MaintenanceController', N'Controllers')
INSERT [dbo].[ControllersActions] ([Id], [Controller], [Action]) VALUES (6, N'FileManagementController', N'TransactionItemsGridViewPartial')
INSERT [dbo].[ControllersActions] ([Id], [Controller], [Action]) VALUES (8, N'FileManagementController', N'TransactionItemsGridViewPartialAddNew')
INSERT [dbo].[ControllersActions] ([Id], [Controller], [Action]) VALUES (9, N'FileManagementController', N'TransactionItemsGridViewPartialDelete')
INSERT [dbo].[ControllersActions] ([Id], [Controller], [Action]) VALUES (10, N'FileManagementController', N'TransactionItemsGridViewPartialUpdate')
INSERT [dbo].[ControllersActions] ([Id], [Controller], [Action]) VALUES (11, N'FileManagementController', N'AddEditTransactionItemPartial')
INSERT [dbo].[ControllersActions] ([Id], [Controller], [Action]) VALUES (12, N'FileManagementController', N'TransactionItems')
INSERT [dbo].[ControllersActions] ([Id], [Controller], [Action]) VALUES (13, N'FileManagementController', N'AddEditFacilitiesPartial')
INSERT [dbo].[ControllersActions] ([Id], [Controller], [Action]) VALUES (14, N'FileManagementController', N'Permitees')
SET IDENTITY_INSERT [dbo].[ControllersActions] OFF
SET IDENTITY_INSERT [dbo].[Items] ON 

INSERT [dbo].[Items] ([Id], [ItemName], [UnitCost], [UnitMeasure]) VALUES (1, N'Item Name', 3000.0000, N'cm')
SET IDENTITY_INSERT [dbo].[Items] OFF
SET IDENTITY_INSERT [dbo].[Permitees] ON 

INSERT [dbo].[Permitees] ([Id], [TownCityId], [PermiteeTypeId], [FirstName], [LastName], [MiddleName], [CompanyName], [AddressLine1], [AddressLine2], [EntryBy], [LastEditedBy]) VALUES (4, NULL, 3, N'Mark Christopher', N'Cacal', N'Ramos', N'Tech Static', N'Bayombong', NULL, NULL, NULL)
INSERT [dbo].[Permitees] ([Id], [TownCityId], [PermiteeTypeId], [FirstName], [LastName], [MiddleName], [CompanyName], [AddressLine1], [AddressLine2], [EntryBy], [LastEditedBy]) VALUES (5, NULL, 3, N'Enter your name...', N'Cacal', N'D', N'Company Name', N'Address Line1', NULL, N'b97f5a28-5900-4787-bf54-7f4029796a6d', N'b97f5a28-5900-4787-bf54-7f4029796a6d')
SET IDENTITY_INSERT [dbo].[Permitees] OFF
SET IDENTITY_INSERT [dbo].[PermiteeTypes] ON 

INSERT [dbo].[PermiteeTypes] ([Id], [PermiteeTypeName]) VALUES (3, N'Premium')
SET IDENTITY_INSERT [dbo].[PermiteeTypes] OFF
INSERT [dbo].[QuariesInPermitees] ([PermiteeId], [QuarrieId]) VALUES (4, 2)
INSERT [dbo].[QuariesInPermitees] ([PermiteeId], [QuarrieId]) VALUES (4, 3)
INSERT [dbo].[QuariesInPermitees] ([PermiteeId], [QuarrieId]) VALUES (5, 2)
INSERT [dbo].[QuariesInPermitees] ([PermiteeId], [QuarrieId]) VALUES (5, 3)
SET IDENTITY_INSERT [dbo].[Quarries] ON 

INSERT [dbo].[Quarries] ([Id], [QuarryName], [JurisdictionName], [EntryBy], [LastEditedBy]) VALUES (2, N'Santa Rosa', N'Bayombong', NULL, N'b97f5a28-5900-4787-bf54-7f4029796a6d')
INSERT [dbo].[Quarries] ([Id], [QuarryName], [JurisdictionName], [EntryBy], [LastEditedBy]) VALUES (3, N'Don Mariano', N'Bayombong', NULL, N'b97f5a28-5900-4787-bf54-7f4029796a6d')
SET IDENTITY_INSERT [dbo].[Quarries] OFF
SET IDENTITY_INSERT [dbo].[TransactionDetails] ON 

INSERT [dbo].[TransactionDetails] ([Id], [TransactionId], [ItemId], [Quantity], [UnitCost], [EntryBy], [LastEditedBy]) VALUES (7, N'f2eea2e3-b063-4e07-b122-46e1758328ab', 1, 3, 3000.0000, NULL, NULL)
INSERT [dbo].[TransactionDetails] ([Id], [TransactionId], [ItemId], [Quantity], [UnitCost], [EntryBy], [LastEditedBy]) VALUES (8, N'6ef43fdc-a010-4fc4-9ae0-4982ed48c044', 1, 6000, 4.0000, NULL, NULL)
INSERT [dbo].[TransactionDetails] ([Id], [TransactionId], [ItemId], [Quantity], [UnitCost], [EntryBy], [LastEditedBy]) VALUES (9, N'654bfb7e-1318-4e63-bdcd-15c1803e189d', 1, 6, 60000.0000, NULL, NULL)
INSERT [dbo].[TransactionDetails] ([Id], [TransactionId], [ItemId], [Quantity], [UnitCost], [EntryBy], [LastEditedBy]) VALUES (11, N'60f249ab-aa69-4c41-9e8e-f63efd91b192', 1, 2, 4000.0000, NULL, NULL)
SET IDENTITY_INSERT [dbo].[TransactionDetails] OFF
INSERT [dbo].[Transactions] ([Id], [PermiteeId], [TransactionDate], [TransactionTotal], [EntryBy], [LastEditedBy]) VALUES (N'60f249ab-aa69-4c41-9e8e-f63efd91b192', 4, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[TransactionVehicles] ON 

INSERT [dbo].[TransactionVehicles] ([Id], [TransactionId], [VehicleId], [Cost], [isRenew], [EntryBy], [EntryModifiedBy]) VALUES (3, N'29ab9653-c698-4774-b5a4-0690ab5d4217', 2, 5000.0000, 1, NULL, NULL)
INSERT [dbo].[TransactionVehicles] ([Id], [TransactionId], [VehicleId], [Cost], [isRenew], [EntryBy], [EntryModifiedBy]) VALUES (4, N'eb0242fe-e049-45c0-832a-9beb5958f583', 2, 4000.0000, NULL, NULL, NULL)
INSERT [dbo].[TransactionVehicles] ([Id], [TransactionId], [VehicleId], [Cost], [isRenew], [EntryBy], [EntryModifiedBy]) VALUES (5, N'60f249ab-aa69-4c41-9e8e-f63efd91b192', 2, 9000.0000, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[TransactionVehicles] OFF
INSERT [dbo].[Users] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [LastUpdatedBy], [LastUpdated], [CreatedDate], [FirstName], [LastName], [MiddleName], [CivilStatus], [Gender], [BirthDate], [AddressLine2], [AddressLine1], [TownCity], [Cellular], [Height], [Weight], [Religion], [Citizenship], [Languages]) VALUES (N'b97f5a28-5900-4787-bf54-7f4029796a6d', N'admin@admin.com', NULL, N'ALrUzLJwaXZIChjzCQs3oz53rrt891ANtFecBA9z4vnk44tmlmQj7I+drinE/+LJsA==', N'8165128f-1f04-4474-9a72-8f5b231a357c', NULL, NULL, NULL, NULL, NULL, NULL, N'admin@admin.com', NULL, NULL, NULL, N'Mark Christopher ', N'Cacal', N'Ramos', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[UsersInControllers] ([ControllerId], [UserId]) VALUES (2, N'b97f5a28-5900-4787-bf54-7f4029796a6d')
INSERT [dbo].[UsersInControllers] ([ControllerId], [UserId]) VALUES (5, N'b97f5a28-5900-4787-bf54-7f4029796a6d')
INSERT [dbo].[UsersInControllers] ([ControllerId], [UserId]) VALUES (6, N'b97f5a28-5900-4787-bf54-7f4029796a6d')
INSERT [dbo].[UsersInControllers] ([ControllerId], [UserId]) VALUES (8, N'b97f5a28-5900-4787-bf54-7f4029796a6d')
INSERT [dbo].[UsersInControllers] ([ControllerId], [UserId]) VALUES (9, N'b97f5a28-5900-4787-bf54-7f4029796a6d')
INSERT [dbo].[UsersInControllers] ([ControllerId], [UserId]) VALUES (10, N'b97f5a28-5900-4787-bf54-7f4029796a6d')
INSERT [dbo].[UsersInControllers] ([ControllerId], [UserId]) VALUES (11, N'b97f5a28-5900-4787-bf54-7f4029796a6d')
INSERT [dbo].[UsersInControllers] ([ControllerId], [UserId]) VALUES (12, N'b97f5a28-5900-4787-bf54-7f4029796a6d')
INSERT [dbo].[UsersInControllers] ([ControllerId], [UserId]) VALUES (13, N'b97f5a28-5900-4787-bf54-7f4029796a6d')
INSERT [dbo].[UsersInControllers] ([ControllerId], [UserId]) VALUES (14, N'b97f5a28-5900-4787-bf54-7f4029796a6d')
SET IDENTITY_INSERT [dbo].[Vehicles] ON 

INSERT [dbo].[Vehicles] ([Id], [PermiteeId], [VehicleTypeId], [VehicleName], [PlateNo]) VALUES (2, 4, 1, N'Vehicle Name', N'Plate No')
SET IDENTITY_INSERT [dbo].[Vehicles] OFF
SET IDENTITY_INSERT [dbo].[VehicleTypes] ON 

INSERT [dbo].[VehicleTypes] ([Id], [VehicleTypeName]) VALUES (1, N'1')
SET IDENTITY_INSERT [dbo].[VehicleTypes] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_FK_UserClaims_Users]    Script Date: 5/16/2019 9:35:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_UserClaims_Users] ON [dbo].[UserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_FK_UserLogins_Users]    Script Date: 5/16/2019 9:35:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_UserLogins_Users] ON [dbo].[UserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_FK_UsersInRoles_Users]    Script Date: 5/16/2019 9:35:01 PM ******/
CREATE NONCLUSTERED INDEX [IX_FK_UsersInRoles_Users] ON [dbo].[UsersInRoles]
(
	[Users_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Facilities]  WITH CHECK ADD  CONSTRAINT [FK_Facilities_Vehicles] FOREIGN KEY([PermiteeId])
REFERENCES [dbo].[Vehicles] ([Id])
GO
ALTER TABLE [dbo].[Facilities] CHECK CONSTRAINT [FK_Facilities_Vehicles]
GO
ALTER TABLE [dbo].[Permitees]  WITH CHECK ADD  CONSTRAINT [FK_Permitees_PermiteeTypes] FOREIGN KEY([PermiteeTypeId])
REFERENCES [dbo].[PermiteeTypes] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Permitees] CHECK CONSTRAINT [FK_Permitees_PermiteeTypes]
GO
ALTER TABLE [dbo].[QuariesInPermitees]  WITH CHECK ADD  CONSTRAINT [FK_QuariesInPermitees_Quarries] FOREIGN KEY([QuarrieId])
REFERENCES [dbo].[Quarries] ([Id])
GO
ALTER TABLE [dbo].[QuariesInPermitees] CHECK CONSTRAINT [FK_QuariesInPermitees_Quarries]
GO
ALTER TABLE [dbo].[QuariesInPermitees]  WITH CHECK ADD  CONSTRAINT [FK_QuariesInPermites_Permitees] FOREIGN KEY([PermiteeId])
REFERENCES [dbo].[Permitees] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuariesInPermitees] CHECK CONSTRAINT [FK_QuariesInPermites_Permitees]
GO
ALTER TABLE [dbo].[TransactionDetails]  WITH NOCHECK ADD  CONSTRAINT [FK_TransactionDetails_Items] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Items] ([Id])
ON DELETE CASCADE
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[TransactionDetails] NOCHECK CONSTRAINT [FK_TransactionDetails_Items]
GO
ALTER TABLE [dbo].[TransactionDetails]  WITH NOCHECK ADD  CONSTRAINT [FK_TransactionDetails_Transactions] FOREIGN KEY([TransactionId])
REFERENCES [dbo].[Transactions] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[TransactionDetails] NOCHECK CONSTRAINT [FK_TransactionDetails_Transactions]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Permittees] FOREIGN KEY([PermiteeId])
REFERENCES [dbo].[Permitees] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Permittees]
GO
ALTER TABLE [dbo].[TransactionVehicles]  WITH NOCHECK ADD  CONSTRAINT [FK_TransactionVehicles_Transactions] FOREIGN KEY([TransactionId])
REFERENCES [dbo].[Transactions] ([Id])
ON DELETE CASCADE
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[TransactionVehicles] NOCHECK CONSTRAINT [FK_TransactionVehicles_Transactions]
GO
ALTER TABLE [dbo].[TransactionVehicles]  WITH CHECK ADD  CONSTRAINT [FK_TransactionVehicles_Vehicles] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicles] ([Id])
GO
ALTER TABLE [dbo].[TransactionVehicles] CHECK CONSTRAINT [FK_TransactionVehicles_Vehicles]
GO
ALTER TABLE [dbo].[UserClaims]  WITH CHECK ADD  CONSTRAINT [FK_UserClaims_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserClaims] CHECK CONSTRAINT [FK_UserClaims_Users]
GO
ALTER TABLE [dbo].[UserLogins]  WITH CHECK ADD  CONSTRAINT [FK_UserLogins_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserLogins] CHECK CONSTRAINT [FK_UserLogins_Users]
GO
ALTER TABLE [dbo].[UsersInControllers]  WITH CHECK ADD  CONSTRAINT [FK_UsersInControllers_Controllers] FOREIGN KEY([ControllerId])
REFERENCES [dbo].[ControllersActions] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersInControllers] CHECK CONSTRAINT [FK_UsersInControllers_Controllers]
GO
ALTER TABLE [dbo].[UsersInControllers]  WITH CHECK ADD  CONSTRAINT [FK_UsersInControllers_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersInControllers] CHECK CONSTRAINT [FK_UsersInControllers_Users]
GO
ALTER TABLE [dbo].[UsersInRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersInRoles_UserRoles] FOREIGN KEY([UserRoles_Id])
REFERENCES [dbo].[UserRoles] ([Id])
GO
ALTER TABLE [dbo].[UsersInRoles] CHECK CONSTRAINT [FK_UsersInRoles_UserRoles]
GO
ALTER TABLE [dbo].[UsersInRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersInRoles_Users] FOREIGN KEY([Users_Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UsersInRoles] CHECK CONSTRAINT [FK_UsersInRoles_Users]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_Permittees] FOREIGN KEY([PermiteeId])
REFERENCES [dbo].[Permitees] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_Permittees]
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_VehicleTypes] FOREIGN KEY([VehicleTypeId])
REFERENCES [dbo].[VehicleTypes] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_VehicleTypes]
GO
USE [master]
GO
ALTER DATABASE [quary] SET  READ_WRITE 
GO
