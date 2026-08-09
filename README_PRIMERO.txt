PROYECTO BIBLIOTECA - PARTE DE STEBAN Y DENISSE

Este ZIP sí es un proyecto para abrir en Visual Studio.

1. Abra Visual Studio 2026/2022 compatible con .NET 10.
2. Abra el archivo Biblioteca.slnx.
3. Espere que Visual Studio restaure los paquetes NuGet.
4. Ejecute con el botón HTTPS.
5. La base de datos se crea automáticamente en SQL Server LocalDB.

USUARIO ADMINISTRADOR
Correo: admin@biblioteca.com
Contraseña: Admin123

PARTE DE STEBAN
- Catálogo público.
- Búsqueda por título o autor usando LINQ.
- Filtro por categoría.
- Detalle del libro.
- Disponibilidad de ejemplares.
- Diseño de las pantallas públicas.

PARTE DE DENISSE
- CRUD completo de autores.
- CRUD completo de categorías.
- Validación de nombres repetidos.
- Protección de pantallas administrativas con Identity y rol Admin.

ESTRUCTURA TOMADA DEL EJEMPLO DEL PROFESOR
- ASP.NET Core MVC.
- Entity Framework Core y SQL Server.
- Entidades y configuraciones separadas.
- ApplicationDbContext.
- Servicios e interfaces con inyección de dependencias.
- Controladores delgados.
- ViewModels para el catálogo.
- async/await y métodos asíncronos de EF.
- Identity para autenticación.
- Patrón Post-Redirect-Get y AntiForgeryToken.

IMPORTANTE
No incluye todavía los módulos de libros administrativos, usuarios ni préstamos de los otros compañeros. Sí incluye la entidad Libro y datos de prueba para que la parte pública de Steban funcione.
