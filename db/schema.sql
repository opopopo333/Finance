USE [master]
GO
/****** Object:  Database [DB_Ulianenko_Finance]    Script Date: 20.10.2026 13:22:59 ******/
CREATE DATABASE [DB_Ulianenko_Finance]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DB_Ulianenko_Finance', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL17.SQLEXPRESS\MSSQL\DATA\DB_Ulianenko_Finance.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DB_Ulianenko_Finance_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL17.SQLEXPRESS\MSSQL\DATA\DB_Ulianenko_Finance_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET COMPATIBILITY_LEVEL = 170
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DB_Ulianenko_Finance].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET ARITHABORT OFF 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET  MULTI_USER 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET OPTIMIZED_LOCKING = OFF 
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET QUERY_STORE = ON
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DB_Ulianenko_Finance]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 20.10.2026 13:22:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Type] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operation]    Script Date: 20.10.2026 13:22:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[CategoryID] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Amount] [decimal](12, 2) NOT NULL,
	[Comment] [nvarchar](500) NULL,
 CONSTRAINT [PK_Operation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 20.10.2026 13:22:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](64) NOT NULL,
	[Role] [nvarchar](10) NOT NULL,
	[FIO] [nvarchar](150) NOT NULL,
	[Photo] [nvarchar](255) NULL,
	[Theme] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([ID], [Name], [Type]) VALUES (1, N'Зарплата', N'Доход')
INSERT [dbo].[Category] ([ID], [Name], [Type]) VALUES (2, N'Подработка', N'Доход')
INSERT [dbo].[Category] ([ID], [Name], [Type]) VALUES (3, N'Кэшбэк и проценты', N'Доход')
INSERT [dbo].[Category] ([ID], [Name], [Type]) VALUES (4, N'Продукты', N'Расход')
INSERT [dbo].[Category] ([ID], [Name], [Type]) VALUES (5, N'Транспорт', N'Расход')
INSERT [dbo].[Category] ([ID], [Name], [Type]) VALUES (6, N'ЖКУ и связь', N'Расход')
INSERT [dbo].[Category] ([ID], [Name], [Type]) VALUES (7, N'Развлечения', N'Расход')
INSERT [dbo].[Category] ([ID], [Name], [Type]) VALUES (8, N'Здоровье', N'Расход')
INSERT [dbo].[Category] ([ID], [Name], [Type]) VALUES (9, N'Одежда', N'Расход')
INSERT [dbo].[Category] ([ID], [Name], [Type]) VALUES (10, N'Подписки', N'Расход')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Operation] ON 

INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (1, 3, 1, CAST(N'2026-08-05' AS Date), N'Зарплата за месяц', CAST(67000.00 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (2, 4, 1, CAST(N'2026-08-05' AS Date), N'Зарплата за месяц', CAST(70000.00 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (3, 5, 1, CAST(N'2026-08-05' AS Date), N'Зарплата за месяц', CAST(81500.00 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (4, 3, 8, CAST(N'2026-08-07' AS Date), N'Спортзал', CAST(3665.29 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (5, 4, 5, CAST(N'2026-08-07' AS Date), N'Пополнение Тройки', CAST(1025.05 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (6, 5, 9, CAST(N'2026-08-07' AS Date), N'Куртка', CAST(2646.29 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (7, 3, 4, CAST(N'2026-08-08' AS Date), N'Пятёрочка', CAST(1315.16 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (8, 3, 4, CAST(N'2026-08-08' AS Date), N'Пятёрочка', CAST(2471.96 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (9, 4, 8, CAST(N'2026-08-08' AS Date), N'Аптека', CAST(3517.69 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (10, 3, 6, CAST(N'2026-08-10' AS Date), N'Коммунальные услуги', CAST(4550.00 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (11, 4, 6, CAST(N'2026-08-10' AS Date), N'Коммунальные услуги', CAST(5650.00 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (12, 5, 6, CAST(N'2026-08-10' AS Date), N'Коммунальные услуги', CAST(4850.00 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (13, 5, 10, CAST(N'2026-08-10' AS Date), N'Онлайн-кинотеатр', CAST(788.17 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (14, 4, 7, CAST(N'2026-08-13' AS Date), N'Кафе с друзьями', CAST(1298.81 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (15, 3, 9, CAST(N'2026-08-14' AS Date), N'Куртка', CAST(1738.37 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (16, 5, 4, CAST(N'2026-08-14' AS Date), N'Пятёрочка', CAST(1048.45 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (17, 5, 4, CAST(N'2026-08-17' AS Date), N'Пятёрочка', CAST(3524.05 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (18, 4, 4, CAST(N'2026-08-18' AS Date), N'Перекрёсток', CAST(3389.42 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (19, 4, 10, CAST(N'2026-08-21' AS Date), N'Онлайн-кинотеатр', CAST(248.98 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (20, 3, 10, CAST(N'2026-08-24' AS Date), N'Яндекс Плюс', CAST(823.53 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (21, 5, 8, CAST(N'2026-08-25' AS Date), N'Аптека', CAST(4635.44 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (22, 5, 4, CAST(N'2026-09-02' AS Date), N'Пятёрочка', CAST(1387.57 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (23, 3, 3, CAST(N'2026-09-03' AS Date), N'Проценты по вкладу', CAST(1042.83 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (24, 5, 2, CAST(N'2026-09-04' AS Date), N'Репетиторство', CAST(14149.18 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (25, 3, 1, CAST(N'2026-09-05' AS Date), N'Зарплата за месяц', CAST(94500.00 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (26, 4, 1, CAST(N'2026-09-05' AS Date), N'Зарплата за месяц', CAST(74500.00 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (27, 5, 1, CAST(N'2026-09-05' AS Date), N'Зарплата за месяц', CAST(80500.00 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (28, 3, 4, CAST(N'2026-09-07' AS Date), N'Магнит', CAST(4324.57 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (29, 4, 3, CAST(N'2026-09-07' AS Date), N'Проценты по вкладу', CAST(2295.87 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (30, 4, 4, CAST(N'2026-09-09' AS Date), N'Магнит', CAST(972.48 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (31, 3, 6, CAST(N'2026-09-10' AS Date), N'Коммунальные услуги', CAST(5800.00 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (32, 4, 6, CAST(N'2026-09-10' AS Date), N'Коммунальные услуги', CAST(7100.00 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (33, 5, 6, CAST(N'2026-09-10' AS Date), N'Коммунальные услуги', CAST(6050.00 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (34, 4, 9, CAST(N'2026-09-14' AS Date), N'Кроссовки', CAST(8233.67 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (35, 3, 4, CAST(N'2026-09-15' AS Date), N'Пятёрочка', CAST(2815.99 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (36, 5, 7, CAST(N'2026-09-18' AS Date), N'Кафе с друзьями', CAST(910.97 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (37, 3, 5, CAST(N'2026-09-20' AS Date), N'Метро', CAST(440.91 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (38, 4, 4, CAST(N'2026-09-21' AS Date), N'Рынок', CAST(2446.85 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (39, 3, 4, CAST(N'2026-09-23' AS Date), N'Магнит', CAST(2132.72 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (40, 5, 3, CAST(N'2026-09-24' AS Date), N'Кэшбэк по карте', CAST(1292.08 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (41, 5, 8, CAST(N'2026-09-25' AS Date), N'Спортзал', CAST(891.07 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (42, 4, 2, CAST(N'2026-09-26' AS Date), N'Репетиторство', CAST(6785.44 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (43, 5, 5, CAST(N'2026-10-01' AS Date), N'Метро', CAST(907.16 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (44, 3, 3, CAST(N'2026-10-02' AS Date), N'Проценты по вкладу', CAST(1703.97 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (45, 4, 9, CAST(N'2026-10-03' AS Date), N'Футболки', CAST(4385.75 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (46, 4, 2, CAST(N'2026-10-04' AS Date), N'Репетиторство', CAST(11180.52 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (47, 5, 7, CAST(N'2026-10-04' AS Date), N'Кино', CAST(1217.25 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (48, 1, 1, CAST(N'2026-10-05' AS Date), N'Зарплата за месяц', CAST(90000.00 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (49, 2, 1, CAST(N'2026-10-05' AS Date), N'Зарплата за месяц', CAST(90000.00 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (50, 3, 1, CAST(N'2026-10-05' AS Date), N'Зарплата за месяц', CAST(89000.00 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (51, 4, 1, CAST(N'2026-10-05' AS Date), N'Зарплата за месяц', CAST(83000.00 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (52, 4, 3, CAST(N'2026-10-05' AS Date), N'Проценты по вкладу', CAST(1624.50 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (53, 5, 1, CAST(N'2026-10-05' AS Date), N'Зарплата за месяц', CAST(87000.00 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (54, 1, 4, CAST(N'2026-10-07' AS Date), N'Перекрёсток', CAST(2350.40 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (55, 2, 4, CAST(N'2026-10-07' AS Date), N'Перекрёсток', CAST(2350.40 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (56, 5, 8, CAST(N'2026-10-07' AS Date), N'Стоматолог', CAST(3650.86 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (57, 3, 3, CAST(N'2026-10-08' AS Date), N'Проценты по вкладу', CAST(2186.24 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (58, 5, 9, CAST(N'2026-10-08' AS Date), N'Футболки', CAST(1553.93 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (59, 3, 6, CAST(N'2026-10-10' AS Date), N'Коммунальные услуги', CAST(6200.00 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (60, 4, 6, CAST(N'2026-10-10' AS Date), N'Коммунальные услуги', CAST(5200.00 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (61, 5, 6, CAST(N'2026-10-10' AS Date), N'Коммунальные услуги', CAST(5150.00 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (62, 3, 10, CAST(N'2026-10-12' AS Date), N'Онлайн-кинотеатр', CAST(603.15 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (63, 5, 10, CAST(N'2026-10-12' AS Date), N'Онлайн-кинотеатр', CAST(505.67 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (64, 3, 3, CAST(N'2026-10-13' AS Date), N'Проценты по вкладу', CAST(335.18 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (65, 3, 4, CAST(N'2026-10-15' AS Date), N'Магнит', CAST(3006.31 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (66, 4, 4, CAST(N'2026-10-17' AS Date), N'Пятёрочка', CAST(2423.41 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (67, 4, 7, CAST(N'2026-10-17' AS Date), N'Кафе с друзьями', CAST(1230.23 AS Decimal(12, 2)), NULL)
INSERT [dbo].[Operation] ([ID], [UserID], [CategoryID], [Date], [Name], [Amount], [Comment]) VALUES (68, 3, 3, CAST(N'2026-10-20' AS Date), N'fasdf', CAST(2222.00 AS Decimal(12, 2)), N'sfd')
SET IDENTITY_INSERT [dbo].[Operation] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [Login], [Password], [Role], [FIO], [Photo], [Theme]) VALUES (1, N'admin', N'3B612C75A7B5048A435FB6EC81E52FF92D6D795A8B5A9C17070F6A63C97A53B2', N'Admin', N'Гуцалюк Алексей Сергеевич', NULL, N'Dark')
INSERT [dbo].[Users] ([ID], [Login], [Password], [Role], [FIO], [Photo], [Theme]) VALUES (2, N'manager', N'67E06322C104F451A8CE3D2E1A40DB0C0B91DDB4CD8C5FDACE6425ED6E624A97', N'Admin', N'Смирнова Ольга Викторовна', NULL, N'Light')
INSERT [dbo].[Users] ([ID], [Login], [Password], [Role], [FIO], [Photo], [Theme]) VALUES (3, N'ivanov', N'2E7BBFC7A6F2DC3B78F9E86947B951D00CA90CAD36C68E88730B9A6014EC378C', N'User', N'Иванов Иван Иванович', NULL, N'Light')
INSERT [dbo].[Users] ([ID], [Login], [Password], [Role], [FIO], [Photo], [Theme]) VALUES (4, N'petrova', N'D917A87622B5222DFBAA972B779E6D25910B0E6F40BC4C943D2AC64787AF34B4', N'User', N'Петрова Анна Михайловна', NULL, N'Light')
INSERT [dbo].[Users] ([ID], [Login], [Password], [Role], [FIO], [Photo], [Theme]) VALUES (5, N'sidorov', N'181483CBA59DDB72656707B73A8327A98CE31725748084331571D6137E3CCB5C', N'User', N'Сидоров Пётр Андреевич', NULL, N'Light')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Category_Name]    Script Date: 20.10.2026 13:22:59 ******/
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [UQ_Category_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Operation_CategoryID]    Script Date: 20.10.2026 13:22:59 ******/
CREATE NONCLUSTERED INDEX [IX_Operation_CategoryID] ON [dbo].[Operation]
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Operation_Date]    Script Date: 20.10.2026 13:22:59 ******/
CREATE NONCLUSTERED INDEX [IX_Operation_Date] ON [dbo].[Operation]
(
	[Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Operation_UserID]    Script Date: 20.10.2026 13:22:59 ******/
CREATE NONCLUSTERED INDEX [IX_Operation_UserID] ON [dbo].[Operation]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Users_Login]    Script Date: 20.10.2026 13:22:59 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [UQ_Users_Login] UNIQUE NONCLUSTERED 
(
	[Login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Theme]  DEFAULT (N'Light') FOR [Theme]
GO
ALTER TABLE [dbo].[Operation]  WITH CHECK ADD  CONSTRAINT [FK_Operation_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Operation] CHECK CONSTRAINT [FK_Operation_Category]
GO
ALTER TABLE [dbo].[Operation]  WITH CHECK ADD  CONSTRAINT [FK_Operation_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Operation] CHECK CONSTRAINT [FK_Operation_Users]
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [CK_Category_Type] CHECK  (([Type]=N'Расход' OR [Type]=N'Доход'))
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [CK_Category_Type]
GO
ALTER TABLE [dbo].[Operation]  WITH CHECK ADD  CONSTRAINT [CK_Operation_Amount] CHECK  (([Amount]>(0) AND [Amount]<=(10000000)))
GO
ALTER TABLE [dbo].[Operation] CHECK CONSTRAINT [CK_Operation_Amount]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [CK_Users_Role] CHECK  (([Role]=N'User' OR [Role]=N'Admin'))
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [CK_Users_Role]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [CK_Users_Theme] CHECK  (([Theme]=N'Dark' OR [Theme]=N'Light'))
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [CK_Users_Theme]
GO
USE [master]
GO
ALTER DATABASE [DB_Ulianenko_Finance] SET  READ_WRITE 
GO
