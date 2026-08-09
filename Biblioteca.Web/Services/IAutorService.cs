using Biblioteca.Web.Models.Entities;
namespace Biblioteca.Web.Services;
public interface IAutorService { Task<List<Autor>> ListarAsync(); Task<Autor?> ObtenerAsync(int id); Task<bool> ExisteNombreAsync(string nombre,int? excluir=null); Task CrearAsync(Autor autor); Task<bool> ActualizarAsync(Autor autor); Task<(bool eliminado,string? error)> EliminarAsync(int id); }
