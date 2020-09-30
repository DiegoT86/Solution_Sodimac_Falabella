USE [SodimacDB]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'ASNVerify_GetById')
	DROP PROCEDURE [dbo].[ASNVerify_GetById];
GO


CREATE PROCEDURE [dbo].[ASNVerify_GetById]
	@Id int
AS
BEGIN
	SELECT * FROM dbo.ASNVerify a
	WHERE Id = @Id
END
GO


