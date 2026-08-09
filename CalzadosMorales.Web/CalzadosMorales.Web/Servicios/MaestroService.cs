using CalzadosMorales.Web.Models;
using CalzadosMorales.Web.Repositorio;
using System.Text.RegularExpressions;

namespace CalzadosMorales.Web.Servicios
{
    public class MaestroService
    {
        private readonly MaestroRepository _repositorio;

        // Expresión regular para validar exclusivamente letras, tildes y espacios
        private readonly string _patronTexto = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$";

        public MaestroService(MaestroRepository repositorio)
        {
            _repositorio = repositorio;
        }

        // ==========================================
        // 1. CATEGORÍAS 
        // ==========================================
        public List<Categoria> ObtenerCategorias() => _repositorio.ListarCategorias();

        public Categoria ObtenerCategoriaPorId(int id) => _repositorio.ObtenerCategoriaPorId(id);

        public void GuardarCategoria(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            string nombreLimpio = nombre.Trim();
            if (!Regex.IsMatch(nombreLimpio, _patronTexto))
                throw new ArgumentException("El nombre de la categoría solo debe contener letras y espacios.");

            _repositorio.RegistrarCategoria(nombreLimpio);
        }

        public void ActualizarCategoria(int id, string nombre)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido.");

            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            string nombreLimpio = nombre.Trim();
            if (!Regex.IsMatch(nombreLimpio, _patronTexto))
                throw new ArgumentException("El nombre de la categoría solo debe contener letras y espacios.");

            _repositorio.ActualizarCategoria(id, nombreLimpio);
        }

        public void CambiarEstadoCategoria(int id, bool estado) => _repositorio.CambiarEstadoCategoria(id, estado);

        // ==========================================
        // 2. COLORES 
        // ==========================================
        public List<Color> ObtenerColores() => _repositorio.ListarColores();

        public Color ObtenerColorPorId(int id) => _repositorio.ObtenerColorPorId(id);

        public void GuardarColor(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            string nombreLimpio = nombre.Trim();
            if (!Regex.IsMatch(nombreLimpio, _patronTexto))
                throw new ArgumentException("El nombre del color solo debe contener letras y espacios.");

            _repositorio.RegistrarColor(nombreLimpio);
        }

        public void ActualizarColor(int id, string nombre)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido.");

            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            string nombreLimpio = nombre.Trim();
            if (!Regex.IsMatch(nombreLimpio, _patronTexto))
                throw new ArgumentException("El nombre del color solo debe contener letras y espacios.");

            _repositorio.ActualizarColor(id, nombreLimpio);
        }

        public void CambiarEstadoColor(int id, bool estado) => _repositorio.CambiarEstadoColor(id, estado);

        // ==========================================
        // 3. MATERIALES 
        // ==========================================
        public List<Material> ObtenerMateriales() => _repositorio.ListarMateriales();

        public Material ObtenerMaterialPorId(int id) => _repositorio.ObtenerMaterialPorId(id);

        public void GuardarMaterial(string tipo)
        {
            if (string.IsNullOrWhiteSpace(tipo))
                throw new ArgumentException("El tipo de material es obligatorio.");

            string tipoLimpio = tipo.Trim();
            if (tipoLimpio.Length < 2)
                throw new ArgumentException("El nombre del material es demasiado corto.");

            if (!Regex.IsMatch(tipoLimpio, _patronTexto))
                throw new ArgumentException("El tipo de material solo debe contener letras y espacios.");

            _repositorio.RegistrarMaterial(tipoLimpio);
        }

        public void ActualizarMaterial(int id, string tipo)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido.");

            if (string.IsNullOrWhiteSpace(tipo))
                throw new ArgumentException("El tipo de material es obligatorio.");

            string tipoLimpio = tipo.Trim();
            if (tipoLimpio.Length < 2)
                throw new ArgumentException("El nombre del material es demasiado corto.");

            if (!Regex.IsMatch(tipoLimpio, _patronTexto))
                throw new ArgumentException("El tipo de material solo debe contener letras y espacios.");

            _repositorio.ActualizarMaterial(id, tipoLimpio);
        }

        public void CambiarEstadoMaterial(int id, bool estado) => _repositorio.CambiarEstadoMaterial(id, estado);

        // ==========================================
        // 4. TALLAS 
        // ==========================================
        public List<Talla> ObtenerTallas() => _repositorio.ListarTallas();

        public Talla ObtenerTallaPorId(int id) => _repositorio.ObtenerTallaPorId(id);

        public void GuardarTalla(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la talla es obligatorio.");

            string tallaLimpia = nombre.Trim();

            if (!int.TryParse(tallaLimpia, out int tallaNumero))
                throw new ArgumentException("La talla debe ser un número entero válido (sin letras ni decimales).");

            if (tallaNumero < 15 || tallaNumero > 48)
                throw new ArgumentException("La talla debe estar en un rango válido entre 15 y 48.");

            _repositorio.RegistrarTalla(tallaLimpia);
        }

        public void ActualizarTalla(int id, string nombre)
        {
            if (id <= 0)
                throw new ArgumentException("ID inválido.");

            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la talla es obligatorio.");

            string tallaLimpia = nombre.Trim();

            if (!int.TryParse(tallaLimpia, out int tallaNumero))
                throw new ArgumentException("La talla debe ser un número entero válido (sin letras ni decimales).");

            if (tallaNumero < 15 || tallaNumero > 48)
                throw new ArgumentException("La talla debe estar en un rango válido entre 15 y 48.");

            _repositorio.ActualizarTalla(id, tallaLimpia);
        }

        public void CambiarEstadoTalla(int id, bool estado) => _repositorio.CambiarEstadoTalla(id, estado);
    }
}