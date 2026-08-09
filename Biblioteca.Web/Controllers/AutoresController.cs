using Biblioteca.Web.Data; using Biblioteca.Web.Models.Entities; using Biblioteca.Web.Services; using Microsoft.AspNetCore.Authorization; using Microsoft.AspNetCore.Mvc;
namespace Biblioteca.Web.Controllers;
[Authorize(Roles=DbInitializer.RolAdmin)] public class AutoresController(IAutorService service):Controller
{
 public async Task<IActionResult> Index()=>View(await service.ListarAsync());
 [HttpGet] public IActionResult Create()=>View(new Autor());
 [HttpPost][ValidateAntiForgeryToken] public async Task<IActionResult> Create(Autor autor){if(await service.ExisteNombreAsync(autor.Nombre))ModelState.AddModelError(nameof(autor.Nombre),"Ya existe un autor con ese nombre.");if(!ModelState.IsValid)return View(autor);await service.CrearAsync(autor);TempData["Mensaje"]="Autor creado correctamente.";return RedirectToAction(nameof(Index));}
 [HttpGet] public async Task<IActionResult> Edit(int id){var a=await service.ObtenerAsync(id);return a is null?NotFound():View(a);}
 [HttpPost][ValidateAntiForgeryToken] public async Task<IActionResult> Edit(int id,Autor autor){if(id!=autor.Id)return BadRequest();if(await service.ExisteNombreAsync(autor.Nombre,id))ModelState.AddModelError(nameof(autor.Nombre),"Ya existe otro autor con ese nombre.");if(!ModelState.IsValid)return View(autor);if(!await service.ActualizarAsync(autor))return NotFound();TempData["Mensaje"]="Autor actualizado correctamente.";return RedirectToAction(nameof(Index));}
 [HttpGet] public async Task<IActionResult> Delete(int id){var a=await service.ObtenerAsync(id);return a is null?NotFound():View(a);}
 [HttpPost,ActionName("Delete")][ValidateAntiForgeryToken] public async Task<IActionResult> DeleteConfirmed(int id){var r=await service.EliminarAsync(id);if(!r.eliminado){TempData["Error"]=r.error??"Autor no encontrado.";return RedirectToAction(nameof(Index));}TempData["Mensaje"]="Autor eliminado correctamente.";return RedirectToAction(nameof(Index));}
}
