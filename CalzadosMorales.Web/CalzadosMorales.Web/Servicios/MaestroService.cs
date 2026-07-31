using CalzadosMorales.Web.Models;
using CalzadosMorales.Web.Repositorio;

namespace CalzadosMorales.Web.Servicios
{
    public class MaestroService
    {
        private readonly MaestroRepository _repositorio;

        public MaestroService(MaestroRepository repositorio)
        {
            _repositorio = repositorio;
        }

        // ==========================================
        // 1. CATEGORÍAS 
        // ==========================================
        public List<Categoria> ObtenerCategorias() => _repositorio.ListarCategorias();
        public Categoria ObtenerCategoriaPorId(int id) => _repositorio.ObtenerCategoriaPorId(id);
        public void GuardarCategoria(string nombre) => _repositorio.RegistrarCategoria(nombre);
        public void ActualizarCategoria(int id, string nombre) => _repositorio.ActualizarCategoria(id, nombre);
        public void CambiarEstadoCategoria(int id, bool estado) => _repositorio.CambiarEstadoCategoria(id, estado);


        // ==========================================
        // 2. COLORES 
        // ==========================================
        public List<Color> ObtenerColores() => _repositorio.ListarColores();
        public Color ObtenerColorPorId(int id) => _repositorio.ObtenerColorPorId(id);
        public void GuardarColor(string nombre) => _repositorio.RegistrarColor(nombre);
        public void ActualizarColor(int id, string nombre) => _repositorio.ActualizarColor(id, nombre);
        public void CambiarEstadoColor(int id, bool estado) => _repositorio.CambiarEstadoColor(id, estado);


        // ==========================================
        // 3. MATERIALES 
        // ==========================================
        public List<Material> ObtenerMateriales() => _repositorio.ListarMateriales();
        public Material ObtenerMaterialPorId(int id) => _repositorio.ObtenerMaterialPorId(id);
        public void GuardarMaterial(string tipo) => _repositorio.RegistrarMaterial(tipo);
        public void ActualizarMaterial(int id, string tipo) => _repositorio.ActualizarMaterial(id, tipo);
        public void CambiarEstadoMaterial(int id, bool estado) => _repositorio.CambiarEstadoMaterial(id, estado);


        // ==========================================
        // 4. TALLAS 
        // ==========================================
        public List<Talla> ObtenerTallas() => _repositorio.ListarTallas();
        public Talla ObtenerTallaPorId(int id) => _repositorio.ObtenerTallaPorId(id);
        public void GuardarTalla(string nombre) => _repositorio.RegistrarTalla(nombre);
        public void ActualizarTalla(int id, string nombre) => _repositorio.ActualizarTalla(id, nombre);
        public void CambiarEstadoTalla(int id, bool estado) => _repositorio.CambiarEstadoTalla(id, estado);
    }
}