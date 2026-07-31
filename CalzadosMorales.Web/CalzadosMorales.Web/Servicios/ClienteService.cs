using CalzadosMorales.Web.Models;
using CalzadosMorales.Web.Repositorio;

namespace CalzadosMorales.Web.Servicios
{
    public class ClienteService
    {
        private readonly ClienteRepository _clienteRepository;

        public ClienteService(ClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        // ==========================================
        // PERSONAS NATURALES
        // ==========================================
        public List<PersonaNatural> ObtenerPersonasNaturales()
        {
            return _clienteRepository.ListarPersonasNaturales();
        }

        public PersonaNatural ObtenerPersonaNaturalPorId(int idCliente)
        {
            return _clienteRepository.ObtenerPersonaNaturalPorId(idCliente);
        }

        public void RegistrarPersonaNatural(string dni, int genero, string nombre, string apellido, string telefono, string email, string direccion)
        {
            _clienteRepository.RegistrarPersonaNatural(dni, genero, nombre, apellido, telefono, email, direccion);
        }

        public void ActualizarPersonaNatural(int idCliente, string dni, int genero, string nombre, string apellido, string telefono, string email, string direccion)
        {
            _clienteRepository.ActualizarPersonaNatural(idCliente, dni, genero, nombre, apellido, telefono, email, direccion);
        }

        public void CambiarEstadoPersonaNatural(int idCliente, bool estado)
        {
            _clienteRepository.CambiarEstadoPersonaNatural(idCliente, estado);
        }

        // ==========================================
        // PERSONAS JURÍDICAS
        // ==========================================
        public List<PersonaJuridica> ObtenerPersonasJuridicas()
        {
            return _clienteRepository.ListarPersonasJuridicas();
        }

        public PersonaJuridica ObtenerPersonaJuridicaPorId(int idCliente)
        {
            return _clienteRepository.ObtenerPersonaJuridicaPorId(idCliente);
        }

        public void RegistrarPersonaJuridica(string ruc, string razonSocial, string repreLegal, string telefono, string email, string direccion)
        {
            _clienteRepository.RegistrarPersonaJuridica(ruc, razonSocial, repreLegal, telefono, email, direccion);
        }

        public void ActualizarPersonaJuridica(int idCliente, string ruc, string razonSocial, string repreLegal, string telefono, string email, string direccion)
        {
            _clienteRepository.ActualizarPersonaJuridica(idCliente, ruc, razonSocial, repreLegal, telefono, email, direccion);
        }

        public void CambiarEstadoPersonaJuridica(int idCliente, bool estado)
        {
            _clienteRepository.CambiarEstadoPersonaJuridica(idCliente, estado);
        }
    }
}