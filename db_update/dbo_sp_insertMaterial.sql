USE [HTSdb]
GO

/****** Object:  StoredProcedure [dbo].[sp_insertMaterial]    Script Date: 09/29/2014 12:32:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






ALTER PROCEDURE [dbo].[sp_insertMaterial]
	(	@idpl as bigint= NULL,
		@mat as nvarchar(15)= null,
		@kntr as nvarchar(15) = null, 
		@trcount as int, @dest as bigint
	)
AS

BEGIN
	--declare @idpl as bigint = 110, @mtrl as nvarchar(15)= 'TSSLA', @mtLama as nvarchar(15) = 'TPHC';
	DECLARE @opsi as smallint, @dtm as NVARCHAR(25), @jam as NVARCHAR(25), 
	 @plan_hour as smalldatetime, @TM float, @TS float, @ASH float, @CalAr float;

	DECLARE @qual as Table ( 
		id int,
		MaterialKode varchar(10),
		TM float,
		TS float,
		ASH float,
		CalAr float
	);
	
	--determine hour of barging plan--
	select @dtm = convert(nvarchar,pln_date,101), @opsi = ID_Opsi, @jam = convert(nvarchar,pln_date,108), @plan_hour = pln_date 
	from tbl_PlanBarging where id_plnbrg = @idpl
	
	INSERT @qual
	EXEC _sp_qual_search1 @mat, @plan_hour
	
	SELECT @TM = TM, @TS = TS, @ASH = ASH, @CalAr = CalAr FROM @qual
	
	--<<<<<<<<<<<<< INSERT TO CURRENT BLEND >>>>>>>>>>>>>>>>>>>
	insert into tbl_ActMasterBlend 
	select @idpl,@mat,@kntr,@trcount,@dest,@TM,@TS,@ASH,@CalAr	
	
	
	--<<<<<<<<<<<<< INSERT TO LATER BLEND >>>>>>>>>>>>>>>>>>>		
	IF(CONVERT(nvarchar, @plan_hour, 108) >= '00:00:00' AND CONVERT(nvarchar, @plan_hour, 108) < '05:00:00')
	BEGIN
		insert into tbl_ActMasterBlend 
		select id_plnbrg,@mat,@kntr,@trcount,@dest,@TM,@TS,@ASH,@CalAr
		from tbl_PlanBarging
		where  (pln_date  between DATEADD(HOUR, 1, pln_date)  AND DATEADD(minute, -1, DATEADD(HOUR, 6, convert(nvarchar, pln_date, 112))))
		and ID_opsi  = @opsi
	END
	ELSE IF(CONVERT(nvarchar, @plan_hour, 108) >= '06:00:00')
	BEGIN
		insert into tbl_ActMasterBlend 
		select id_plnbrg,@mat,@kntr,@trcount,@dest,@TM,@TS,@ASH,@CalAr
		from tbl_PlanBarging
		where convert(nvarchar,pln_date,101) = @dtm
		AND (
			(pln_date BETWEEN DATEADD(HOUR, 0, convert(nvarchar, @plan_hour, 112)) 
				AND DATEADD(minute, -1, DATEADD(HOUR, 6, convert(nvarchar, pln_date, 112))))
			AND convert(nvarchar, pln_date, 101) = @dtm						
			and ID_Opsi = @opsi			
		)
		OR (
			(pln_date BETWEEN DATEADD(HOUR, 1, @plan_hour) 
				AND DATEADD(minute, -1, DATEADD(HOUR, 24, convert(nvarchar, pln_date, 112))))
			and ID_Opsi = @opsi			
			)
		
	END
END






GO


