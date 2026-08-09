# Sistema de Gestión de Biblioteca

Proyecto del curso Programación IV - UIA
Docente: Caleb Oreamuno López

## Descripción

Sistema web para administrar el catálogo de libros, autores, categorías,
usuarios y préstamos de una biblioteca. Cuenta con un módulo público de
consulta y un módulo administrativo con autenticación.

## Equipo

- Keylor – Configuración inicial de Entity Framework (DbContext, migraciones), gestión de libros y usuarios, autenticación y autorización
- Denisse – Gestión de autores y categorías, documento escrito
- Steban Flores – Módulo público (catálogo, búsqueda, disponibilidad), presentación
- Minor Rojas Cubero – Gestión de préstamos, repositorio GitHub, despliegue

## Tecnologías

- ASP.NET MVC
- Entity Framework
- SQL Server
- C#

## Estado del proyecto

***En desarrollo*** — Entrega: Semana 13, 11 de agosto de 2026

## Instalación y ejecución

1. Clonar el repositorio:
```bash
   git clone https://github.com/MinorRojas/sistema_gestion_biblioteca-programacion_IV.git
```
2. Abrir la solución en Visual Studio.
3. Restaurar los paquetes NuGet (se hace automáticamente al abrir el proyecto, o clic derecho en la solución → Restore NuGet Packages).
4. Configurar la cadena de conexión a SQL Server en `appsettings.json` (por defecto usa LocalDB, instancia `(localdb)\MSSQLLocalDB`).
5. La base de datos se crea automáticamente al ejecutar el proyecto por primera vez, junto con datos de prueba y el usuario administrador (correo `admin@biblioteca.com`, contraseña `Admin123`).
6. Ejecutar el proyecto (F5 en Visual Studio, o `dotnet run` desde la terminal).

## Estructura del proyecto

- `Controllers/` – Controladores MVC (Libros, Autores, Categorías, Usuarios, Préstamos)
- `Models/` – Modelos y entidades del sistema
- `Views/` – Vistas Razor (módulo público y administrativo)
- `Data/` – DbContext y configuración de Entity Framework
- `wwwroot/` – Archivos estáticos (CSS, JS, imágenes)

## Funcionalidades

### Módulo Público
- Consultar catálogo de libros
- Buscar libros por título o autor
- Filtrar libros por categoría
- Ver detalle de un libro
- Consultar disponibilidad de ejemplares

### Módulo Administrativo (requiere autenticación)
- Gestión de libros (crear, modificar, eliminar, consultar)
- Gestión de autores (crear, modificar, eliminar)
- Gestión de categorías (crear, modificar, eliminar)
- Gestión de usuarios (crear, modificar, consultar)
- Gestión de préstamos (registrar préstamo, registrar devolución, consultar historial)

## Despliegue

El sistema está desplegado en Azure App Service:
https://biblioteca-grupo5-bgemd4dtc0hug6ar.mexicocentral-01.azurewebsites.net
