using CalzadosMorales.Web.Models;
using CalzadosMorales.Web.Repositorio;

namespace CalzadosMorales.Web.Servicios
{
    public class ProductoService
    {
        private readonly ProductoRepository _productoRepository;

        public ProductoService(ProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public List<Producto> ListarProductos()
        {
            return _productoRepository.ListarProductos();
        }

        public Producto ObtenerProductoPorId(int idProducto)
        {
            return _productoRepository.ObtenerProductoPorId(idProducto);
        }

        public int RegistrarProducto(Producto producto)
        {
            return _productoRepository.RegistrarProducto(producto);
        }

        public void ActualizarProducto(Producto producto)
        {
            _productoRepository.ActualizarProducto(producto);
        }

        public void CambiarEstadoProducto(int idProducto, bool estado)
        {
            _productoRepository.CambiarEstadoProducto(idProducto, estado);
        }

        public void GuardarProductoTallaStock(int idProducto, int idTalla, int stock)
        {
            _productoRepository.GuardarProductoTallaStock(idProducto, idTalla, stock);
        }

        public void RegistrarImagen(int idProducto, string imagenUrl)
        {
            _productoRepository.RegistrarImagen(idProducto, imagenUrl);
        }
    }
}