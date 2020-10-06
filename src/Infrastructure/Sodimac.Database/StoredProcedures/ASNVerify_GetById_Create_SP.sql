CREATE PROCEDURE [dbo].[ASNVerify_GetById]
	@Id int
AS
BEGIN
	SELECT * FROM dbo.ASNVerify a
	WHERE Id = @Id
END
GO


