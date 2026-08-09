using Biblioteca.Web.Data; using Biblioteca.Web.Models.Entities; using Biblioteca.Web.Services; using Microsoft.AspNetCore.Authorization; using Microsoft.AspNetCore.Mvc;
namespace Biblioteca.Web.Controllers;
[Authorize(Roles=DbInitializer.RolAdmin)] public class CategoriasController(ICategoriaService service):Controller
{
 public async Task<IActionResult> Index()=>View(await service.ListarAsync());
 [HttpGet] public IActionResult Create()=>View(new Categoria());
 [HttpPost][ValidateAntiForgeryToken] public async Task<IActionResult> Create(Categoria c){if(await service.ExisteNombreAsync(c.Nombre))ModelState.AddModelError(nameof(c.Nombre),"Ya existe una categoría con ese nombre.");if(!ModelState.IsValid)return View(c);await service.CrearAsync(c);TempData["Mensaje"]="Categoría creada correctamente.";return RedirectToAction(nameof(Index));}
 [HttpGet] public async Task<IActionResult> Edit(int id){var c=await service.ObtenerAsync(id);return c is null?NotFound():View(c);}
 [HttpPost][ValidateAntiForgeryToken] public async Task<IActionResult> Edit(int id,Categoria c){if(id!=c.Id)return BadRequest();if(await service.ExisteNombreAsync(c.Nombre,id))ModelState.AddModelError(nameof(c.Nombre),"Ya existe otra categoría con ese nombre.");if(!ModelState.IsValid)return View(c);if(!await service.ActualizarAsync(c))return NotFound();TempData["Mensaje"]="Categoría actualizada correctamente.";return RedirectToAction(nameof(Index));}
 [HttpGet] public async Task<IActionResult> Delete(int id){var c=await service.ObtenerAsync(id);return c is null?NotFound():View(c);}
 [HttpPost,ActionName("Delete")][ValidateAntiForgeryToken] public async Task<IActionResult> DeleteConfirmed(int id){var r=await service.EliminarAsync(id);if(!r.eliminado){TempData["Error"]=r.error??"Categoría no encontrada.";return RedirectToAction(nameof(Index));}TempData["Mensaje"]="Categoría eliminada correctamente.";return RedirectToAction(nameof(Index));}
}
