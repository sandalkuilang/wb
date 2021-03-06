USE [master]
GO
/****** Object:  Database [WebTemplate]    Script Date: 7/28/2017 5:17:02 AM ******/
CREATE DATABASE [WebTemplate]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SAPPayment', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\SAPPayment.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SAPPayment_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\SAPPayment_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [WebTemplate] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WebTemplate].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WebTemplate] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WebTemplate] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WebTemplate] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WebTemplate] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WebTemplate] SET ARITHABORT OFF 
GO
ALTER DATABASE [WebTemplate] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WebTemplate] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WebTemplate] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WebTemplate] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WebTemplate] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WebTemplate] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WebTemplate] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WebTemplate] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WebTemplate] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WebTemplate] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WebTemplate] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WebTemplate] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WebTemplate] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WebTemplate] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WebTemplate] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WebTemplate] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WebTemplate] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WebTemplate] SET RECOVERY FULL 
GO
ALTER DATABASE [WebTemplate] SET  MULTI_USER 
GO
ALTER DATABASE [WebTemplate] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WebTemplate] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WebTemplate] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WebTemplate] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [WebTemplate] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'WebTemplate', N'ON'
GO
USE [WebTemplate]
GO
/****** Object:  UserDefinedFunction [dbo].[convert_date_to_normal]    Script Date: 7/28/2017 5:17:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[convert_date_to_normal] (@julian int)  
RETURNS [datetime] AS  
BEGIN
 
DECLARE @Gregorian datetime

	set @Gregorian = NULL

	IF @julian >0 
	BEGIN
		DECLARE @year int, @day int, @base datetime
		SET @year = (@julian/1000 - 100) 
		SET @day = (@julian % 1000) 
		SET @base = '12-31-1999'
		set @Gregorian= DATEADD(dy, @day, DATEADD(yyyy, @year, @base))
	END

	RETURN @Gregorian

END




GO
/****** Object:  UserDefinedFunction [dbo].[FUNCT_AR]    Script Date: 7/28/2017 5:17:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		AZSG
-- Create date: 23-09-2015
-- Description:	Function for query report AR Overrun 
-- Example for calling this function:
-- select * from dbo.[FUNCT_AR]('2015','7','','','','','','','','','PO')
-- 22/10/2015 (HPFR) replace SUM(C.OVERUNDERACTUAL) OverUnder into SUM(C.OVERUNDERPOTENTIAL) OverUnder
-- =============================================
CREATE FUNCTION [dbo].[FUNCT_AR] (
	@Year INT
	,@Month INT
	,@CompanyCode VARCHAR(15)
	,@OPG VARCHAR(10)
	,@Division VARCHAR(10)
	,@subDivision VARCHAR(10)
	,@Department VARCHAR(10)
	,@cc VARCHAR(10)
	,@wipa VARCHAR(10)
	,@pm VARCHAR(10)
	,@type VARCHAR(2)
	)
RETURNS @data TABLE (
	Company [varchar](100)
	,GOIARId [varchar](20)
	,GOIAR [varchar](20)
	,GOIARDesc [varchar](200)
	,YEAR [varchar](4)
	,MONTH [varchar](2)
	,PMBADGE [varchar](100)
	,PM [varchar](1000)
	,Budget FLOAT
	,CummExpend FLOAT
	,EAC FLOAT
	,OverUnder FLOAT
	,CloseOut FLOAT
	,Balance FLOAT
	,Pct FLOAT
	,VOWD FLOAT
	)
AS
BEGIN
	DECLARE @Company [varchar] (100)
		,@GOIARId [varchar] (20)
		,@GOIAR [varchar] (20)
		,@GOIARDesc [varchar] (200)
		,@YEAR1 [varchar] (4)
		,@MONTH1 [varchar] (2)
		,@PMBADGE [varchar] (100)
		,@PM1 [varchar] (100)
		,@Budget FLOAT
		,@CummExpend FLOAT
		,@EAC FLOAT
		,@OverUnder FLOAT
		,@CloseOut FLOAT
		,@Balance FLOAT
		,@Pct FLOAT
		,@VOWD FLOAT
		,@PMGab VARCHAR(1000)
		,@PMBADGEGab VARCHAR(200)
		,@PMGabLen INT

	DECLARE data_cursor CURSOR
	FOR
	SELECT *
	FROM (
		SELECT A1.*
			,SUM(C.BUDGET) Budget
			,SUM(C.CUMMULATIVEEXPENDITURE) CummExpend
			,SUM(C.EAC) EAC
			,CASE 
				WHEN MAX(c.CUMMULATIVEEXPENDITURE) > MAX(c.VOWD)
					THEN 
						SUM(c.CUMMULATIVEEXPENDITURE) - SUM(c.BUDGET) 
				ELSE
					CASE WHEN SUM(c.BUDGET) = 0
						THEN 0
						ELSE SUM(c.VOWD) - SUM(c.BUDGET)	
					END 
			END OverUnder 
			,SUM(C.CLOSEOUT) CloseOut
			,SUM(C.BALANCE) Balance
			,SUM(C.EXPENDED) Pct
			,SUM(C.VoWD) VOWD
		FROM (
			SELECT DISTINCT Y.COMPANYNAME Company
				,A.AR_ID GOIARId
				,B.AR_Number GOIAR
				,B.AR_DESCRIPTION GOIARDesc
				,A.YEARV
				,A.MONTHV
			FROM [MARS].[dbo].[AR_EXCEPTIONREPORT] A
			INNER JOIN AR_AFE_MASTER B ON A.AR_ID = B.AR_ID
			INNER JOIN AFE_AR_LOCAL_MAP D ON D.AR_ID = A.AR_ID
			INNER JOIN LOCAL_AFE_ORG Y ON Y.LOCAL_AFE_ID = D.Local_AFE_ID
			INNER JOIN V_MAP_ORG Z ON Z.LOCAL_AFE_ID = D.Local_AFE_ID
				AND A.EXCEPTIONREPORTTYPE = CASE 
					WHEN @type = 'PO'
						THEN '10'
					ELSE '12'
					END
			WHERE A.YEARV = @Year
				AND A.MONTHV = @Month
				--and Y.COMPANYCODE =@CompanyCode 
				AND (
					ISNULL(@CompanyCode, '') = ''
					OR (
						ISNULL(@CompanyCode, '') <> ''
						AND Y.companycode = @CompanyCode
						)
					)
				--AND Y.OPG=@OPG 
				AND (
					ISNULL(@OPG, '') = ''
					OR (
						ISNULL(@OPG, '') <> ''
						AND Y.OPG = @OPG
						)
					)
				--AND Y.DIVISION=@Division
				AND (
					ISNULL(@Division, '') = ''
					OR (
						ISNULL(@Division, '') <> ''
						AND Y.DIVISION = @Division
						)
					)
				--AND Y.SUBDIVISION=@subDivision 
				AND (
					ISNULL(@subDivision, '') = ''
					OR (
						ISNULL(@subDivision, '') <> ''
						AND Y.SUBDIVISION = @subDivision
						)
					)
				--AND Y.DEPARTMENT=@Department  
				AND (
					ISNULL(@Department, '') = ''
					OR (
						ISNULL(@Department, '') <> ''
						AND Y.DEPARTMENT = @Department
						)
					)
				--AND Z.CCBADGE=ISNULL(@cc,Z.CCBADGE) -- cc
				AND (
					ISNULL(@cc, '') = ''
					OR (
						ISNULL(@cc, '') <> ''
						AND Z.CCBADGE = @cc
						)
					)
				--AND Z.WIPABADGE=ISNULL(@wipa,Z.WIPABADGE) -- wipa
				AND (
					ISNULL(@wipa, '') = ''
					OR (
						ISNULL(@wipa, '') <> ''
						AND Z.WIPABADGE = @wipa
						)
					)
				--AND Z.PMBADGE=ISNULL(@pm,Z.PMBADGE) -- PM
				AND (
					ISNULL(@pm, '') = ''
					OR (
						ISNULL(@pm, '') <> ''
						AND Z.PMBADGE = @pm
						)
					)
				--GROUP BY Y.COMPANYNAME ,A.AR_ID , B.AR_Number ,B.AR_DESCRIPTION 
			) A1
		INNER JOIN AR_MONTHLYSTATUS C ON A1.GOIARId = C.AR_ID
			AND A1.YEARV = C.YEAR
			AND A1.MONTHV = C.MONTH
		GROUP BY A1.Company
			,A1.GOIARId
			,A1.GOIAR
			,A1.GOIARDesc
			,A1.YEARV
			,A1.MONTHV
		) AA
	ORDER BY AA.Pct DESC

	OPEN data_cursor

	FETCH NEXT
	FROM data_cursor
	INTO @Company
		,@GOIARId
		,@GOIAR
		,@GOIARDesc
		,@YEAR1
		,@MONTH1
		,@Budget
		,@CummExpend
		,@EAC
		,@OverUnder
		,@CloseOut
		,@Balance
		,@Pct
		,@VOWD

	SET @PMBADGEGab = ''
	SET @PMGab = ''

	WHILE @@FETCH_STATUS = 0
	BEGIN
		DECLARE pm_cursor CURSOR
		FOR
		SELECT DISTINCT PMBADGE
			,(
				SELECT TOP 1 NAME
				FROM USER_PROFILE
				WHERE BADGE = B.PMBADGE
				) PM
		FROM AFE_AR_LOCAL_MAP A
		INNER JOIN V_MAP_ORG B ON A.LOCAL_AFE_ID = B.Local_AFE_ID
		WHERE A.AR_ID = @GOIARId

		SET @PMBADGEGab = ''
		SET @PMGab = ''

		OPEN pm_cursor

		FETCH NEXT
		FROM pm_cursor
		INTO @PMBADGE
			,@PM1

		WHILE @@FETCH_STATUS = 0
		BEGIN
			SET @PMBADGEGab = @PMBADGEGab + ', ' + @PMBADGE
			SET @PMGab = @PMGab + ', ' + @PM1

			FETCH NEXT
			FROM pm_cursor
			INTO @PMBADGE
				,@PM1
		END

		CLOSE pm_cursor

		DEALLOCATE pm_cursor

		SET @PMGabLen = LEN(@PMGab)
		SET @PMGab = SUBSTRING(@PMGab, 3, @PMGabLen - 2)

		INSERT INTO @data
		VALUES (
			@Company
			,@GOIARId
			,@GOIAR
			,@GOIARDesc
			,@YEAR1
			,@MONTH1
			,SUBSTRING(@PMBADGEGab, 3, 100)
			,@PMGab
			,@Budget
			,@CummExpend
			,@EAC
			,@OverUnder
			,@CloseOut
			,@Balance
			,@Pct
			,@VOWD
			)

		FETCH NEXT
		FROM data_cursor
		INTO @Company
			,@GOIARId
			,@GOIAR
			,@GOIARDesc
			,@YEAR1
			,@MONTH1
			,@Budget
			,@CummExpend
			,@EAC
			,@OverUnder
			,@CloseOut
			,@Balance
			,@Pct
			,@VOWD
	END

	CLOSE data_cursor

	DEALLOCATE data_cursor

	RETURN
END




GO
/****** Object:  UserDefinedFunction [dbo].[FUNCT_GOI]    Script Date: 7/28/2017 5:17:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		AZSG
-- Create date: 23-09-2015
-- Description:	Function for query report GOI Overrun (Potential/ Actual)
-- Example for calling this function:
-- select * from dbo.[FUNCT_GOI]('2015','7','','','','','','','','','AO')
-- 22/10/2015 (HPFR) add SELECT DISTINCT(Y.COMPANYNAME) Company
-- =============================================
CREATE FUNCTION [dbo].[FUNCT_GOI] (
	@Year INT
	,@Month INT
	,@CompanyCode VARCHAR(15)
	,@OPG VARCHAR(10)
	,@Division VARCHAR(10)
	,@subDivision VARCHAR(10)
	,@Department VARCHAR(10)
	,@cc VARCHAR(10)
	,@wipa VARCHAR(10)
	,@pm VARCHAR(10)
	,@type VARCHAR(2)
	)
RETURNS @data TABLE (
	Company [varchar](100)
	,GOIARId [varchar](20)
	,GOIAR [varchar](20)
	,GOIARDesc [varchar](200)
	,YEAR [varchar](4)
	,MONTH [varchar](2)
	,PMBADGE [varchar](100)
	,PM [varchar](1000)
	,Budget FLOAT
	,CummExpend FLOAT
	,EAC FLOAT
	,OverUnder FLOAT
	,CloseOut FLOAT
	,Balance FLOAT
	,Pct FLOAT
	,VOWD FLOAT
	)
AS
BEGIN
	DECLARE @Company [varchar] (100)
		,@GOIARId [varchar] (20)
		,@GOIAR [varchar] (20)
		,@GOIARDesc [varchar] (200)
		,@YEAR1 [varchar] (4)
		,@MONTH1 [varchar] (2)
		,@PMBADGE [varchar] (100)
		,@PM1 [varchar] (100)
		,@Budget FLOAT
		,@CummExpend FLOAT
		,@EAC FLOAT
		,@OverUnder FLOAT
		,@CloseOut FLOAT
		,@Balance FLOAT
		,@Pct FLOAT
		,@VOWD FLOAT
		,@PMGab VARCHAR(1000)
		,@PMBADGEGab VARCHAR(200)
		,@PMGabLen INT

	DECLARE data_cursor CURSOR
	FOR
	SELECT *
	FROM (
		SELECT A1.*
			,SUM(C.BUDGET) Budget
			,SUM(C.CUMMULATIVEEXPENDITURE) CummExpend
			,SUM(C.EAC) EAC
			,CASE 
				WHEN MAX(c.CUMMULATIVEEXPENDITURE) > MAX(c.VOWD)
					THEN 
						SUM(c.CUMMULATIVEEXPENDITURE) - SUM(c.BUDGET) 
				ELSE
					CASE WHEN SUM(c.BUDGET) = 0
						THEN 0
						ELSE SUM(c.VOWD) - SUM(c.BUDGET)	
					END 
			END OverUnder 
			,SUM(C.CLOSEOUT) CloseOut
			,SUM(C.BALANCE) Balance
			,SUM(C.EXPENDED) Pct
			,SUM(C.VoWD) VOWD
		FROM (
			SELECT DISTINCT(Y.COMPANYNAME) Company
				,A.GOI_ID GOIARId
				,D.GOI_NUMBER GOIAR
				,D.GOI_DESCRIPTION GOIARDesc
				,A.YEAR
				,A.MONTH
			FROM [MARS].[dbo].[GOI_EXCEPTIONREPORT] A
			INNER JOIN AFE_GOI_LOCAL_MAP B ON A.GOI_ID = B.GOI_ID
			INNER JOIN GOI_AFE_MASTER D ON D.GOI_ID = A.GOI_ID
			INNER JOIN LOCAL_AFE_ORG Y ON Y.LOCAL_AFE_ID = B.LOCAL_AFE_ID
			INNER JOIN V_MAP_ORG Z ON Z.LOCAL_AFE_ID = B.LOCAL_AFE_ID
				AND A.EXCEPTIONREPORTTYPE = CASE 
					WHEN @type = 'PO'
						THEN '6'
					ELSE '8'
					END
			WHERE A.YEAR = @Year
				AND A.MONTH = @Month
				--and Y.COMPANYCODE =@CompanyCode 
				AND (
					ISNULL(@CompanyCode, '') = ''
					OR (
						ISNULL(@CompanyCode, '') <> ''
						AND Y.companycode = @CompanyCode
						)
					)
				--AND Y.OPG=@OPG 
				AND (
					ISNULL(@OPG, '') = ''
					OR (
						ISNULL(@OPG, '') <> ''
						AND Y.OPG = @OPG
						)
					)
				--AND Y.DIVISION=@Division
				AND (
					ISNULL(@Division, '') = ''
					OR (
						ISNULL(@Division, '') <> ''
						AND Y.DIVISION = @Division
						)
					)
				--AND Y.SUBDIVISION=@subDivision 
				AND (
					ISNULL(@subDivision, '') = ''
					OR (
						ISNULL(@subDivision, '') <> ''
						AND Y.SUBDIVISION = @subDivision
						)
					)
				--AND Y.DEPARTMENT=@Department  
				AND (
					ISNULL(@Department, '') = ''
					OR (
						ISNULL(@Department, '') <> ''
						AND Y.DEPARTMENT = @Department
						)
					)
				--AND Z.CCBADGE=ISNULL(@cc,Z.CCBADGE) -- cc
				AND (
					ISNULL(@cc, '') = ''
					OR (
						ISNULL(@cc, '') <> ''
						AND Z.CCBADGE = @cc
						)
					)
				--AND Z.WIPABADGE=ISNULL(@wipa,Z.WIPABADGE) -- wipa
				AND (
					ISNULL(@wipa, '') = ''
					OR (
						ISNULL(@wipa, '') <> ''
						AND Z.WIPABADGE = @wipa
						)
					)
				--AND Z.PMBADGE=ISNULL(@pm,Z.PMBADGE) -- PM
				AND (
					ISNULL(@pm, '') = ''
					OR (
						ISNULL(@pm, '') <> ''
						AND Z.PMBADGE = @pm
						)
					) 
			) A1
		INNER JOIN GOI_MONTHLYSTATUS C ON A1.GOIARId = C.GOI_ID
			AND A1.YEAR = C.YEAR
			AND A1.MONTH = C.MONTH
		GROUP BY A1.Company
			,A1.GOIARId
			,A1.GOIAR
			,A1.GOIARDesc
			,A1.YEAR
			,A1.MONTH
		) AA
	ORDER BY AA.Pct DESC

	OPEN data_cursor

	FETCH NEXT
	FROM data_cursor
	INTO @Company
		,@GOIARId
		,@GOIAR
		,@GOIARDesc
		,@YEAR1
		,@MONTH1
		,@Budget
		,@CummExpend
		,@EAC
		,@OverUnder
		,@CloseOut
		,@Balance
		,@Pct
		,@VOWD

	SET @PMBADGEGab = ''
	SET @PMGab = ''

	WHILE @@FETCH_STATUS = 0
	BEGIN
		DECLARE pm_cursor CURSOR
		FOR
		SELECT DISTINCT PMBADGE
			,(
				SELECT TOP 1 NAME
				FROM USER_PROFILE
				WHERE BADGE = B.PMBADGE
				) PM
		FROM AFE_GOI_LOCAL_MAP A
		INNER JOIN V_MAP_ORG B ON A.LOCAL_AFE_ID = B.Local_AFE_ID
		WHERE A.GOI_ID = @GOIARId

		SET @PMBADGEGab = ''
		SET @PMGab = ''

		OPEN pm_cursor

		FETCH NEXT
		FROM pm_cursor
		INTO @PMBADGE
			,@PM1

		WHILE @@FETCH_STATUS = 0
		BEGIN
			SET @PMBADGEGab = @PMBADGEGab + ', ' + @PMBADGE
			SET @PMGab = @PMGab + ', ' + @PM1

			FETCH NEXT
			FROM pm_cursor
			INTO @PMBADGE
				,@PM1
		END

		CLOSE pm_cursor

		DEALLOCATE pm_cursor

		SET @PMGabLen = LEN(@PMGab)
		SET @PMGab = SUBSTRING(@PMGab, 3, @PMGabLen - 2)

		INSERT INTO @data
		VALUES (
			@Company
			,@GOIARId
			,@GOIAR
			,@GOIARDesc
			,@YEAR1
			,@MONTH1
			,SUBSTRING(@PMBADGEGab, 3, 100)
			,@PMGab
			,@Budget
			,@CummExpend
			,@EAC
			,@OverUnder
			,@CloseOut
			,@Balance
			,@Pct
			,@VOWD
			)

		FETCH NEXT
		FROM data_cursor
		INTO @Company
			,@GOIARId
			,@GOIAR
			,@GOIARDesc
			,@YEAR1
			,@MONTH1
			,@Budget
			,@CummExpend
			,@EAC
			,@OverUnder
			,@CloseOut
			,@Balance
			,@Pct
			,@VOWD
	END

	CLOSE data_cursor

	DEALLOCATE data_cursor

	RETURN
END




GO
/****** Object:  UserDefinedFunction [dbo].[SplitString]    Script Date: 7/28/2017 5:17:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		HPFR
-- Create date: 2015-07-29
-- Description:	Split a string by delimeter char
-- =============================================
CREATE FUNCTION [dbo].[SplitString]
(    
      @Input NVARCHAR(MAX),
      @Character CHAR(1)
)
RETURNS @Output TABLE (
      Item NVARCHAR(1000)
)
AS
BEGIN
      DECLARE @StartIndex INT, @EndIndex INT
 
      SET @StartIndex = 1
      IF SUBSTRING(@Input, LEN(@Input) - 1, LEN(@Input)) <> @Character
      BEGIN
            SET @Input = @Input + @Character
      END
 
      WHILE CHARINDEX(@Character, @Input) > 0
      BEGIN
            SET @EndIndex = CHARINDEX(@Character, @Input)
           
            INSERT INTO @Output(Item)
            SELECT SUBSTRING(@Input, @StartIndex, @EndIndex - 1)
           
            SET @Input = SUBSTRING(@Input, @EndIndex + 1, LEN(@Input))
      END
 
      RETURN
END




GO
/****** Object:  Table [dbo].[CONFIG_TABLE]    Script Date: 7/28/2017 5:17:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CONFIG_TABLE](
	[CONFIGID] [int] NOT NULL,
	[PARAMETERNAME] [nvarchar](100) NULL,
	[TYPEV] [nvarchar](100) NULL,
	[VALUE] [nvarchar](2000) NULL,
	[ADDITIONAL_VALUE] [varchar](max) NULL,
	[CREATEDBY] [nvarchar](50) NOT NULL,
	[CREATEDDATE] [datetime] NOT NULL,
	[MODIFIEDDATE] [datetime] NULL,
	[MODIFIEDBY] [nvarchar](50) NULL,
 CONSTRAINT [PK__CONFIG_T__C1DFC8E41A14E395] PRIMARY KEY CLUSTERED 
(
	[CONFIGID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FEATURE]    Script Date: 7/28/2017 5:17:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FEATURE](
	[ID] [int] NOT NULL,
	[KEY] [nvarchar](100) NOT NULL,
	[QUALIFIER] [nvarchar](100) NOT NULL,
	[CREATEDBY] [nvarchar](50) NOT NULL,
	[CREATEDDATE] [datetime] NOT NULL,
	[MODIFIEDDATE] [datetime] NULL,
	[MODIFIEDBY] [nvarchar](50) NULL,
 CONSTRAINT [PK_FEATURE] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MENU]    Script Date: 7/28/2017 5:17:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MENU](
	[ID] [int] NOT NULL,
	[NAME] [nvarchar](50) NULL,
	[DESCRIPTION] [nvarchar](500) NULL,
	[REMARK] [nvarchar](50) NULL,
	[PARENT_ID] [int] NOT NULL,
	[URL] [nvarchar](100) NULL,
	[CREATEDBY] [nvarchar](50) NOT NULL,
	[CREATEDDATE] [datetime] NOT NULL,
	[MODIFIEDDATE] [datetime] NULL,
	[MODIFIEDBY] [nvarchar](50) NULL,
 CONSTRAINT [PK_MENU_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ROLE]    Script Date: 7/28/2017 5:17:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ROLE](
	[ID] [int] NOT NULL,
	[NAME] [nvarchar](50) NOT NULL,
	[DESCRIPTION] [varchar](100) NULL,
	[CREATEDBY] [nvarchar](50) NOT NULL,
	[CREATEDDATE] [datetime] NOT NULL,
	[MODIFIEDBY] [nvarchar](50) NULL,
	[MODIFIEDDATE] [datetime] NULL,
 CONSTRAINT [PK_ROLE_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ROLE_FUNCTION]    Script Date: 7/28/2017 5:17:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ROLE_FUNCTION](
	[ROLE_ID] [int] NOT NULL,
	[FUNCTION_ID] [int] NOT NULL,
	[FEATURE_ID] [int] NOT NULL,
	[DISPLAY] [int] NOT NULL,
	[CREATED_BY] [nvarchar](50) NOT NULL,
	[CREATED_DATE] [datetime] NOT NULL,
	[MODIFIED_BY] [nvarchar](50) NULL,
	[MODIFIED_DATE] [datetime] NULL,
 CONSTRAINT [PK_ROLE_FUNCTION_1] PRIMARY KEY CLUSTERED 
(
	[ROLE_ID] ASC,
	[FUNCTION_ID] ASC,
	[FEATURE_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ROLE_MENU]    Script Date: 7/28/2017 5:17:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ROLE_MENU](
	[ROLE_ID] [int] NOT NULL,
	[MENU_ID] [int] NOT NULL,
	[CREATEDBY] [nvarchar](50) NOT NULL,
	[CREATEDDATE] [datetime] NOT NULL,
	[MODIFIED_BY] [nvarchar](50) NULL,
	[MODIFIED_DATE] [datetime] NULL,
 CONSTRAINT [PK_ROLE_MENU_1] PRIMARY KEY CLUSTERED 
(
	[ROLE_ID] ASC,
	[MENU_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[USER_PROFILE]    Script Date: 7/28/2017 5:17:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USER_PROFILE](
	[USERNAME] [nvarchar](50) NOT NULL,
	[FIRSTNAME] [nvarchar](300) NULL,
	[LASTNAME] [nvarchar](50) NULL,
	[EMAIL] [nvarchar](300) NOT NULL,
	[STATUS] [int] NULL,
	[PASSWORDHASH] [nvarchar](50) NOT NULL,
	[SALT] [uniqueidentifier] NOT NULL,
	[ISBACKUP] [int] NULL,
	[COMPANY] [int] NULL,
	[OPG] [nchar](10) NULL,
	[DIVISION] [nvarchar](300) NULL,
	[SUBDIVISION] [nvarchar](300) NULL,
	[DEPARTMENT] [nvarchar](300) NULL,
	[SUBDEPARTMENT] [nvarchar](300) NULL,
	[ISACTIVE] [int] NOT NULL,
	[CREATEDBY] [nvarchar](50) NOT NULL,
	[CREATEDDATE] [datetime] NOT NULL,
	[MODIFIEDDATE] [datetime] NULL,
	[MODIFIEDBY] [nvarchar](50) NULL,
 CONSTRAINT [PK_USER_PROFILE] PRIMARY KEY CLUSTERED 
(
	[USERNAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[USER_ROLE]    Script Date: 7/28/2017 5:17:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[USER_ROLE](
	[USERNAME] [varchar](30) NOT NULL,
	[ROLE_ID] [int] NOT NULL,
	[CREATEDBY] [nvarchar](50) NOT NULL,
	[CREATEDDATE] [date] NOT NULL,
	[MODIFIEDBY] [varchar](30) NULL,
	[MODIFIEDDATE] [datetime] NULL,
 CONSTRAINT [PK_USER_ROLE] PRIMARY KEY CLUSTERED 
(
	[USERNAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[MENU] ([ID], [NAME], [DESCRIPTION], [REMARK], [PARENT_ID], [URL], [CREATEDBY], [CREATEDDATE], [MODIFIEDDATE], [MODIFIEDBY]) VALUES (1, N'Home', N'Home', NULL, -1, N'Home', N'System', CAST(N'2017-07-28 00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[MENU] ([ID], [NAME], [DESCRIPTION], [REMARK], [PARENT_ID], [URL], [CREATEDBY], [CREATEDDATE], [MODIFIEDDATE], [MODIFIEDBY]) VALUES (2, N'Payment', N'Payment', NULL, -1, N'Payment', N'System', CAST(N'2017-07-28 00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[MENU] ([ID], [NAME], [DESCRIPTION], [REMARK], [PARENT_ID], [URL], [CREATEDBY], [CREATEDDATE], [MODIFIEDDATE], [MODIFIEDBY]) VALUES (3, N'Admin', N'Admin', NULL, -1, N'', N'System', CAST(N'2017-07-28 00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[MENU] ([ID], [NAME], [DESCRIPTION], [REMARK], [PARENT_ID], [URL], [CREATEDBY], [CREATEDDATE], [MODIFIEDDATE], [MODIFIEDBY]) VALUES (4, N'Users', N'Users', NULL, 3, N'Account', N'System', CAST(N'2017-07-28 00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[ROLE] ([ID], [NAME], [DESCRIPTION], [CREATEDBY], [CREATEDDATE], [MODIFIEDBY], [MODIFIEDDATE]) VALUES (10, N'Guest', N'Guest', N'System', CAST(N'2017-07-28 00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[ROLE] ([ID], [NAME], [DESCRIPTION], [CREATEDBY], [CREATEDDATE], [MODIFIEDBY], [MODIFIEDDATE]) VALUES (50, N'Accounting', N'Accounting', N'System', CAST(N'2017-07-28 00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[ROLE] ([ID], [NAME], [DESCRIPTION], [CREATEDBY], [CREATEDDATE], [MODIFIEDBY], [MODIFIEDDATE]) VALUES (90, N'Administrator', N'Administrator', N'System', CAST(N'2017-07-28 00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[ROLE_MENU] ([ROLE_ID], [MENU_ID], [CREATEDBY], [CREATEDDATE], [MODIFIED_BY], [MODIFIED_DATE]) VALUES (50, 1, N'System', CAST(N'2017-07-28 00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[ROLE_MENU] ([ROLE_ID], [MENU_ID], [CREATEDBY], [CREATEDDATE], [MODIFIED_BY], [MODIFIED_DATE]) VALUES (50, 2, N'System', CAST(N'2017-07-28 00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[ROLE_MENU] ([ROLE_ID], [MENU_ID], [CREATEDBY], [CREATEDDATE], [MODIFIED_BY], [MODIFIED_DATE]) VALUES (90, 1, N'System', CAST(N'2017-07-28 00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[ROLE_MENU] ([ROLE_ID], [MENU_ID], [CREATEDBY], [CREATEDDATE], [MODIFIED_BY], [MODIFIED_DATE]) VALUES (90, 2, N'System', CAST(N'2017-07-28 00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[ROLE_MENU] ([ROLE_ID], [MENU_ID], [CREATEDBY], [CREATEDDATE], [MODIFIED_BY], [MODIFIED_DATE]) VALUES (90, 3, N'System', CAST(N'2017-07-28 00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[ROLE_MENU] ([ROLE_ID], [MENU_ID], [CREATEDBY], [CREATEDDATE], [MODIFIED_BY], [MODIFIED_DATE]) VALUES (90, 4, N'System', CAST(N'2017-07-28 00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[USER_PROFILE] ([USERNAME], [FIRSTNAME], [LASTNAME], [EMAIL], [STATUS], [PASSWORDHASH], [SALT], [ISBACKUP], [COMPANY], [OPG], [DIVISION], [SUBDIVISION], [DEPARTMENT], [SUBDEPARTMENT], [ISACTIVE], [CREATEDBY], [CREATEDDATE], [MODIFIEDDATE], [MODIFIEDBY]) VALUES (N'yudha', N'asd', N'asdasd', N'yudha_hyp@yahoo.com', NULL, N'莗È쐜匫Ɤ倦珘∍䗩겓뛪龏⟂�풧8ﳶ⢷Ⓙﴫ䑞꠶풪善펙㘧䡹⚶墉ꖚ㐧', N'e7dab57e-90f9-4fee-918c-7a6aa95e9682', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, N'System', CAST(N'2017-07-28 00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[USER_ROLE] ([USERNAME], [ROLE_ID], [CREATEDBY], [CREATEDDATE], [MODIFIEDBY], [MODIFIEDDATE]) VALUES (N'yudha', 90, N'System', CAST(N'2017-07-28' AS Date), NULL, NULL)
USE [master]
GO
ALTER DATABASE [WebTemplate] SET  READ_WRITE 
GO
