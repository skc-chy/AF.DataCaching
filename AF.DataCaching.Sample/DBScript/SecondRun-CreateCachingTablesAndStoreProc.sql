
/****** Object:  StoredProcedure [dbo].[ClearCache]    Script Date: 7/16/2016 12:16:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ClearCache] 
	
AS

BEGIN
	
	DELETE FROM dbo.DataCaching

END

GO
/****** Object:  StoredProcedure [dbo].[ClearCacheByType]    Script Date: 7/16/2016 12:16:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ClearCacheByType] 
	@CacheObjectType NVarchar(200)

AS

BEGIN
	
	DELETE FROM dbo.DataCaching WHERE CacheObjectType=@CacheObjectType

END

GO
/****** Object:  StoredProcedure [dbo].[GetCache]    Script Date: 7/16/2016 12:16:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetCache] 
	@CacheParam NVarchar(Max),
	@CacheObjectType NVarchar(200),
	@CurrentDateTime Datetime
AS
BEGIN

   DELETE FROM dbo.DataCaching WHERE ExpiredOn<@CurrentDateTime
	
   SELECT CacheData FROM dbo.DataCaching WHERE CacheParam=@CacheParam AND CacheObjectType=@CacheObjectType
END

GO
/****** Object:  StoredProcedure [dbo].[SaveCache]    Script Date: 7/16/2016 12:16:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveCache] 
	-- Add the parameters for the stored procedure here
	@CacheID uniqueidentifier,
	@CacheData XML,
	@CacheParam NVarchar(max),
	@CacheObjectType NVarchar(200),
	@CacheOn Datetime,
	@ExpiredOn Datetime
	
AS

BEGIN
	
	DELETE FROM dbo.DataCaching WHERE CacheParam=@CacheParam AND CacheObjectType=@CacheObjectType

	INSERT INTO dbo.DataCaching(CacheID,CacheData,CacheParam,CacheObjectType,CacheOn,ExpiredOn) VALUES(@CacheID,@CacheData,@CacheParam,@CacheObjectType,@CacheOn,@ExpiredOn)
END

GO

/****** Object:  StoredProcedure [dbo].[DeleteExpiredCache]    Script Date: 7/16/2016 4:04:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteExpiredCache] 

AS
BEGIN
	DELETE FROM [dbo].[DataCaching] WHERE ExpiredOn<GETDATE()
END

GO

/****** Object:  Table [dbo].[DataCaching]    Script Date: 7/16/2016 12:16:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataCaching](
	[CacheID] [uniqueidentifier] NOT NULL,
	[CacheData] [xml] NOT NULL,
	[CacheParam] [nvarchar](max) NOT NULL,
	[CacheObjectType] [nvarchar](200) NOT NULL,
	[CacheOn] [datetime] NOT NULL,
	[ExpiredOn] [datetime] NOT NULL,
 CONSTRAINT [PK_DataCaching] PRIMARY KEY CLUSTERED 
(
	[CacheID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO