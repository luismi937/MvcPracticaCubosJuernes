namespace MvcPracticaCubosJuernes.Models
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public int IdCubo { get; set; }
        public int Cantidad { get; set; }
        public int Precio { get; set; }
        public DateTime FechaPedido { get; set; }
    }
}
