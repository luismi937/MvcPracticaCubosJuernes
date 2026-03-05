using Microsoft.EntityFrameworkCore;
using MvcPracticaCubosJuernes.Data;
using MvcPracticaCubosJuernes.Models;

namespace MvcPracticaCubosJuernes.Repositories
{
    public class RepositoryCubos
    {
        private readonly CubosContext context;

        public RepositoryCubos(CubosContext context)
        {
            this.context = context;
        }

        public async Task<List<Cubo>> GetCubosAsync()
        {
            return await this.context.Cubos.ToListAsync();
        }

        public async Task<Cubo?> FindCuboAsync(int idCubo)
        {
            return await this.context.Cubos
                .FirstOrDefaultAsync(c => c.IdCubo == idCubo);
        }

        public async Task<List<Cubo>> GetCubosFavoritosAsync(List<int> idsCubos)
        {
            var consulta = from datos in this.context.Cubos
                           where idsCubos.Contains(datos.IdCubo)
                           select datos;
            return await consulta.ToListAsync();
        }

        public async Task InsertCuboAsync(Cubo cubo)
        {
            int maxId = await this.context.Cubos.MaxAsync(c => (int?)c.IdCubo) ?? 0;
            cubo.IdCubo = maxId + 1;
            
            this.context.Cubos.Add(cubo);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateCuboAsync(Cubo cubo)
        {
            this.context.Cubos.Update(cubo);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteCuboAsync(int idCubo)
        {
            var cubo = await FindCuboAsync(idCubo);
            if (cubo != null)
            {
                this.context.Cubos.Remove(cubo);
                await this.context.SaveChangesAsync();
            }
        }

        public async Task InsertCompraAsync(int idCubo, int cantidad, int precio)
        {
            int maxId = await this.context.Compras.MaxAsync(c => (int?)c.IdCompra) ?? 0;
            
            var compra = new Compra
            {
                IdCompra = maxId + 1,
                IdCubo = idCubo,
                Cantidad = cantidad,
                Precio = precio,
                FechaPedido = DateTime.Now
            };

            this.context.Compras.Add(compra);
            await this.context.SaveChangesAsync();
        }
    }
}
