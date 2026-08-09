PROYECTO BIBLIOTECA - INTEGRACIÓN DE MÓDULOS

Proyecto ASP.NET Core MVC + EF Core + SQL Server + Identity (.NET 10).

PARTES EXISTENTES DEL GRUPO
- Catálogo público, búsqueda, filtro y detalle.
- CRUD de autores.
- CRUD de categorías.
- Gestión de préstamos.
- Identity y rol Admin.

PARTE INTEGRADA PARA KEYLOR
- CRUD completo de libros.
- Crear, editar, consultar y eliminar libros.
- Validaciones de ISBN, ejemplares y año.
- Protección de administración con [Authorize(Roles = "Admin")].
- CRUD de usuarios con Identity.
- Cambio de correo, rol Admin y contraseña opcional.
- Prueba de integración: los libros guardados en Libros son consultados por CatalogoService y aparecen en el catálogo público.
- Menús de administración para Libros y Usuarios.

USUARIO ADMINISTRADOR
Correo: admin@biblioteca.com
Contraseña: Admin123

BASE DE DATOS
La conexión está en appsettings.json y usa SQL Server LocalDB:
BibliotecaGrupo5

IMPORTANTE SOBRE EF CORE
El proyecto original usaba EnsureCreatedAsync() para crear la BD. Para esta entrega se conserva ese mecanismo para no romper una BD existente.

Para dejar migraciones físicas en el proyecto desde Visual Studio:
1. Tools > NuGet Package Manager > Package Manager Console.
2. Add-Migration ModulosAdministracion
3. Update-Database

O terminal:
 dotnet ef migrations add ModulosAdministracion
 dotnet ef database update

Si se decide usar migraciones exclusivamente, después de crear la migración puede cambiarse EnsureCreatedAsync() por MigrateAsync() en DbInitializer.

PRUEBAS RECOMENDADAS
1. Iniciar sesión como admin@biblioteca.com / Admin123.
2. Ir a Administración > Libros.
3. Crear un libro nuevo.
4. Volver a Catálogo y verificar que aparece.
5. Editarlo y comprobar los cambios.
6. Intentar eliminarlo.
7. Ir a Usuarios, crear un usuario y modificarlo.
8. Cerrar sesión y comprobar que las pantallas administrativas solicitan autenticación.
