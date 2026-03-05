using Microsoft.AspNetCore.Mvc;
using MvcPracticaCubosJuernes.Extensions;
using MvcPracticaCubosJuernes.Models;
using MvcPracticaCubosJuernes.Repositories;

namespace MvcPracticaCubosJuernes.Controllers
{
    public class CarritoController : Controller
    {
        private readonly RepositoryCubos repo;

        public CarritoController(RepositoryCubos repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<CarritoItem> carrito = HttpContext.Session.GetObject<List<CarritoItem>>("CARRITO") ?? new List<CarritoItem>();
            return View(carrito);
        }

        public async Task<IActionResult> Agregar(int id)
        {
            Cubo? cubo = await this.repo.FindCuboAsync(id);
            if (cubo == null)
            {
                return RedirectToAction("Index", "Cubos");
            }

            List<CarritoItem> carrito = HttpContext.Session.GetObject<List<CarritoItem>>("CARRITO") ?? new List<CarritoItem>();
            
            CarritoItem? item = carrito.FirstOrDefault(x => x.Cubo.IdCubo == id);
            if (item != null)
            {
                item.Cantidad++;
            }
            else
            {
                carrito.Add(new CarritoItem
                {
                    Cubo = cubo,
                    Cantidad = 1
                });
            }

            HttpContext.Session.SetObject("CARRITO", carrito);
            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id)
        {
            List<CarritoItem> carrito = HttpContext.Session.GetObject<List<CarritoItem>>("CARRITO") ?? new List<CarritoItem>();
            CarritoItem? item = carrito.FirstOrDefault(x => x.Cubo.IdCubo == id);
            
            if (item != null)
            {
                carrito.Remove(item);
                HttpContext.Session.SetObject("CARRITO", carrito);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> FinalizarCompra()
        {
            List<CarritoItem> carrito = HttpContext.Session.GetObject<List<CarritoItem>>("CARRITO") ?? new List<CarritoItem>();
            
            foreach (var item in carrito)
            {
                await this.repo.InsertCompraAsync(item.Cubo.IdCubo, item.Cantidad, item.Cubo.Precio);
            }

            HttpContext.Session.Remove("CARRITO");
            return RedirectToAction("Confirmacion");
        }

        public IActionResult Confirmacion()
        {
            return View();
        }
    }
}
