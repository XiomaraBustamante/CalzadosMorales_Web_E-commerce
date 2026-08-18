using CalzadosMorales.Web.Models;
using CalzadosMorales.Web.Repositorio;
using System.Collections.Generic;

namespace CalzadosMorales.Web.Servicios
{
    public class FiltroTiendaService
    {
        private readonly FiltroTiendaRepository _filtroRepo;

        public FiltroTiendaService(FiltroTiendaRepository filtroRepo)
        {
            _filtroRepo = filtroRepo;
        }

        public List<ProductoViewModel> FiltrarCatalogo(int? id_categoria, int? id_color, int? id_material, int? id_talla, decimal? precio_min, decimal? precio_max, string busqueda, string orden)
        {
            return _filtroRepo.FiltrarCatalogo(id_categoria, id_color, id_material, id_talla, precio_min, precio_max, busqueda, orden);
        }

        public List<CategoriaViewModel> ObtenerCategorias()
        {
            return _filtroRepo.ObtenerCategorias();
        }

        // --- NUEVOS MÉTODOS AÑADIDOS ---

        public List<ColorViewModel> ObtenerColores()
        {
            return _filtroRepo.ObtenerColores();
        }

        public List<MaterialViewModel> ObtenerMateriales()
        {
            return _filtroRepo.ObtenerMateriales();
        }

        public List<TallaViewModel> ObtenerTallas()
        {
            return _filtroRepo.ObtenerTallas();
        }
    }
}