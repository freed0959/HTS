USE [HTSdb]
GO

/****** Object:  StoredProcedure [dbo].[SP_Upd_Mtrl]    Script Date: 09/16/2014 22:53:06 ******/
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

	--declare @idpl as bigint = 107, @mtrl as nvarchar(15)= 'TPTS', @mtLama as nvarchar(15) = 'TPHC', @ID_actMB as bigint = 1014;
	DECLARE @opsi as smallint, @dtm as NVARCHAR(25), @jam as NVARCHAR(25)
	, @dest as bigint, @mtLama as nvarchar(15), @kntr as nvarchar(15), @plan_hour as smalldatetime;

	select @dtm = convert(nvarchar,pln_date,101), @opsi = ID_Opsi, @jam = convert(nvarchar,pln_date,108) 
	from tbl_PlanBarging where id_plnbrg = @idpl

	select @dest = Destination , @mtLama = MaterialKode, @kntr = KontraktorKode
	from tbl_ActMasterBlend where ID_actMB = @ID_actMB

	--determine hour of barging plan--
	SELECT @plan_hour = pln_date FROM dbo.tbl_PlanBarging WHERE ID_PlnBrg = @idpl

	update tbl_ActMasterBlend set MaterialKode = @mtrl 
	where ID_actMB = @ID_actMB
	and MaterialKode = @mtLama

	IF(CONVERT(nvarchar, @plan_hour, 108) >= '06:00:00')
	BEGIN
		update tbl_ActMasterBlend set MaterialKode = @mtrl, TruckCount = @trCount 
		from tbl_PlanBarging pb, tbl_ActMasterBlend ac 
		WHERE pb.id_plnbrg = ac.id_plnbrg
		AND (convert(nvarchar,pb.pln_date,108) > @jam 
			OR pb.pln_date BETWEEN DATEADD(HOUR, 0, convert(nvarchar, pb.pln_date, 112)) 
			AND DATEADD(minute, -1, DATEADD(HOUR, 6, convert(nvarchar, pb.pln_date, 112))))
		AND convert(nvarchar,pb.pln_date,101) = @dtm
		and ac.MaterialKode = @mtLama
		and pb.ID_Opsi = @opsi
		and ac.Destination = @dest
		and ac.KontraktorKode = @kntr
	END
	ELSE IF(CONVERT(nvarchar, @plan_hour, 108) >= '00:00:00' AND CONVERT(nvarchar, @plan_hour, 108) < '06:00:00')
	BEGIN
		update tbl_ActMasterBlend set MaterialKode = @mtrl, TruckCount=@trCount  
		from tbl_PlanBarging pb, tbl_ActMasterBlend ac 
		WHERE pb.id_plnbrg = ac.id_plnbrg
		AND (convert(nvarchar,pb.pln_date,108) > @jam)
		AND convert(nvarchar,pb.pln_date,101) = @dtm
		and ac.MaterialKode = @mtLama
		and pb.ID_Opsi = @opsi
		and ac.Destination = @dest
		and ac.KontraktorKode = @kntr
	END
END

GO


