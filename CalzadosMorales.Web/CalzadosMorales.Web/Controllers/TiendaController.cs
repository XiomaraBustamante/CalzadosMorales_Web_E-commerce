using CalzadosMorales.Web.Models;
using CalzadosMorales.Web.Servicios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CalzadosMorales.Web.Controllers
{
    public class TiendaController : Controller
    {
        private readonly ProductoTiendaService _productoTiendaService;
        private readonly FiltroTiendaService _filtroTiendaService; // Servicio para tu SP de filtros avanzados
        private const int RegistrosPorPagina = 6; // Cantidad fija de productos por página

        // Inyección de dependencias para ambos servicios
        public TiendaController(ProductoTiendaService productoTiendaService, FiltroTiendaService filtroTiendaService)
        {
            _productoTiendaService = productoTiendaService;
            _filtroTiendaService = filtroTiendaService;
        }

        public IActionResult Index()
        {
            // CARGAMOS LAS LISTAS COMPLETAS PARA EL CARRUSEL DE LA PÁGINA DE INICIO
            ViewBag.ProductosHombre = _productoTiendaService.ObtenerCatalogoPorCategoria("Hombre");
            ViewBag.ProductosMujer = _productoTiendaService.ObtenerCatalogoPorCategoria("Mujer");
            ViewBag.ProductosNino = _productoTiendaService.ObtenerCatalogoPorCategoria("Niño");

            return View();
        }

        // Método auxiliar privado para reutilizar toda la lógica de filtrado, paginación y seguridad (Tus categorías por texto)
        private IActionResult ProcesarCatalogo(string categoria, int pagina)
        {
            // 1. Obtenemos la lista completa usando nuestro servicio tradicional
            var listaProductos = _productoTiendaService.ObtenerCatalogoPorCategoria(categoria);

            // Si la lista es nula, inicializamos una vacía para evitar errores
            if (listaProductos == null)
            {
                listaProductos = new System.Collections.Generic.List<CalzadosMorales.Web.Models.ProductoTienda>();
            }

            // 2. Calculamos el total de registros y páginas necesarias
            int totalRegistros = listaProductos.Count;
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / RegistrosPorPagina);

            // Aseguramos que la página actual no sea menor a 1 ni mayor al total de páginas existentes
            if (pagina < 1) pagina = 1;
            if (totalPaginas > 0 && pagina > totalPaginas) pagina = totalPaginas;

            // 3. Aplicamos la paginación con LINQ (Skip y Take)
            var productosPaginados = listaProductos
                                      .Skip((pagina - 1) * RegistrosPorPagina)
                                      .Take(RegistrosPorPagina)
                                      .ToList();

            // 4. Guardamos los datos necesarios en ViewBag para usarlos en tu vista Razor actual
            ViewBag.CategoriaActual = categoria;
            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = totalPaginas > 0 ? totalPaginas : 1; // Mínimo 1 página

            // 5. Enviamos la sub-lista paginada reutilizando tu vista actual "Catalogo.cshtml"
            return View("Catalogo", productosPaginados);
        }

        // ACCIONES INDEPENDIENTES PARA CADA UNA DE TUS 5 CATEGORÍAS
        public IActionResult Zapatillas(int pagina = 1) => ProcesarCatalogo("Zapatillas", pagina);

        public IActionResult Sandalias(int pagina = 1) => ProcesarCatalogo("Sandalias", pagina);

        public IActionResult Zapatos(int pagina = 1) => ProcesarCatalogo("Zapatos", pagina);

        public IActionResult Botines(int pagina = 1) => ProcesarCatalogo("Botines", pagina);

        public IActionResult Mocasines(int pagina = 1) => ProcesarCatalogo("Mocasines", pagina);

        // ACCIONES PARA LAS TRES CATEGORÍAS PRINCIPALES (Hombre, Mujer, Niño)
        public IActionResult Hombre(int pagina = 1) => ProcesarCatalogo("Hombre", pagina);

        public IActionResult Mujer(int pagina = 1) => ProcesarCatalogo("Mujer", pagina);

        public IActionResult Nino(int pagina = 1) => ProcesarCatalogo("Niño", pagina);

        // Acción general por si se invoca mediante parámetros tradicionales (?categoria=...)
        public IActionResult Catalogo(string categoria, int pagina = 1)
        {
            if (string.IsNullOrEmpty(categoria))
            {
                categoria = "Zapatillas"; // Categoría por defecto si entra sin elegir
            }
            return ProcesarCatalogo(categoria, pagina);
        }

        // ACCIÓN AVANZADA: Maneja los filtros completos por SP (Categoría, Color, Material, Talla, Precios, Búsqueda y Orden)
        public IActionResult CatalogoAvanzado(int? id_categoria, int? id_color, int? id_material, int? id_talla, decimal? precio_min, decimal? precio_max, string busqueda, string orden)
        {
            // 1. Obtenemos los productos filtrados mediante el SP (sp_FiltroCatalogoWeb)
            var productos = _filtroTiendaService.FiltrarCatalogo(id_categoria, id_color, id_material, id_talla, precio_min, precio_max, busqueda, orden);

            // 2. Obtenemos las listas para poblar todos los filtros laterales
            var categorias = _filtroTiendaService.ObtenerCategorias();
            var colores = _filtroTiendaService.ObtenerColores();
            var materiales = _filtroTiendaService.ObtenerMateriales();
            var tallas = _filtroTiendaService.ObtenerTallas();

            // 3. Empaquetamos todo en el ViewModel completo
            var modelo = new CatalogoCompletoViewModel
            {
                ListaProductos = productos ?? new System.Collections.Generic.List<ProductoViewModel>(),
                ListaCategorias = categorias ?? new System.Collections.Generic.List<CategoriaViewModel>(),
                ListaColores = colores ?? new System.Collections.Generic.List<ColorViewModel>(),
                ListaMateriales = materiales ?? new System.Collections.Generic.List<MaterialViewModel>(),
                ListaTallas = tallas ?? new System.Collections.Generic.List<TallaViewModel>()
            };

            // 4. Guardamos los estados actuales en ViewBag para mantener los filtros seleccionados en la vista
            ViewBag.BusquedaActual = busqueda;
            ViewBag.OrdenActual = string.IsNullOrEmpty(orden) ? "recientes" : orden;
            ViewBag.CategoriaSeleccionada = id_categoria;
            ViewBag.ColorSeleccionado = id_color;
            ViewBag.MaterialSeleccionado = id_material;
            ViewBag.TallaSeleccionada = id_talla;
            ViewBag.PrecioMinActual = precio_min;
            ViewBag.PrecioMaxActual = precio_max;

            return View("CatalogoAvanzado", modelo);
        }
    }
}