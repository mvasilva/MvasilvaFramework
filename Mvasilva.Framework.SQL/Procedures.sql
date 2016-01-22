
CREATE PROCEDURE [dbo].[PR_GER_Permissao_SEL_ByUsuario]
	@cod_Usuario INT,
	@nom_Action VARCHAR(100),
	@nom_Controller VARCHAR(100),
	@cod_Modulo INT = null
AS
BEGIN
	DECLARE @val_PermissaoPerfil INT = 0,
			@val_PermissaoUsuario  INT = 0,
			@cod_Permissao INT,
			@cod_Modulo_Acesso INT = 0,
			@nom_Permissao varchar(100),
			@dsc_Permissao varchar(MAX)
			
	SELECT 
		@cod_Permissao = P.cod_Permissao,
		@cod_Modulo_Acesso = M.cod_Modulo,
		@nom_Permissao = P.nom_Permissao,
		@dsc_Permissao = P.dsc_Permissao
	FROM
		TB_GER_Permissao P
	JOIN
		TB_GER_Modulo M 
			ON P.cod_Modulo = M.cod_Modulo
			AND M.ind_Ativo = 1
	WHERE
		P.nom_Action  = @nom_Action
		AND P.nom_Controller = @nom_Controller
		AND P.ind_Ativo = 1
		AND (M.cod_Modulo = @cod_Modulo OR @cod_Modulo IS NULL)

	SELECT 
		@val_PermissaoPerfil = @val_PermissaoPerfil | PP.val_Permissao 
	FROM 
		PORTAL..TB_GER_Usuario U
	JOIN
		TB_GER_Usuario_Perfil UP ON
			U.cod_Usuario = UP.cod_Usuario
	JOIN
		TB_GER_Perfil PE
			ON UP.cod_Perfil = PE.cod_Perfil
	JOIN
		TB_GER_Perfil_Permissao PP
			ON PP.cod_Perfil = PE.cod_Perfil
	WHERE
		PE.ind_Ativo = 1
		AND U.cod_Usuario = @cod_Usuario
		AND PP.cod_Permissao = @cod_Permissao

	SELECT
		@val_PermissaoUsuario = UP.val_Permissao
	FROM
		PORTAL..TB_GER_Usuario U
	JOIN
		TB_GER_Usuario_Permissao UP
			ON U.cod_Usuario = UP.cod_Usuario
	WHERE
		UP.cod_Usuario = @cod_Usuario
	AND
		UP.cod_Permissao = @cod_Permissao

	SELECT
		@val_PermissaoPerfil | @val_PermissaoUsuario val_Permissao,
		@cod_Modulo_Acesso cod_Modulo,
		@nom_Permissao nom_Permissao,
		@dsc_Permissao dsc_Permissao
END

go


CREATE PROCEDURE [dbo].[PR_GER_UsuarioLogin_SEL]
	@nom_Email VARCHAR(100),
	@nom_Senha VARCHAR(500)
AS
BEGIN
	DECLARE @cod_Usuario INT
	
	SELECT 
		@cod_Usuario = cod_Usuario 
	FROM 
		TB_GER_Usuario 
	WHERE 
		nom_Email = @nom_Email 
		AND nom_Senha = @nom_Senha


    IF @cod_Usuario IS NOT NULL OR @cod_Usuario <> 0 BEGIN
		SELECT 
			cod_Usuario, 
			nom_Usuario,
			cod_Perfil, 
			nom_Email,
			cod_Status
		FROM 
			TB_GER_Usuario
		WHERE
			cod_Usuario = @cod_Usuario
	END 
	ELSE BEGIN
			SELECT 
			-1 cod_Usuario, 
			'' nom_Usuario,
			0 cod_Perfil, 
			'' nom_Email,
			-1 cod_Status
	END

	
END







