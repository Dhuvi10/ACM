USE [master]
GO
/****** Object:  Database [ACM]    Script Date: 7/4/2018 10:46:32 PM ******/
CREATE DATABASE [ACM]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ACM', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\ACM.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ACM_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\ACM_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ACM] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ACM].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ACM] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ACM] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ACM] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ACM] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ACM] SET ARITHABORT OFF 
GO
ALTER DATABASE [ACM] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ACM] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ACM] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ACM] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ACM] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ACM] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ACM] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ACM] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ACM] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ACM] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ACM] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ACM] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ACM] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ACM] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ACM] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ACM] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ACM] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ACM] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ACM] SET  MULTI_USER 
GO
ALTER DATABASE [ACM] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ACM] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ACM] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ACM] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ACM] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ACM] SET QUERY_STORE = OFF
GO
USE [ACM]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
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
USE [ACM]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 7/4/2018 10:46:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 7/4/2018 10:46:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 7/4/2018 10:46:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 7/4/2018 10:46:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 7/4/2018 10:46:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 7/4/2018 10:46:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[DOB] [datetime2](7) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[AdminId] [nvarchar](100) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 7/4/2018 10:46:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CheckInForm]    Script Date: 7/4/2018 10:46:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CheckInForm](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](15) NULL,
	[EmailAddress] [nvarchar](50) NULL,
	[Year] [nvarchar](10) NULL,
	[Make] [nvarchar](50) NULL,
	[Models] [nvarchar](50) NULL,
	[Vin] [nvarchar](50) NULL,
	[OdoMeter] [nvarchar](50) NULL,
	[SummeryOfTaskCompleted] [nvarchar](max) NULL,
	[PartsNeeded] [nvarchar](max) NULL,
	[CustomerPartSupplied] [nvarchar](max) NULL,
	[PersonalItemInVehicle] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[IsActive] [bit] NULL,
	[StoreId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_CheckInForm] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gallery]    Script Date: 7/4/2018 10:46:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gallery](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StoreId] [nvarchar](450) NOT NULL,
	[Image] [nvarchar](100) NULL,
	[IsActive] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[IsMain] [bit] NULL,
	[ThumbnailImage] [nvarchar](100) NULL,
	[CheckInId] [bigint] NOT NULL,
 CONSTRAINT [PK_Gallery] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhotoComment]    Script Date: 7/4/2018 10:46:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhotoComment](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[PhotoId] [bigint] NULL,
	[Comment] [nvarchar](200) NULL,
	[CreatedOn] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_PhotoComment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProfileInfo]    Script Date: 7/4/2018 10:46:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfileInfo](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CheckInId] [bigint] NOT NULL,
	[Signature] [nvarchar](200) NULL,
	[Photo] [nvarchar](200) NULL,
	[CreatedOn] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_ProfileInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoreContracts]    Script Date: 7/4/2018 10:46:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreContracts](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StoreId] [nvarchar](450) NOT NULL,
	[CheckInContract] [nvarchar](max) NULL,
	[CheckOutContract] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_StoreContracts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoreInfo]    Script Date: 7/4/2018 10:46:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreInfo](
	[LogoId] [bigint] IDENTITY(1,1) NOT NULL,
	[Logo] [nvarchar](100) NULL,
	[StoreId] [nvarchar](450) NULL,
 CONSTRAINT [PK_StoreInfo] PRIMARY KEY CLUSTERED 
(
	[LogoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT ('0001-01-01T00:00:00.000') FOR [DOB]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Gallery]  WITH CHECK ADD  CONSTRAINT [FK_Gallery_AspNetUsers] FOREIGN KEY([StoreId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Gallery] CHECK CONSTRAINT [FK_Gallery_AspNetUsers]
GO
ALTER TABLE [dbo].[Gallery]  WITH CHECK ADD  CONSTRAINT [FK_Gallery_CheckInForm] FOREIGN KEY([CheckInId])
REFERENCES [dbo].[CheckInForm] ([Id])
GO
ALTER TABLE [dbo].[Gallery] CHECK CONSTRAINT [FK_Gallery_CheckInForm]
GO
ALTER TABLE [dbo].[PhotoComment]  WITH CHECK ADD  CONSTRAINT [FK_PhotoComment_Gallery] FOREIGN KEY([PhotoId])
REFERENCES [dbo].[Gallery] ([Id])
GO
ALTER TABLE [dbo].[PhotoComment] CHECK CONSTRAINT [FK_PhotoComment_Gallery]
GO
ALTER TABLE [dbo].[ProfileInfo]  WITH CHECK ADD  CONSTRAINT [FK_ProfileInfo_CheckInForm] FOREIGN KEY([CheckInId])
REFERENCES [dbo].[CheckInForm] ([Id])
GO
ALTER TABLE [dbo].[ProfileInfo] CHECK CONSTRAINT [FK_ProfileInfo_CheckInForm]
GO
ALTER TABLE [dbo].[StoreContracts]  WITH CHECK ADD  CONSTRAINT [FK_StoreContracts_AspNetUsers] FOREIGN KEY([StoreId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[StoreContracts] CHECK CONSTRAINT [FK_StoreContracts_AspNetUsers]
GO
ALTER TABLE [dbo].[StoreInfo]  WITH CHECK ADD  CONSTRAINT [FK_StoreInfo_AspNetUsers] FOREIGN KEY([StoreId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[StoreInfo] CHECK CONSTRAINT [FK_StoreInfo_AspNetUsers]
GO
USE [master]
GO
ALTER DATABASE [ACM] SET  READ_WRITE 
GO
