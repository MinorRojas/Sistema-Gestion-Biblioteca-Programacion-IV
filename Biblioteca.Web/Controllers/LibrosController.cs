using Biblioteca.Web.Data;
using Biblioteca.Web.Models.Entities;
using Biblioteca.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Web.Controllers;

[Authorize(Roles = DbInitializer.RolAdmin)]
public class LibrosController(ILibroService service) : Controller
{
    public async Task<IActionResult> Index(string? busqueda)
    {
        ViewBag.Busqueda = busqueda;
        return View(await service.ListarAsync(busqueda));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var libro = await service.ObtenerAsync(id);
        return libro is null ? NotFound() : View(libro);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await CargarSelectsAsync();
        return View(new Libro());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Libro libro)
    {
        ValidarLibro(libro);
        if (await service.ExisteIsbnAsync(libro.Isbn))
            ModelState.AddModelError(nameof(libro.Isbn), "Ya existe un libro con ese ISBN.");

        if (!ModelState.IsValid)
        {
            await CargarSelectsAsync();
            return View(libro);
        }

        await service.CrearAsync(libro);
        TempData["Mensaje"] = "Libro creado correctamente.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var libro = await service.ObtenerAsync(id);
        if (libro is null) return NotFound();
        await CargarSelectsAsync();
        return View(libro);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Libro libro)
    {
        if (id != libro.Id) return BadRequest();

        ValidarLibro(libro);
        if (await service.ExisteIsbnAsync(libro.Isbn, libro.Id))
            ModelState.AddModelError(nameof(libro.Isbn), "Ya existe otro libro con ese ISBN.");

        if (!ModelState.IsValid)
        {
            await CargarSelectsAsync();
            return View(libro);
        }

        if (!await service.ActualizarAsync(libro)) return NotFound();
        TempData["Mensaje"] = "Libro actualizado correctamente.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var libro = await service.ObtenerAsync(id);
        return libro is null ? NotFound() : View(libro);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var resultado = await service.EliminarAsync(id);
        if (!resultado.eliminado)
        {
            TempData["Error"] = resultado.error ?? "No se pudo eliminar el libro.";
            return RedirectToAction(nameof(Index));
        }

        TempData["Mensaje"] = "Libro eliminado correctamente.";
        return RedirectToAction(nameof(Index));
    }

    private void ValidarLibro(Libro libro)
    {
        if (libro.AnioPublicacion < 0 || libro.AnioPublicacion > DateTime.Now.Year)
            ModelState.AddModelError(nameof(libro.AnioPublicacion), "Ingrese un año de publicación válido.");

        if (libro.CantidadEjemplares < 0)
            ModelState.AddModelError(nameof(libro.CantidadEjemplares), "La cantidad no puede ser negativa.");

        if (libro.EjemplaresDisponibles < 0)
            ModelState.AddModelError(nameof(libro.EjemplaresDisponibles), "Los ejemplares disponibles no pueden ser negativos.");

        if (libro.EjemplaresDisponibles > libro.CantidadEjemplares)
            ModelState.AddModelError(nameof(libro.EjemplaresDisponibles), "Los disponibles no pueden superar el total.");
    }

    private async Task CargarSelectsAsync()
    {
        ViewBag.Autores = await service.ListarAutoresAsync();
        ViewBag.Categorias = await service.ListarCategoriasAsync();
    }
}
