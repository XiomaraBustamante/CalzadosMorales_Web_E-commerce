using CalzadosMorales.Web.Models;
using CalzadosMorales.Web.Repositorio;

namespace CalzadosMorales.Web.Servicios
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioService(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Usuario ValidarUsuario(string usuario, string clave)
        {
            return _usuarioRepository.ValidarUsuario(usuario, clave);
        }

        public List<Rol> ListarRoles()
        {
            return _usuarioRepository.ListarRoles();
        }

        public List<Usuario> ListarUsuarios()
        {
            return _usuarioRepository.ListarUsuarios();
        }

        public Usuario ObtenerUsuarioPorId(int idUsuario)
        {
            return _usuarioRepository.ObtenerUsuarioPorId(idUsuario);
        }

        public void RegistrarUsuario(string nombre, string usuario, string clave, int idRol)
        {
            _usuarioRepository.RegistrarUsuario(nombre, usuario, clave, idRol);
        }

        public void ActualizarUsuario(int idUsuario, string nombre, string usuario, int idRol)
        {
            _usuarioRepository.ActualizarUsuario(idUsuario, nombre, usuario, idRol);
        }

        public void CambiarEstadoUsuario(int idUsuario, bool estado)
        {
            _usuarioRepository.CambiarEstadoUsuario(idUsuario, estado);
        }
    }
}
