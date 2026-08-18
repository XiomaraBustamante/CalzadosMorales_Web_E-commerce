using CalzadosMorales.Web.Models;
using CalzadosMorales.Web.Servicios;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalzadosMorales.Web.Controllers
{
    [Authorize(AuthenticationSchemes = "AdminCookie")]
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
            // Omitir validaciones de propiedades no mapeadas del modelo principal
            ModelState.Remove("ListaTallasStock");
            ModelState.Remove("ListaImagenes");
            ModelState.Remove("CategoriaNombre");
            ModelState.Remove("ColorNombre");
            ModelState.Remove("MaterialTipo");
            ModelState.Remove("Talla");

            // Validar si el modelo cumple con las Data Annotations establecidas
            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Json(new { success = false, message = "Por favor, corrija los siguientes errores:", errors = errores });
            }

            try
            {
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

                    // 3. Subida de imágenes a Cloudinary utilizando el orden explícito
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
                                        _productoService.RegistrarImagen(idGenerado, uploadResult.SecureUrl.ToString(), imgModel.Orden);
                                    }
                                }
                            }
                        }
                    }

                    return Json(new { success = true, message = "¡Producto registrado correctamente!" });
                }

                return Json(new { success = false, message = "No se pudo generar el ID del producto en la base de datos." });
            }
            catch (System.Exception ex)
            {
                return Json(new { success = false, message = "Error inesperado: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(Producto producto, List<ProductoTalla> listaTallasStock, List<ImagenFormModel> imagenesForm)
        {
            ModelState.Remove("ListaTallasStock");
            ModelState.Remove("ListaImagenes");
            ModelState.Remove("CategoriaNombre");
            ModelState.Remove("ColorNombre");
            ModelState.Remove("MaterialTipo");
            ModelState.Remove("Talla");

            // IMPORTANTE: Evitar que los slots de imágenes sin archivos nuevos disparen errores de validación
            if (imagenesForm != null)
            {
                for (int i = 0; i < imagenesForm.Count; i++)
                {
                    ModelState.Remove($"imagenesForm[{i}].Archivo");
                }
            }

            if (producto == null || producto.IdProducto <= 0)
            {
                return Json(new { success = false, message = "El ID del producto es inválido." });
            }

            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Json(new { success = false, message = "Por favor, corrija los siguientes errores:", errors = errores });
            }

            try
            {
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

                // 3. Procesamiento inteligente de imágenes por slot exacto
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
                                    string nuevaUrl = uploadResult.SecureUrl.ToString();
                                    _productoService.RegistrarImagen(producto.IdProducto, nuevaUrl, imgModel.Orden);
                                }
                            }
                        }
                    }
                }

                return Json(new { success = true, message = "¡Producto actualizado correctamente!" });
            }
            catch (System.Exception ex)
            {
                return Json(new { success = false, message = "Error inesperado al actualizar: " + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult CambiarEstado(int idProducto, bool estado)
        {
            try
            {
                _productoService.CambiarEstadoProducto(idProducto, estado);
                return Json(new { success = true, message = "Estado actualizado correctamente." });
            }
            catch (System.Exception ex)
            {
                return Json(new { success = false, message = "Error al cambiar estado: " + ex.Message });
            }
        }
    }
}