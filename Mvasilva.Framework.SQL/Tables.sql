/****** Object:  Table [dbo].[TB_GER_Usuario]    Script Date: 22/01/2016 14:50:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TB_GER_Usuario](
	[cod_Usuario] [int] IDENTITY(1,1) NOT NULL,
	[cod_Status] [int] NOT NULL,
	[cod_Empresa] [int] NOT NULL,
	[cod_Perfil] [int] NOT NULL,
	[dat_Criacao] [datetime] NOT NULL,
	[dat_UltimoLogin] [datetime] NULL,
	[dat_UltimaTrocaSenha] [datetime] NULL,
	[dat_UltimoBloqueio] [datetime] NULL,
	[ind_termo] [int] NULL,
	[nom_Email] [varchar](100) NOT NULL,
	[nom_Re] [varchar](10) NULL,
	[nom_Senha] [varchar](30) NULL,
	[nom_Usuario] [varchar](100) NOT NULL,
	[num_Telefone1] [varchar](20) NULL,
	[num_Telefone2] [varchar](20) NULL,
	[num_Tentativas] [int] NULL,
	[cod_Cache] [int] NULL,
	[cod_Cargo] [int] NULL,
	[cod_Chefe] [int] NULL,
	[cod_Matricula] [varchar](10) NULL,
	[dat_Nascimento] [datetime] NULL,
	[num_CPF] [varchar](11) NULL,
 CONSTRAINT [PK_TB_GER_Usuario] PRIMARY KEY CLUSTERED 
(
	[cod_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING ON
GO

ALTER TABLE [dbo].[TB_GER_Usuario]  WITH CHECK ADD  CONSTRAINT [FK_TB_GER_Usuario_TB_GER_Perfil] FOREIGN KEY([cod_Perfil])
REFERENCES [dbo].[TB_GER_Perfil] ([cod_Perfil])
GO

ALTER TABLE [dbo].[TB_GER_Usuario] CHECK CONSTRAINT [FK_TB_GER_Usuario_TB_GER_Perfil]
GO

ALTER TABLE [dbo].[TB_GER_Usuario]  WITH CHECK ADD  CONSTRAINT [FK_TB_GER_Usuario_TB_GER_Usuario_Status] FOREIGN KEY([cod_Status])
REFERENCES [dbo].[TB_GER_Usuario_Status] ([cod_Status])
GO

ALTER TABLE [dbo].[TB_GER_Usuario] CHECK CONSTRAINT [FK_TB_GER_Usuario_TB_GER_Usuario_Status]
GO


/****** Object:  Table [dbo].[TB_GER_KeepAlive]    Script Date: 22/01/2016 14:45:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_GER_KeepAlive](
	[cod_Usuario] [int] NULL,
	[cod_Modulo] [int] NULL,
	[dat_Registro] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TB_GER_Modulo]    Script Date: 22/01/2016 14:45:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TB_GER_Modulo](
	[cod_Modulo] [int] IDENTITY(1,1) NOT NULL,
	[nom_Modulo] [varchar](100) NULL,
	[dsc_Modulo] [varchar](8000) NULL,
	[dat_DataAtualizacao] [datetime] NULL,
	[ind_Ativo] [bit] NULL,
	[nom_URL] [varchar](max) NULL,
	[nom_URL_Externo] [varchar](max) NULL,
 CONSTRAINT [PK_TB_GER_Modulo] PRIMARY KEY CLUSTERED 
(
	[cod_Modulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
/****** Object:  Table [dbo].[TB_GER_Perfil]    Script Date: 22/01/2016 14:45:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TB_GER_Perfil](
	[cod_Perfil] [int] IDENTITY(1,1) NOT NULL,
	[nom_Perfil] [varchar](100) NULL,
	[ind_Ativo] [int] NULL,
	[dsc_Perfil] [varchar](8000) NULL,
 CONSTRAINT [PK_TB_GER_Perfil] PRIMARY KEY CLUSTERED 
(
	[cod_Perfil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
/****** Object:  Table [dbo].[TB_GER_Perfil_Permissao]    Script Date: 22/01/2016 14:45:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_GER_Perfil_Permissao](
	[cod_Perfil] [int] NULL,
	[cod_Permissao] [int] NULL,
	[val_Permissao] [int] NULL,
 CONSTRAINT [PK_TB_GER_Perfil_Permissao] UNIQUE NONCLUSTERED 
(
	[cod_Perfil] ASC,
	[cod_Permissao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TB_GER_Permissao]    Script Date: 22/01/2016 14:45:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TB_GER_Permissao](
	[cod_Permissao] [int] IDENTITY(1,1) NOT NULL,
	[cod_PermissaoPai] [int] NULL,
	[nom_Permissao] [varchar](100) NULL,
	[dsc_Permissao] [varchar](max) NULL,
	[nom_Action] [varchar](100) NULL,
	[nom_Controller] [varchar](100) NULL,
	[ind_Ativo] [bit] NULL,
	[cod_Modulo] [int] NULL,
	[ind_Menu] [bit] NULL,
	[cod_PermissaoTipo] [int] NULL,
	[ind_Ordem] [int] NULL,
 CONSTRAINT [PK_TB_GER_Permissao] PRIMARY KEY CLUSTERED 
(
	[cod_Permissao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
/****** Object:  Table [dbo].[TB_GER_PermissaoTipo]    Script Date: 22/01/2016 14:45:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TB_GER_PermissaoTipo](
	[cod_PermissaoTipo] [int] IDENTITY(1,1) NOT NULL,
	[nom_PermissaoTipo] [varchar](300) NULL,
 CONSTRAINT [PK_TB_GER_PermissaoTipo] PRIMARY KEY CLUSTERED 
(
	[cod_PermissaoTipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
/****** Object:  Table [dbo].[TB_GER_Usuario_Perfil]    Script Date: 22/01/2016 14:45:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_GER_Usuario_Perfil](
	[cod_Usuario] [int] NOT NULL,
	[cod_Perfil] [int] NOT NULL,
 CONSTRAINT [PK_TB_GER_Usuario_Perfil] PRIMARY KEY CLUSTERED 
(
	[cod_Usuario] ASC,
	[cod_Perfil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TB_GER_Usuario_Permissao]    Script Date: 22/01/2016 14:45:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TB_GER_Usuario_Permissao](
	[cod_Usuario] [int] NOT NULL,
	[cod_Permissao] [int] NOT NULL,
	[val_Permissao] [int] NULL,
 CONSTRAINT [PK_TB_GER_Usuario_Permissao_1] PRIMARY KEY CLUSTERED 
(
	[cod_Usuario] ASC,
	[cod_Permissao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [PK_TB_GER_Usuario_Permissao] UNIQUE NONCLUSTERED 
(
	[cod_Usuario] ASC,
	[cod_Permissao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TB_GER_Perfil_Permissao]  WITH CHECK ADD  CONSTRAINT [FK_TB_GER_Perfil_Permissao_TB_GER_Perfil] FOREIGN KEY([cod_Perfil])
REFERENCES [dbo].[TB_GER_Perfil] ([cod_Perfil])
GO
ALTER TABLE [dbo].[TB_GER_Perfil_Permissao] CHECK CONSTRAINT [FK_TB_GER_Perfil_Permissao_TB_GER_Perfil]
GO
ALTER TABLE [dbo].[TB_GER_Perfil_Permissao]  WITH CHECK ADD  CONSTRAINT [FK_TB_GER_Perfil_Permissao_TB_GER_Permissao] FOREIGN KEY([cod_Permissao])
REFERENCES [dbo].[TB_GER_Permissao] ([cod_Permissao])
GO
ALTER TABLE [dbo].[TB_GER_Perfil_Permissao] CHECK CONSTRAINT [FK_TB_GER_Perfil_Permissao_TB_GER_Permissao]
GO
ALTER TABLE [dbo].[TB_GER_Permissao]  WITH CHECK ADD  CONSTRAINT [FK_TB_GER_Permissao_TB_GER_PermissaoTipo] FOREIGN KEY([cod_PermissaoTipo])
REFERENCES [dbo].[TB_GER_PermissaoTipo] ([cod_PermissaoTipo])
GO
ALTER TABLE [dbo].[TB_GER_Permissao] CHECK CONSTRAINT [FK_TB_GER_Permissao_TB_GER_PermissaoTipo]
GO
