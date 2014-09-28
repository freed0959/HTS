USE [HTSdb]
GO

/****** Object:  StoredProcedure [dbo].[SP_Upd_Mtrl]    Script Date: 09/29/2014 11:46:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_Upd_Mtrl]	
	(	@idpl as bigint= NULL
		, @mtrl as nvarchar(15)= NULL
		, @ID_actMB as bigint = NULL
		, @trCount as int
	)
AS

BEGIN

	--declare @idpl as bigint = 467, @mtrl as nvarchar(15)= 'BESI', @ID_actMB as bigint = 51736, @trCount as int = 0;
	DECLARE @opsi as smallint, @dtm as NVARCHAR(25), @jam as NVARCHAR(25)
	, @dest as bigint, @mtLama as nvarchar(15), @kntr as nvarchar(15), @plan_hour as smalldatetime;
	
	DECLARE @TM float,	@TS float,	@ASH float,	@CalAr float;

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

	select @dest = Destination , @mtLama = MaterialKode, @kntr = KontraktorKode
	from tbl_ActMasterBlend where ID_actMB = @ID_actMB

	INSERT @qual
	EXEC _sp_qual_search1 @mtrl, @plan_hour
	
	SELECT @TM = TM, @TS = TS, @ASH = ASH, @CalAr = CalAr FROM @qual
	
	--<<<<<<<<<<< UPDATE CURRENT BLENDING SCHEME >>>>>>>>>>>>>>
	update tbl_ActMasterBlend set MaterialKode = @mtrl, TruckCount = @trCount, TM = @TM, TS = @TS , ASH = @ASH, CalAr = @CalAr
	where ID_actMB = @ID_actMB	

	--<<<<<<<<<<< UPDATE LATER BLENDING SCHEME >>>>>>>>>>>>>>
	IF(CONVERT(nvarchar, @plan_hour, 108) >= '00:00:00' AND CONVERT(nvarchar, @plan_hour, 108) < '05:00:00')
	BEGIN
		update tbl_ActMasterBlend set MaterialKode = @mtrl, TruckCount=@trCount, TM = @TM, TS = @TS , ASH = @ASH, CalAr = @CalAr  
		from tbl_PlanBarging pb, tbl_ActMasterBlend ac 
		WHERE pb.id_plnbrg = ac.id_plnbrg
		AND (pb.pln_date BETWEEN DATEADD(HOUR, 1,@plan_hour) AND DATEADD(minute, -1, DATEADD(HOUR, 6, convert(nvarchar, pb.pln_date, 112))))
		and ac.MaterialKode = @mtLama
		and pb.ID_Opsi = @opsi
		and ac.Destination = @dest
		and ac.KontraktorKode = @kntr
	END
	ELSE IF(CONVERT(nvarchar, @plan_hour, 108) >= '06:00:00')
	BEGIN
		update tbl_ActMasterBlend set MaterialKode = @mtrl, TruckCount = @trCount, TM = @TM, TS = @TS , ASH = @ASH, CalAr = @CalAr
		from tbl_PlanBarging pb, tbl_ActMasterBlend ac 
		WHERE (
			(pb.pln_date BETWEEN DATEADD(HOUR, 0, convert(nvarchar, @plan_hour, 112)) 
				AND DATEADD(minute, -1, DATEADD(HOUR, 6, convert(nvarchar, pb.pln_date, 112))))
			AND convert(nvarchar,pb.pln_date,101) = @dtm
			AND pb.id_plnbrg = ac.id_plnbrg
			and ac.MaterialKode = @mtLama			
			and pb.ID_Opsi = @opsi
			and ac.Destination = @dest
			and ac.KontraktorKode = @kntr
		)
		OR (
			(pb.pln_date BETWEEN DATEADD(HOUR, 1, @plan_hour) 
				AND DATEADD(minute, -1, DATEADD(HOUR, 24, convert(nvarchar, pb.pln_date, 112))))
			AND convert(nvarchar,pb.pln_date,101) = @dtm
			AND pb.id_plnbrg = ac.id_plnbrg
			and ac.MaterialKode = @mtLama
			and pb.ID_Opsi = @opsi
			and ac.Destination = @dest
			and ac.KontraktorKode = @kntr
			)
	END
	
END




GO


