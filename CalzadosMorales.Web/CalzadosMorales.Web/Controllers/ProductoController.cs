using CalzadosMorales.Web.Models;
using CalzadosMorales.Web.Servicios;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalzadosMorales.Web.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        private readonly ProductoService _productoService;
        private readonly MaestroService _maestroService;
        private readonly Cloudinary _cloudinary;

        public ProductoController(ProductoService productoService, MaestroService maestroService, Cloudinary cloudinary)
        {
            _productoService = productoService;
            _maestroService = maestroService;
            _cloudinary = cloudinary;
        }

        private void CargarCombosYTallas()
        {
            var tallasMaestro = _maestroService.ObtenerTallas();
            ViewBag.ListaCategorias = _maestroService.ObtenerCategorias();
            ViewBag.ListaColores = _maestroService.ObtenerColores();
            ViewBag.ListaMateriales = _maestroService.ObtenerMateriales();
            ViewBag.ListaTallas = tallasMaestro;
            ViewBag.ListaTallasTotal = tallasMaestro != null ? tallasMaestro.Where(t => t.Estado).ToList() : new List<Talla>();
        }

        public IActionResult Index()
        {
            var lista = _productoService.ListarProductos();
            CargarCombosYTallas();
            return View(lista);
        }

        [HttpGet]
        public IActionResult ObtenerPorId(int id)
        {
            var producto = _productoService.ObtenerProductoPorId(id);
            var imagenes = _productoService.ListarImagenesPorProducto(id);

            // Retornamos un objeto estructurado que incluye el producto y sus imágenes asociadas
            return Json(new
            {
                producto = producto,
                listaTallasStock = producto?.ListaTallasStock,
                imagenes = imagenes
            });
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(Producto producto, List<ProductoTalla> listaTallasStock, List<ImagenFormModel> imagenesForm)
        {
            ModelState.Remove("ListaTallasStock");
            ModelState.Remove("CategoriaNombre");
            ModelState.Remove("ColorNombre");
            ModelState.Remove("MaterialTipo");
            ModelState.Remove("Talla");

            // 1. Registrar producto básico y obtener el ID generado
            int idGenerado = _productoService.RegistrarProducto(producto);

            if (idGenerado > 0)
            {
                // 2. Procesamiento limpio de la lista de tallas y stock
                if (listaTallasStock != null && listaTallasStock.Count > 0)
                {
                    foreach (var item in listaTallasStock)
                    {
                        if (item.Stock >= 0)
                        {
                            _productoService.GuardarProductoTallaStock(idGenerado, item.IdTalla, item.Stock);
                        }
                    }
                }

                // 3. Subida de imágenes a Cloudinary utilizando el orden explícito (1, 2 o 3)
                if (imagenesForm != null && imagenesForm.Count > 0)
                {
                    foreach (var imgModel in imagenesForm)
                    {
                        if (imgModel.Archivo != null && imgModel.Archivo.Length > 0)
                        {
                            using (var stream = imgModel.Archivo.OpenReadStream())
                            {
                                var uploadParams = new ImageUploadParams()
                                {
                                    File = new FileDescription(imgModel.Archivo.FileName, stream),
                                    Folder = "calzados_morales/productos"
                                };
                                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    // Guardamos usando el procedimiento almacenado que respeta el orden exacto
                                    _productoService.RegistrarImagen(idGenerado, uploadResult.SecureUrl.ToString(), imgModel.Orden);
                                }
                            }
                        }
                    }
                }

                return RedirectToAction("Index");
            }

            CargarCombosYTallas();
            return View("Index", _productoService.ListarProductos());
        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(Producto producto, List<ProductoTalla> listaTallasStock, List<ImagenFormModel> imagenesForm)
        {
            ModelState.Remove("ListaTallasStock");
            ModelState.Remove("CategoriaNombre");
            ModelState.Remove("ColorNombre");
            ModelState.Remove("MaterialTipo");
            ModelState.Remove("Talla");

            // Validación estricta para evitar errores si el ID viene vacío o en 0
            if (producto == null || producto.IdProducto <= 0)
            {
                return RedirectToAction("Index");
            }

            // 1. Actualizar datos principales del producto
            _productoService.ActualizarProducto(producto);

            // 1.1 Limpiar las tallas anteriores para evitar conflictos o duplicados
            _productoService.LimpiarTallasProducto(producto.IdProducto);

            // 2. Actualizar stock por tallas
            if (listaTallasStock != null && listaTallasStock.Count > 0)
            {
                foreach (var item in listaTallasStock)
                {
                    item.IdProducto = producto.IdProducto;
                    _productoService.GuardarProductoTallaStock(producto.IdProducto, item.IdTalla, item.Stock);
                }
            }

            // 3. Procesamiento inteligente de imágenes por slot exacto (Orden 1, 2 o 3)
            if (imagenesForm != null && imagenesForm.Count > 0)
            {
                foreach (var imgModel in imagenesForm)
                {
                    // Solo procesamos los slots donde el usuario seleccionó un archivo nuevo
                    if (imgModel.Archivo != null && imgModel.Archivo.Length > 0)
                    {
                        using (var stream = imgModel.Archivo.OpenReadStream())
                        {
                            var uploadParams = new ImageUploadParams()
                            {
                                File = new FileDescription(imgModel.Archivo.FileName, stream),
                                Folder = "calzados_morales/productos"
                            };
                            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                string nuevaUrl = uploadResult.SecureUrl.ToString();

                                // Gracias a tu procedimiento sp_RegistrarProductoImagen, 
                                // este método evalúa si ya existe una foto para este producto en este orden exacto:
                                // - Si existe: la actualiza (UPDATE).
                                // - Si no existe: la inserta nueva (INSERT).
                                _productoService.RegistrarImagen(producto.IdProducto, nuevaUrl, imgModel.Orden);
                            }
                        }
                    }
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CambiarEstado(int idProducto, bool estado)
        {
            _productoService.CambiarEstadoProducto(idProducto, estado);
            return RedirectToAction("Index");
        }
    }
}