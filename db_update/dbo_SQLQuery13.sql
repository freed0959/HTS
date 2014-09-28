USE [HTSdb]
GO

/****** Object:  StoredProcedure [dbo].[SP_del_SkmBlnd]    Script Date: 09/29/2014 12:32:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




ALTER PROCEDURE [dbo].[SP_del_SkmBlnd]
	(	@idpl as bigint= NULL, 
		@ID_actMB as bigint = NULL
	)
AS

BEGIN
	--declare @idpl as bigint = 110, @mtrl as nvarchar(15)= 'TSSLA', @mtLama as nvarchar(15) = 'TPHC';
	DECLARE @opsi as smallint, @dtm as NVARCHAR(25), @jam as NVARCHAR(25)
	,@dest as bigint, @mtLama as nvarchar(15), @kntr as nvarchar(15), @plan_hour as smalldatetime;

	select @dtm = convert(nvarchar,pln_date,101), @opsi = ID_Opsi, @jam = convert(nvarchar,pln_date,108) 
	from tbl_PlanBarging where id_plnbrg = @idpl

	select @dest = Destination , @mtLama = MaterialKode, @kntr = KontraktorKode
	from tbl_ActMasterBlend where ID_actMB = @ID_actMB

	--determine hour of barging plan--
	SELECT @plan_hour = pln_date FROM dbo.tbl_PlanBarging WHERE ID_PlnBrg = @idpl

	--<<<<<<<<<<<<<<<< DELETE CURRENT BLENDING SCHEME >>>>>>>>>>>>>>>>>>>>>
	delete tbl_ActMasterBlend
	where ID_actMB = @ID_actMB

	--<<<<<<<<<<<<<<<< DELETE LATER BLENDING SCHEME >>>>>>>>>>>>>>>>>>>>>
	IF(CONVERT(nvarchar, @plan_hour, 108) >= '00:00:00' AND CONVERT(nvarchar, @plan_hour, 108) < '05:00:00')
	BEGIN
		delete tbl_ActMasterBlend 
		from tbl_PlanBarging pb, tbl_ActMasterBlend ac 
		WHERE pb.id_plnbrg = ac.id_plnbrg
		AND (pb.pln_date  between DATEADD(HOUR, 1, pb.pln_date)  AND DATEADD(minute, -1, DATEADD(HOUR, 6, convert(nvarchar, pb.pln_date, 112))))
		and ac.MaterialKode = @mtLama
		and pb.ID_Opsi = @opsi
		and ac.Destination = @dest
		and ac.KontraktorKode = @kntr
	END
	ELSE IF(CONVERT(nvarchar, @plan_hour, 108) >= '06:00:00')
	BEGIN
		delete tbl_ActMasterBlend 
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


