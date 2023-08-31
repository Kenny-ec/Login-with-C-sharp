USE Proyectos
GO

CREATE TABLE Usuarios(
	idUsuario INTEGER PRIMARY KEY IDENTITY(1,1),
	nombre VARCHAR(20) NOT NULL,
	clave VARCHAR(50) NOT NULL
)

------------------STORE PROCEDURE-----------------

CREATE PROC sp_verificarCredenciales
	@Nombre VARCHAR(20),
	@Clave VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT CAST(COUNT(*) AS BIT) AS ExisteUsuario
	FROM Usuarios
	WHERE nombre = @Nombre AND clave = @clave
END
GO

EXEC sp_verificarCredenciales 'kepizarro', '1234'


CREATE PROC sp_registrarUsuario
	@Nombre VARCHAR(20),
	@Clave VARCHAR(50)
AS
BEGIN
	INSERT INTO Usuarios(nombre,clave) VALUES(@Nombre, @Clave)
END
GO

EXEC sp_registrarUsuario 'kepizarro','1234'

SELECT *FROM Usuarios