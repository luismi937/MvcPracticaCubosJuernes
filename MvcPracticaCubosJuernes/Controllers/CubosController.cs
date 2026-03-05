using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MvcPracticaCubosJuernes.Models;
using MvcPracticaCubosJuernes.Repositories;

namespace MvcPracticaCubosJuernes.Controllers
{
    public class CubosController : Controller
    {
        private readonly RepositoryCubos repo;
        private readonly IMemoryCache memoryCache;
        private readonly IWebHostEnvironment environment;

        public CubosController(RepositoryCubos repo, IMemoryCache memoryCache, IWebHostEnvironment environment)
        {
            this.repo = repo;
            this.memoryCache = memoryCache;
            this.environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            List<Cubo> cubos = await this.repo.GetCubosAsync();
            return View(cubos);
        }

        public async Task<IActionResult> Details(int id)
        {
            Cubo? cubo = await this.repo.FindCuboAsync(id);
            if (cubo == null)
            {
                return RedirectToAction("Index");
            }
            return View(cubo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cubo cubo, IFormFile? imagen)
        {
            if (imagen != null && imagen.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName);
                string path = Path.Combine(this.environment.WebRootPath, "images", "cubos", fileName);
                
                // Crear directorio si no existe
                string directory = Path.GetDirectoryName(path)!;
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imagen.CopyToAsync(stream);
                }
                
                cubo.Imagen = "/images/cubos/" + fileName;
            }
            else
            {
                cubo.Imagen = "/images/cubos/default.jpg";
            }

            await this.repo.InsertCuboAsync(cubo);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Cubo? cubo = await this.repo.FindCuboAsync(id);
            if (cubo == null)
            {
                return RedirectToAction("Index");
            }
            return View(cubo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Cubo cubo, IFormFile? imagen)
        {
            if (imagen != null && imagen.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName);
                string path = Path.Combine(this.environment.WebRootPath, "images", "cubos", fileName);
                
                string directory = Path.GetDirectoryName(path)!;
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imagen.CopyToAsync(stream);
                }
                
                cubo.Imagen = "/images/cubos/" + fileName;
            }

            await this.repo.UpdateCuboAsync(cubo);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.repo.DeleteCuboAsync(id);
            return RedirectToAction("Index");
        }

        public IActionResult AgregarFavorito(int id)
        {
            List<int> favoritos = this.memoryCache.Get<List<int>>("FAVORITOS") ?? new List<int>();
            
            if (!favoritos.Contains(id))
            {
                favoritos.Add(id);
                this.memoryCache.Set("FAVORITOS", favoritos);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Favoritos()
        {
            List<int> idsFavoritos = this.memoryCache.Get<List<int>>("FAVORITOS") ?? new List<int>();
            
            if (idsFavoritos.Count == 0)
            {
                return View(new List<Cubo>());
            }

            List<Cubo> favoritos = await this.repo.GetCubosFavoritosAsync(idsFavoritos);
            return View(favoritos);
        }

        public IActionResult EliminarFavorito(int id)
        {
            List<int> favoritos = this.memoryCache.Get<List<int>>("FAVORITOS") ?? new List<int>();
            favoritos.Remove(id);
            this.memoryCache.Set("FAVORITOS", favoritos);

            return RedirectToAction("Favoritos");
        }
    }
}
