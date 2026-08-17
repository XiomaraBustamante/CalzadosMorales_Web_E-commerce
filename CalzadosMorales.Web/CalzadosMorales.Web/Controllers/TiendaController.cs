using CalzadosMorales.Web.Servicios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CalzadosMorales.Web.Controllers
{
    public class TiendaController : Controller
    {
        private readonly ProductoTiendaService _productoTiendaService;
        private const int RegistrosPorPagina = 6; // Cantidad fija de productos por página

        // Inyectamos el servicio mediante Inyección de Dependencias
        public TiendaController(ProductoTiendaService productoTiendaService)
        {
            _productoTiendaService = productoTiendaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Método auxiliar privado para reutilizar toda la lógica de filtrado, paginación y seguridad
        private IActionResult ProcesarCatalogo(string categoria, int pagina)
        {
            // 1. Obtenemos la lista completa usando nuestro servicio
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

            // 4. Guardamos los datos necesarios en ViewBag para usarlos en la vista Razor
            ViewBag.CategoriaActual = categoria;
            ViewBag.PaginaActual = pagina;
            ViewBag.TotalPaginas = totalPaginas > 0 ? totalPaginas : 1; // Mínimo 1 página

            // 5. Enviamos la sub-lista paginada reutilizando la vista "Catalogo.cshtml"
            return View("Catalogo", productosPaginados);
        }

        // ACCIONES INDEPENDIENTES PARA CADA UNA DE TUS 5 CATEGORÍAS
        public IActionResult Zapatillas(int pagina = 1) => ProcesarCatalogo("Zapatillas", pagina);

        public IActionResult Sandalias(int pagina = 1) => ProcesarCatalogo("Sandalias", pagina);

        public IActionResult Zapatos(int pagina = 1) => ProcesarCatalogo("Zapatos", pagina);

        public IActionResult Botines(int pagina = 1) => ProcesarCatalogo("Botines", pagina);

        public IActionResult Mocasines(int pagina = 1) => ProcesarCatalogo("Mocasines", pagina);

        // Acción general por si se invoca mediante parámetros tradicionales (?categoria=...)
        public IActionResult Catalogo(string categoria, int pagina = 1)
        {
            if (string.IsNullOrEmpty(categoria))
            {
                categoria = "Zapatillas"; // Categoría por defecto si entra sin elegir
            }
            return ProcesarCatalogo(categoria, pagina);
        }
    }
}