using Biblioteca.Web.Services; using Microsoft.AspNetCore.Mvc;
namespace Biblioteca.Web.Controllers;
public class CatalogoController(ICatalogoService service) : Controller
{
 [HttpGet] public async Task<IActionResult> Index(string? busqueda,int? categoriaId)=>View(await service.ObtenerCatalogoAsync(busqueda,categoriaId));
 [HttpGet] public async Task<IActionResult> Details(int id){var libro=await service.ObtenerDetalleAsync(id);return libro is null?NotFound():View(libro);}
}
