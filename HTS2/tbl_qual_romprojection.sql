USE [HTSdb]
GO

/****** Object:  Table [dbo].[tbl_qual_romprojection]    Script Date: 09/22/2014 16:32:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tbl_qual_romprojection](
	[id_romprojection] [bigint] IDENTITY(1,1) NOT NULL,
	[id_qualplan_containr] [bigint] NULL,
	[id_rom_area] [bigint] NULL,
	[KontraktorKode] [varchar](10) NULL,
	[MaterialKode] [varchar](10) NULL,
	[rom_TM] [float] NULL,
	[rom_IM] [float] NULL,
	[rom_AshAdb] [float] NULL,
	[rom_TSAdb] [float] NULL,
	[rom_CVadb] [float] NULL,
	[rom_CVDaf] [float] NULL,
	[rom_HGI] [float] NULL,
	[rom_AshAr] [float] NULL,
	[rom_TSAr] [float] NULL,
	[rom_CVAr] [float] NULL,
	[id_product] [bigint] NULL,
 CONSTRAINT [Pk_tbl_qual_ptrprojection_0] PRIMARY KEY CLUSTERED 
(
	[id_romprojection] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


