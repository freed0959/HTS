USE [HTSdb]
GO

/****** Object:  StoredProcedure [dbo].[_sp_rtk_pos1]    Script Date: 09/15/2014 16:54:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[_sp_rtk_pos1]
AS

BEGIN
DECLARE @dtm as datetime, 
@tm as datetime = CONVERT(nvarchar, GETDATE(), 108);

--SET @tm = '1900-01-01 09:05:00';

IF(@tm >= '00:00:00' AND @tm < '06:00:00')
BEGIN
	SET @dtm =  CONVERT(nvarchar, DATEADD(Day, -1, GETDATE()), 101)
END
ELSE IF(@tm >= '06:00:00' AND @tm <= '23:59:59')
BEGIN
	SET @dtm =  CONVERT(nvarchar, GETDATE(), 101)	
END

SELECT vw1.*, vw2.jummat1 FROM (
SELECT 1 as idpos, 'Passed Km. 67' as pos, COALESCE(COUNT(ID_Pass),0)  as jummat
	FROM [tbl_TruckPass] p
	WHERE (CONVERT(nvarchar, TransactionDate, 101) = @dtm)
	AND (KM_67Pass=1) AND (KM_29Pass IS NULL OR KM_29Pass=0) AND (inQueue IS NULL OR inQueue =0) AND (DateClosed IS NULL)
UNION ALL
SELECT 2 as idpos, 'Passed Km. 29' as pos, COALESCE(COUNT(ID_Pass),0)  as jummat
	FROM [tbl_TruckPass] p
	WHERE (CONVERT(nvarchar, TransactionDate, 101) = @dtm)
	AND (KM_29Pass=1) AND (inQueue IS NULL OR inQueue =0) AND (DateClosed IS NULL)
UNION ALL
SELECT 3 as idpos, 'Trucks In Queue' as pos, COALESCE(COUNT(ID_Pass),0)  as jummat
	FROM [tbl_TruckPass] p
	WHERE (CONVERT(nvarchar, TransactionDate, 101) = @dtm)
	AND (inQueue =1) AND (DateClosed IS NULL)
UNION ALL
SELECT 4 as idpos, 'Trans. Closed' as pos, COALESCE(COUNT(ID_Pass),0)  as jummat
	FROM [tbl_TruckPass] p
	WHERE (CONVERT(nvarchar, TransactionDate, 101) = @dtm)
	AND (CONVERT(nvarchar, DateClosed, 101) = @dtm) ) as vw1

INNER JOIN (
SELECT 1 as idpos, 'Passed Km. 67' as pos, COALESCE(COUNT(ID_Pass),0)  as jummat1
	FROM [tbl_TruckPass] p
	WHERE (CONVERT(nvarchar, TransactionDate, 101) = @dtm) AND (KM_67Pass=1) 
UNION ALL
SELECT 2 as idpos, 'Passed Km. 29' as pos, COALESCE(COUNT(ID_Pass),0)  as jummat1
	FROM [tbl_TruckPass] p
	WHERE (CONVERT(nvarchar, TransactionDate, 101) = @dtm) AND (KM_29Pass=1)
UNION ALL
SELECT 3 as idpos, 'Trucks In Queue' as pos, COALESCE(COUNT(ID_Pass),0)  as jummat1
	FROM [tbl_TruckPass] p
	WHERE (CONVERT(nvarchar, TransactionDate, 101) = @dtm) AND (inQueue =1) 
UNION ALL
SELECT 4 as idpos, 'Trans. Closed' as pos, COALESCE(COUNT(ID_Pass),0)  as jummat1
	FROM [tbl_TruckPass] p
	WHERE (CONVERT(nvarchar, TransactionDate, 101) = @dtm)
	AND (CONVERT(nvarchar, DateClosed, 101) = @dtm)
) as vw2 ON vw1.idpos = vw2.idpos
END



GO


