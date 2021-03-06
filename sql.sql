CREATE DATABASE Turbo_Az
GO
USE Turbo_Az
GO
CREATE TABLE [dbo].[Cars](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Marka_id] [int] NOT NULL,
	[Model_id] [int] NOT NULL,
	[Year] [date] NOT NULL,
	[Price] [float] NOT NULL,
(
CREATE TABLE [dbo].[Markas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
(
CREATE TABLE [dbo].[Models](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Marka_id] [int] NOT NULL,
(
GO
ALTER TABLE [dbo].[Cars]  WITH CHECK ADD FOREIGN KEY([Marka_id])
REFERENCES [dbo].[Markas] ([Id])
GO
ALTER TABLE [dbo].[Cars]  WITH CHECK ADD FOREIGN KEY([Model_id])
REFERENCES [dbo].[Models] ([Id])
GO
ALTER TABLE [dbo].[Models]  WITH CHECK ADD FOREIGN KEY([Marka_id])
REFERENCES [dbo].[Markas] ([Id])
GO
