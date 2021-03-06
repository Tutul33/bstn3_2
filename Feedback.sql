USE [master]
GO
/****** Object:  Database [FeedBack]    Script Date: 05/16/2020 2:27:06 AM ******/
CREATE DATABASE [FeedBack]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FeedBack', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.TUTUL\MSSQL\DATA\FeedBack.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FeedBack_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.TUTUL\MSSQL\DATA\FeedBack_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [FeedBack] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FeedBack].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FeedBack] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FeedBack] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FeedBack] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FeedBack] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FeedBack] SET ARITHABORT OFF 
GO
ALTER DATABASE [FeedBack] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FeedBack] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FeedBack] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FeedBack] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FeedBack] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FeedBack] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FeedBack] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FeedBack] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FeedBack] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FeedBack] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FeedBack] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FeedBack] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FeedBack] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FeedBack] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FeedBack] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FeedBack] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FeedBack] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FeedBack] SET RECOVERY FULL 
GO
ALTER DATABASE [FeedBack] SET  MULTI_USER 
GO
ALTER DATABASE [FeedBack] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FeedBack] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FeedBack] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FeedBack] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FeedBack] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'FeedBack', N'ON'
GO
ALTER DATABASE [FeedBack] SET QUERY_STORE = OFF
GO
USE [FeedBack]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [FeedBack]
GO
/****** Object:  UserDefinedFunction [dbo].[funcGetComment]    Script Date: 05/16/2020 2:27:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select  dbo.funcGetCloudDeployment(880, 'Terminated', 'Error', 18)
CREATE FUNCTION [dbo].[funcGetComment]( @postId int=0)
RETURNS NVARCHAR(MAX)  
AS  
BEGIN
  RETURN (
		SELECT ISNULL(C.CommentId, 0) commentId,
		       ISNULL(C.CommentText, 0) commentText
			  ,ISNULL(C.CreationTime, '') creationTime
			  ,ISNULL(C.cLike, '') cLike
			  ,ISNULL(C.cDislike, 0) cDislike,
			   ISNULL(C.PostId, 0) postId,
			     ISNULL(C.UserId, 0) userId
				  ,ISNULL(u.UserName, '') userName
				  ,ISNULL(u.UserType, '') userType


		  FROM Comment C
		  LEFT JOIN [User] u ON u.UserId=c.UserId
		  Where C.PostId=@postId 
		 
		  ORDER BY C.CommentId DESC
    FOR JSON AUTO, Include_Null_Values)
END
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 05/16/2020 2:27:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[CommentText] [nvarchar](max) NULL,
	[PostId] [int] NOT NULL,
	[CreationTime] [datetime] NULL,
	[cLike] [int] NULL,
	[cDislike] [int] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OpinionLog]    Script Date: 05/16/2020 2:27:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OpinionLog](
	[OpinionLogId] [int] IDENTITY(1,1) NOT NULL,
	[CommentId] [int] NULL,
	[IsLike] [bit] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_OpinionLog] PRIMARY KEY CLUSTERED 
(
	[OpinionLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 05/16/2020 2:27:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[PostId] [int] IDENTITY(1,1) NOT NULL,
	[PostText] [nvarchar](max) NULL,
	[CreationTime] [datetime] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 05/16/2020 2:27:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](300) NULL,
	[UserType] [nvarchar](300) NULL,
	[Password] [nvarchar](20) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Comment] ON 

INSERT [dbo].[Comment] ([CommentId], [CommentText], [PostId], [CreationTime], [cLike], [cDislike], [UserId]) VALUES (1, N'Comment Created by user1', 1, CAST(N'2020-05-16T01:09:33.260' AS DateTime), 0, 1, 2)
INSERT [dbo].[Comment] ([CommentId], [CommentText], [PostId], [CreationTime], [cLike], [cDislike], [UserId]) VALUES (2, N'Comment 2 Created by user1', 1, CAST(N'2020-05-16T01:09:41.473' AS DateTime), 1, 1, 2)
INSERT [dbo].[Comment] ([CommentId], [CommentText], [PostId], [CreationTime], [cLike], [cDislike], [UserId]) VALUES (3, N'New my first comment', 2, CAST(N'2020-05-16T01:26:29.713' AS DateTime), 1, 0, 4)
INSERT [dbo].[Comment] ([CommentId], [CommentText], [PostId], [CreationTime], [cLike], [cDislike], [UserId]) VALUES (4, N'New comment', 6, CAST(N'2020-05-16T01:28:55.093' AS DateTime), 1, 0, 4)
INSERT [dbo].[Comment] ([CommentId], [CommentText], [PostId], [CreationTime], [cLike], [cDislike], [UserId]) VALUES (5, N'New Comment 3', 5, CAST(N'2020-05-16T01:29:08.857' AS DateTime), NULL, NULL, 4)
INSERT [dbo].[Comment] ([CommentId], [CommentText], [PostId], [CreationTime], [cLike], [cDislike], [UserId]) VALUES (6, N'New Comment 4', 4, CAST(N'2020-05-16T01:29:19.950' AS DateTime), NULL, NULL, 4)
INSERT [dbo].[Comment] ([CommentId], [CommentText], [PostId], [CreationTime], [cLike], [cDislike], [UserId]) VALUES (7, N'New Comment 5', 3, CAST(N'2020-05-16T01:29:31.567' AS DateTime), NULL, NULL, 4)
INSERT [dbo].[Comment] ([CommentId], [CommentText], [PostId], [CreationTime], [cLike], [cDislike], [UserId]) VALUES (8, N'New Post', 7, CAST(N'2020-05-16T01:30:08.030' AS DateTime), 0, 1, 4)
INSERT [dbo].[Comment] ([CommentId], [CommentText], [PostId], [CreationTime], [cLike], [cDislike], [UserId]) VALUES (9, N'New Comment', 8, CAST(N'2020-05-16T02:04:57.207' AS DateTime), 1, 0, 1)
SET IDENTITY_INSERT [dbo].[Comment] OFF
SET IDENTITY_INSERT [dbo].[OpinionLog] ON 

INSERT [dbo].[OpinionLog] ([OpinionLogId], [CommentId], [IsLike], [UserId]) VALUES (1, 2, 1, 2)
INSERT [dbo].[OpinionLog] ([OpinionLogId], [CommentId], [IsLike], [UserId]) VALUES (2, 2, 0, 4)
INSERT [dbo].[OpinionLog] ([OpinionLogId], [CommentId], [IsLike], [UserId]) VALUES (3, 1, 0, 4)
INSERT [dbo].[OpinionLog] ([OpinionLogId], [CommentId], [IsLike], [UserId]) VALUES (4, 3, 1, 4)
INSERT [dbo].[OpinionLog] ([OpinionLogId], [CommentId], [IsLike], [UserId]) VALUES (5, 8, 0, 4)
INSERT [dbo].[OpinionLog] ([OpinionLogId], [CommentId], [IsLike], [UserId]) VALUES (6, 4, 1, 4)
INSERT [dbo].[OpinionLog] ([OpinionLogId], [CommentId], [IsLike], [UserId]) VALUES (7, 9, 1, 1)
SET IDENTITY_INSERT [dbo].[OpinionLog] OFF
SET IDENTITY_INSERT [dbo].[Post] ON 

INSERT [dbo].[Post] ([PostId], [PostText], [CreationTime], [UserId]) VALUES (1, N'Post 1', CAST(N'2020-05-16T01:09:26.603' AS DateTime), 2)
INSERT [dbo].[Post] ([PostId], [PostText], [CreationTime], [UserId]) VALUES (2, N'Post 2', CAST(N'2020-05-16T01:26:10.583' AS DateTime), 4)
INSERT [dbo].[Post] ([PostId], [PostText], [CreationTime], [UserId]) VALUES (3, N'Post 3', CAST(N'2020-05-16T01:27:44.573' AS DateTime), 4)
INSERT [dbo].[Post] ([PostId], [PostText], [CreationTime], [UserId]) VALUES (4, N'Post 4', CAST(N'2020-05-16T01:28:13.037' AS DateTime), 4)
INSERT [dbo].[Post] ([PostId], [PostText], [CreationTime], [UserId]) VALUES (5, N'Post 5', CAST(N'2020-05-16T01:28:24.653' AS DateTime), 4)
INSERT [dbo].[Post] ([PostId], [PostText], [CreationTime], [UserId]) VALUES (6, N'Post 6', CAST(N'2020-05-16T01:28:39.487' AS DateTime), 4)
INSERT [dbo].[Post] ([PostId], [PostText], [CreationTime], [UserId]) VALUES (7, N'Post 7', CAST(N'2020-05-16T01:29:56.143' AS DateTime), 4)
INSERT [dbo].[Post] ([PostId], [PostText], [CreationTime], [UserId]) VALUES (8, N'Post 9', CAST(N'2020-05-16T02:04:48.687' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Post] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [UserName], [UserType], [Password]) VALUES (1, N'User1', N'Admin', N'12345')
INSERT [dbo].[User] ([UserId], [UserName], [UserType], [Password]) VALUES (2, N'User2', N'Admin', N'12345')
INSERT [dbo].[User] ([UserId], [UserName], [UserType], [Password]) VALUES (3, N'User3', N'Admin', N'12345')
INSERT [dbo].[User] ([UserId], [UserName], [UserType], [Password]) VALUES (4, N'Tutul', N'Admin', N'12345')
INSERT [dbo].[User] ([UserId], [UserName], [UserType], [Password]) VALUES (5, N'New User', N'Admin', N'12345')
INSERT [dbo].[User] ([UserId], [UserName], [UserType], [Password]) VALUES (6, N'New 6', N'Admin', N'12345')
INSERT [dbo].[User] ([UserId], [UserName], [UserType], [Password]) VALUES (7, N'New 7', N'Admin', N'12345')
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Post] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Post]
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_User]
GO
/****** Object:  StoredProcedure [dbo].[SpGetPostData]    Script Date: 05/16/2020 2:27:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Author,,Tutul Chakma>
-- Create date: <2020-05-14 00:00:00.000,,>
-- Description:	<Description,,>
-- =============================================

--EXEC SpGetPostData  1,50
CREATE PROCEDURE [dbo].[SpGetPostData]
(		 
	@pageNumber			INT=0
	,@pageSize		    INT=0
	,@search            nvarchar(max)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	
	BEGIN TRY
		BEGIN

		DECLARE @Today DateTime, @result nvarchar(MAX), @recordsTotal int=0;
			DECLARE @SkipRow INT=0;
			SET @SkipRow = ((CASE WHEN @pageNumber=0 THEN 1 ELSE @pageNumber END)-1)*@pageSize
			BEGIN
				

				SET @recordsTotal=( SELECT COUNT(p.PostId)FROM [dbo].[POST] p
				where p.PostText LIKE ''+@search+'%'
				)
					
				---=================Paging------------------------------------					 
				SET @result=(ISNULL((
				 SELECT  
				         ISNULL(p.PostId,0) postId,
						 ISNULL(p.[postText],'') postText,
                         ISNULL(p.[creationTime],'') creationTime,	
						ISNULL(JSON_QUERY(dbo.funcGetComment(p.PostId)), '[]') AS commentList
						,
						ISNULL(@recordsTotal,0) recordsTotal
						
						FROM [dbo].[Post] p
						where p.PostText LIKE ''+@search+'%'
						
						
					ORDER BY p.PostId DESC		
				---=================Paging------------------------------------
				OFFSET @SkipRow ROWS FETCH NEXT @pageSize ROWS ONLY
				FOR JSON AUTO, Include_Null_Values),''))
				SELECT @result
			END

		END
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
		ROLLBACK TRANSACTION;
		DECLARE @ErrorNumber INT = ERROR_NUMBER();
		DECLARE @ErrorLine INT = ERROR_LINE();
		DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
		DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
		DECLARE @ErrorState INT = ERROR_STATE();

	END CATCH
END
GO
USE [master]
GO
ALTER DATABASE [FeedBack] SET  READ_WRITE 
GO
