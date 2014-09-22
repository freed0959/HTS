CREATE PROCEDURE _sp_ql_container_add1
(
	@PlanTypeKode tinyint
	, @plan_startdate smalldatetime
	, @plan_enddate smalldatetime
)
AS

BEGIN

DECLARE @idctr0 bigint, @idctr1 bigint;



INSERT INTO tbl_qualplan_containr (PlanTypeKode, plan_startdate, plan_enddate) VALUES (@PlanTypeKode,@plan_startdate,@plan_enddate)

SELECT @idctr0 = id_qualplan_containr - 1, @idctr1 = id_qualplan_containr 
FROM tbl_qualplan_containr ORDER BY id_qualplan_containr DESC

INSERT INTO [HTSdb].[dbo].[tbl_qual_romprojection]
           ([id_qualplan_containr]
           ,[id_rom_area]
           ,[KontraktorKode]
           ,[MaterialKode]
           ,[rom_TM]
           ,[rom_IM]
           ,[rom_AshAdb]
           ,[rom_TSAdb]
           ,[rom_CVadb]
           ,[rom_CVDaf]
           ,[rom_HGI]
           ,[rom_AshAr]
           ,[rom_TSAr]
           ,[rom_CVAr]
           ,[id_product])
SELECT @idctr1,
       id_ptr_area, 
       KontraktorKode, 
       MaterialKode, 
	   ptr_TM, 
       ptr_IM, 
       ptr_AshAdb,
       ptr_TSAdb, 
       ptr_CVadb, 
       ptr_CVDaf, 
       ptr_HGI, 
       ((100-ptr_TM)/(100-ptr_IM))*ptr_AshAdb, 
       ((100-ptr_TM)/(100-ptr_IM))*ptr_TSAdb, 
       ((100-ptr_TM)/(100-ptr_IM))*ptr_CVAdb, 
       id_product
FROM [HTSdb].[dbo].[tbl_qual_ptrprojection]
WHERE id_qualplan_containr = @idctr0
           
END

GO


