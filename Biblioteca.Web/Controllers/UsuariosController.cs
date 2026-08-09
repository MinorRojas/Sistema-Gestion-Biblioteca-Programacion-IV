using Biblioteca.Web.Data;
using Biblioteca.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Web.Controllers;

[Authorize(Roles = DbInitializer.RolAdmin)]
public class UsuariosController(
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager) : Controller
{
    public async Task<IActionResult> Index(string? busqueda)
    {
        var usuarios = userManager.Users.AsQueryable();
        if (!string.IsNullOrWhiteSpace(busqueda))
        {
            var texto = busqueda.Trim();
            usuarios = usuarios.Where(x => x.Email != null && x.Email.Contains(texto));
        }

        ViewBag.Busqueda = busqueda;
        return View(await usuarios.OrderBy(x => x.Email).ToListAsync());
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        var usuario = await userManager.FindByIdAsync(id);
        if (usuario is null) return NotFound();
        ViewBag.Roles = await userManager.GetRolesAsync(usuario);
        return View(usuario);
    }

    [HttpGet]
    public IActionResult Create() => View(new UsuarioCrearViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UsuarioCrearViewModel modelo)
    {
        if (!ModelState.IsValid) return View(modelo);

        if (await userManager.FindByEmailAsync(modelo.Email) is not null)
        {
            ModelState.AddModelError(nameof(modelo.Email), "Ya existe un usuario con ese correo.");
            return View(modelo);
        }

        var usuario = new IdentityUser
        {
            UserName = modelo.Email,
            Email = modelo.Email,
            EmailConfirmed = true
        };

        var resultado = await userManager.CreateAsync(usuario, modelo.Password);
        if (!resultado.Succeeded)
        {
            foreach (var error in resultado.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return View(modelo);
        }

        if (modelo.EsAdministrador)
            await userManager.AddToRoleAsync(usuario, DbInitializer.RolAdmin);

        TempData["Mensaje"] = "Usuario creado correctamente.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var usuario = await userManager.FindByIdAsync(id);
        if (usuario is null) return NotFound();

        var roles = await userManager.GetRolesAsync(usuario);
        return View(new UsuarioEditarViewModel
        {
            Id = usuario.Id,
            Email = usuario.Email ?? string.Empty,
            EsAdministrador = roles.Contains(DbInitializer.RolAdmin)
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, UsuarioEditarViewModel modelo)
    {
        if (id != modelo.Id) return BadRequest();
        if (!ModelState.IsValid) return View(modelo);

        var usuario = await userManager.FindByIdAsync(id);
        if (usuario is null) return NotFound();

        var otro = await userManager.FindByEmailAsync(modelo.Email);
        if (otro is not null && otro.Id != usuario.Id)
        {
            ModelState.AddModelError(nameof(modelo.Email), "Ese correo ya pertenece a otro usuario.");
            return View(modelo);
        }

        usuario.Email = modelo.Email;
        usuario.UserName = modelo.Email;
        var resultado = await userManager.UpdateAsync(usuario);

        if (!resultado.Succeeded)
        {
            foreach (var error in resultado.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return View(modelo);
        }

        if (!string.IsNullOrWhiteSpace(modelo.NuevaPassword))
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(usuario);
            var passwordResult = await userManager.ResetPasswordAsync(usuario, token, modelo.NuevaPassword);
            if (!passwordResult.Succeeded)
            {
                foreach (var error in passwordResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return View(modelo);
            }
        }

        var esAdminActual = await userManager.IsInRoleAsync(usuario, DbInitializer.RolAdmin);
        if (modelo.EsAdministrador && !esAdminActual)
            await userManager.AddToRoleAsync(usuario, DbInitializer.RolAdmin);
        else if (!modelo.EsAdministrador && esAdminActual)
        {
            if (usuario.Email == User.Identity?.Name)
            {
                ModelState.AddModelError(string.Empty, "No puede quitarse el rol de administrador mientras está conectado.");
                return View(modelo);
            }
            await userManager.RemoveFromRoleAsync(usuario, DbInitializer.RolAdmin);
        }

        TempData["Mensaje"] = "Usuario actualizado correctamente.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        var usuario = await userManager.FindByIdAsync(id);
        if (usuario is null) return NotFound();

        if (usuario.Email == User.Identity?.Name)
        {
            TempData["Error"] = "No puede eliminar el usuario con el que está conectado.";
            return RedirectToAction(nameof(Index));
        }

        var resultado = await userManager.DeleteAsync(usuario);
        TempData[resultado.Succeeded ? "Mensaje" : "Error"] = resultado.Succeeded
            ? "Usuario eliminado correctamente."
            : string.Join(", ", resultado.Errors.Select(x => x.Description));

        return RedirectToAction(nameof(Index));
    }
}
