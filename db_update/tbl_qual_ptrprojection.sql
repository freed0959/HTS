USE [HTSdb]
GO

/****** Object:  Table [dbo].[tbl_qual_ptrprojection]    Script Date: 09/22/2014 16:32:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tbl_qual_ptrprojection](
	[id_prtprojection] [bigint] IDENTITY(1,1) NOT NULL,
	[id_qualplan_containr] [bigint] NULL,
	[id_ptr_area] [bigint] NULL,
	[KontraktorKode] [varchar](10) NULL,
	[MaterialKode] [varchar](10) NULL,
	[ptr_ton_available] [float] NULL,
	[ptr_TM] [float] NULL,
	[ptr_IM] [float] NULL,
	[ptr_AshAdb] [float] NULL,
	[ptr_TSAdb] [float] NULL,
	[ptr_CVadb] [float] NULL,
	[ptr_CVDaf] [float] NULL,
	[ptr_HGI] [float] NULL,
	[id_product] [bigint] NULL,
 CONSTRAINT [Pk_tbl_qual_ptrprojection] PRIMARY KEY CLUSTERED 
(
	[id_prtprojection] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


