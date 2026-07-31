using CalzadosMorales.Web.Models;
using CalzadosMorales.Web.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace CalzadosMorales.Web.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ProductoService _productoService;

        public ProductoController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        // 1. LISTAR PRODUCTOS
        public IActionResult Index()
        {
            var lista = _productoService.ListarProductos();
            return View(lista);
        }

        // OBTENER POR ID (Devuelve JSON para consultas rápidas o modales)
        [HttpGet]
        public IActionResult ObtenerPorId(int id)
        {
            var producto = _productoService.ObtenerProductoPorId(id);
            return Json(producto);
        }

        // 2. REGISTRAR - Vista GET
        [HttpGet]
        public IActionResult Registrar()
        {
            return View();
        }

        // 2. REGISTRAR - Acción POST (Incluyendo datos, tallas/stock e imagen)
        [HttpPost]
        public IActionResult Registrar(Producto producto, List<ProductoTalla> listaTallasStock, string imagenUrl)
        {
            if (ModelState.IsValid)
            {
                // Paso A: Registramos el producto principal y obtenemos el ID nuevo devuelto por el SP
                int idGenerado = _productoService.RegistrarProducto(producto);

                if (idGenerado > 0)
                {
                    // Paso B: Guardar las tallas y stocks seleccionados en producto_talla
                    if (listaTallasStock != null)
                    {
                        foreach (var item in listaTallasStock)
                        {
                            if (item.Stock > 0)
                            {
                                _productoService.GuardarProductoTallaStock(idGenerado, item.IdTalla, item.Stock);
                            }
                        }
                    }

                    // Paso C: Guardar la imagen en producto_imagen si se ingresó URL
                    if (!string.IsNullOrEmpty(imagenUrl))
                    {
                        _productoService.RegistrarImagen(idGenerado, imagenUrl);
                    }

                    return RedirectToAction("Index");
                }
            }
            return View(producto);
        }

        // 3. ACTUALIZAR - Vista GET
        [HttpGet]
        public IActionResult Actualizar(int id)
        {
            var producto = _productoService.ObtenerProductoPorId(id);
            if (producto == null)
            {
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // 3. ACTUALIZAR - Acción POST
        [HttpPost]
        public IActionResult Actualizar(Producto producto, List<ProductoTalla> listaTallasStock, string nuevaImagenUrl)
        {
            if (ModelState.IsValid)
            {
                // Actualiza los datos generales del producto
                _productoService.ActualizarProducto(producto);

                // Actualiza o reasigna las tallas y stock
                if (listaTallasStock != null)
                {
                    foreach (var item in listaTallasStock)
                    {
                        _productoService.GuardarProductoTallaStock(producto.IdProducto, item.IdTalla, item.Stock);
                    }
                }

                // Agrega nueva imagen si se especificó una
                if (!string.IsNullOrEmpty(nuevaImagenUrl))
                {
                    _productoService.RegistrarImagen(producto.IdProducto, nuevaImagenUrl);
                }

                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // 4. CAMBIAR ESTADO (Activo / Inactivo)
        [HttpPost]
        public IActionResult CambiarEstado(int idProducto, bool estado)
        {
            _productoService.CambiarEstadoProducto(idProducto, estado);
            return RedirectToAction("Index");
        }
    }
}