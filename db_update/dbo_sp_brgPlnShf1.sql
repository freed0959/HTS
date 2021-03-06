USE [HTSdb]
GO
/****** Object:  StoredProcedure [dbo].[sp_brgPlnShf1]    Script Date: 09/28/2014 06:47:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [dbo].[sp_brgPlnShf1] (@tgl as nvarchar(25)= NULL)
AS

BEGIN

	--DECLARE @tgl as nvarchar(25);
	IF(@tgl is null)
	BEGIN
		set @tgl = convert(nvarchar,getdate(),101);
	END

	SET NOCOUNT ON;

	select pb.id_plnbrg		
		, pb.pln_date as dt
		, vw.dt as tgl
		, rm1.ref_name as K1
		, rm3.ref_name as K3
		, mo.OpsiName as opsiName
		, pb.K1 as idk1
		, pb.K3 as idk3
		, vw.idx
		, 'Shift ' + CONVERT(char(1), dbo.fn_getShift(vw._hour)) as brg_shift
	from vwHour_shf1 vw   
	left outer join (
			SELECT *, dbo.fn_fullhour(pln_date) as dt
			FROM tbl_PlanBarging
			WHERE CONVERT(nvarchar, pln_date, 101) = @tgl
		) pb ON vw.dt = pb.dt
	left join tbl_ref_master rm1 on pb.K1 = rm1.id_ref_master
	left join tbl_ref_master rm3 on pb.K3 = rm3.id_ref_master
	left outer join tbl_MappingOpsi mo on pb.ID_opsi = mo.ID_Opsi

	union all
	select pb.id_plnbrg
		, pb.pln_date as dt
		, vw.dt as tgl
		, rm1.ref_name as K1
		, rm3.ref_name as K3
		, mo.OpsiName as opsiName
		, pb.K1 as idk1
		, pb.K3 as idk3
		, vw.idx
		, 'Shift ' + CONVERT(char(1), dbo.fn_getShift(vw._hour)) as brg_shift
	from vwHour_shf2 vw   
	left outer join (
			SELECT *, dbo.fn_fullhour(pln_date) as dt
			FROM tbl_PlanBarging
			WHERE CONVERT(nvarchar, pln_date, 101) = @tgl
		) pb ON vw.dt = pb.dt
	left join tbl_ref_master rm1 on pb.K1 = rm1.id_ref_master
	left join tbl_ref_master rm3 on pb.K3 = rm3.id_ref_master
	left outer join tbl_MappingOpsi mo on pb.ID_opsi = mo.ID_Opsi
ORDER BY vw.idx ASC

END


