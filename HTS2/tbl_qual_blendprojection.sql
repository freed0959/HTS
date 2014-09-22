USE [HTSdb]
GO

/****** Object:  Table [dbo].[tbl_qual_blendprojection]    Script Date: 09/22/2014 16:32:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tbl_qual_blendprojection](
	[id_blendprojection] [bigint] IDENTITY(1,1) NOT NULL,
	[id_qualplan_containr] [bigint] NULL,
	[id_blend_area] [bigint] NULL,
	[KontraktorKode] [varchar](10) NULL,
	[MaterialKode] [varchar](10) NULL,
	[blend_TM] [float] NULL,
	[blend_IM] [float] NULL,
	[blend_AshAdb] [float] NULL,
	[blend_AshAr] [float] NULL,
	[blend_TSAdb] [float] NULL,
	[blend_TSAr] [float] NULL,
	[blend_CVAr] [float] NULL,
	[blend_CVadb] [float] NULL,
	[blend_CVDaf] [float] NULL,
	[blend_HGI] [float] NULL,
	[blend_tons_avb] [float] NULL,
	[projection_type] [bigint] NULL,
	[blend_ton_control] [float] NULL,
	[blend_portal] [bigint] NULL,
	[blend_note] [nvarchar](255) NULL,
	[id_product] [bigint] NULL,
 CONSTRAINT [Pk_tbl_qual_ptrprojection_1] PRIMARY KEY CLUSTERED 
(
	[id_blendprojection] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


