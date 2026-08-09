
using Biblioteca.Web.Data;
using Biblioteca.Web.Models.ViewModels;
using Biblioteca.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Biblioteca.Web.Controllers;

[Authorize(Roles = DbInitializer.RolAdmin)]
public class PrestamosController(IPrestamoService service) : Controller

{
    public async Task<IActionResult> Index() => View(await service.ListarAsync());

    [HttpGet]
    public async Task<IActionResult> Create() => View(await service.ObtenerFormularioCreacionAsync());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PrestamoCreateViewModel modelo)
    
    {
        var (ok, error) = await service.RegistrarPrestamoAsync(modelo.LibroId, modelo.UsuarioId, modelo.FechaDevolucionEsperada);
        if (!ok)
        {
            TempData["Error"] = error;
            var vm = await service.ObtenerFormularioCreacionAsync();
            return View(vm);
        }
        TempData["Mensaje"] = "Préstamo registrado correctamente.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Devolver(int id, DateTime? fechaDevolucion)

    {
        var (ok, error) = await service.RegistrarDevolucionAsync(id, fechaDevolucion);
        TempData[ok ? "Mensaje" : "Error"] = ok ? "Devolución registrada correctamente." : error;
        return RedirectToAction(nameof(Index));
    }

}