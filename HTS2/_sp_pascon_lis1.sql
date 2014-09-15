/****** Script for SelectTopNRows command from SSMS  ******/
ALTER PROCEDURE _sp_pascon_lis1 (
	@dtm as varchar(25), 
	@unt as varchar(10)=null,
	@mat as varchar(10)=null,
	@rom as varchar(10)=null,
	@com as varchar(10)
	)
AS

BEGIN

DECLARE @sql  nvarchar(MAX);

SET NOCOUNT ON;

SET @sql = 'SELECT [ID_RomDist]
      ,[TransactionDate]
      ,[KontraktorKode]
      ,[TruckNo]
      ,tr.[MaterialKode]
      ,[SourceKode]
      ,[Shift]
      , map.seamcargo 
      , por.series_name
  FROM [HTSdb].[dbo].[tbl_TruckRomDist] tr
  LEFT OUTER JOIN vwQual_lasportal1 por ON tr.MaterialKode = por.MaterialKode
  LEFT OUTER JOIN tbl_qualmapping map ON tr.MaterialKode = map.Kode 
  WHERE CONVERT(nvarchar, [TransactionDate], 101) = '''+ @dtm +''' AND (KontraktorKode='''+ @com +''') '
  IF(@unt IS NOT NULL)
  BEGIN
	SET @sql = @sql + ' AND (TruckNo='''+ @unt +''')'
  END
  IF(@mat IS NOT NULL)
  BEGIN
	SET @sql = @sql + ' AND (tr.MaterialKode='''+ @mat +''')'
  END
  IF(@rom IS NOT NULL)
  BEGIN
	SET @sql = @sql + ' AND (SourceKode='''+ @rom +''')'
  END
  
 PRINT(@sql) 
 EXEC(@sql)
  
  END
  GO
  
  EXEC _sp_pascon_lis1 '09/15/2014', NULL, NULL, NULL, 'SIS'