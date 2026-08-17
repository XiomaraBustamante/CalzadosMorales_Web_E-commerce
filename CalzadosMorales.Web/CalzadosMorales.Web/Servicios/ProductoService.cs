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
            try
            {
                return _productoRepository.ListarProductos();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el servicio al listar productos: " + ex.Message);
            }
        }

        public Producto ObtenerProductoPorId(int idProducto)
        {
            try
            {
                if (idProducto <= 0)
                    throw new ArgumentException("El ID del producto debe ser mayor a cero.");

                return _productoRepository.ObtenerProductoPorId(idProducto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el servicio al obtener el producto: " + ex.Message);
            }
        }

        public int RegistrarProducto(Producto producto)
        {
            try
            {
                // Validación adicional de negocio opcional
                if (producto == null)
                    throw new ArgumentNullException(nameof(producto), "Los datos del producto no pueden estar vacíos.");

                return _productoRepository.RegistrarProducto(producto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el producto en el servicio: " + ex.Message);
            }
        }

        public void ActualizarProducto(Producto producto)
        {
            try
            {
                if (producto == null || producto.IdProducto <= 0)
                    throw new ArgumentException("ID de producto inválido para actualizar.");

                _productoRepository.ActualizarProducto(producto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el producto en el servicio: " + ex.Message);
            }
        }

        public void CambiarEstadoProducto(int idProducto, bool estado)
        {
            try
            {
                if (idProducto <= 0)
                    throw new ArgumentException("ID de producto no válido.");

                _productoRepository.CambiarEstadoProducto(idProducto, estado);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar el estado del producto: " + ex.Message);
            }
        }

        public void GuardarProductoTallaStock(int idProducto, int idTalla, int stock)
        {
            try
            {
                if (stock < 0)
                    throw new ArgumentException("El stock no puede ser negativo.");

                _productoRepository.GuardarProductoTallaStock(idProducto, idTalla, stock);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la talla y stock: " + ex.Message);
            }
        }

        public void RegistrarImagen(int idProducto, string imagenUrl, int orden)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(imagenUrl))
                    throw new ArgumentException("La URL de la imagen es obligatoria.");

                _productoRepository.RegistrarImagen(idProducto, imagenUrl, orden);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar la imagen: " + ex.Message);
            }
        }

        public void LimpiarTallasProducto(int idProducto)
        {
            try
            {
                _productoRepository.LimpiarTallasProducto(idProducto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al limpiar las tallas: " + ex.Message);
            }
        }

        // --- Métodos de gestión de imágenes ---

        public List<ProductoImagen> ListarImagenesPorProducto(int idProducto)
        {
            try
            {
                return _productoRepository.ListarImagenesPorProducto(idProducto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las imágenes del producto: " + ex.Message);
            }
        }

        public void ActualizarImagen(int idImagen, string imagenUrl)
        {
            try
            {
                _productoRepository.ActualizarImagen(idImagen, imagenUrl);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la imagen: " + ex.Message);
            }
        }
    }
}