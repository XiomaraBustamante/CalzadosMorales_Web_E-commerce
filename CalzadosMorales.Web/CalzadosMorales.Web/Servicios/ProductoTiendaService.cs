using CalzadosMorales.Web.Models;
using CalzadosMorales.Web.Repositorio;

namespace CalzadosMorales.Web.Servicios
{
    public class ProductoTiendaService
    {
        private readonly ProductoTiendaRepository _productoTiendaRepository;

        public ProductoTiendaService(ProductoTiendaRepository productoTiendaRepository)
        {
            _productoTiendaRepository = productoTiendaRepository;
        }

        public List<ProductoTienda> ObtenerCatalogoPorCategoria(string categoria)
        {
            return _productoTiendaRepository.ListarCatalogoPorCategoria(categoria);
        }
    }
}