using CalzadosMorales.Web.Models;
using CalzadosMorales.Web.Repositorio;
using System.Text.RegularExpressions;

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
            try { return _clienteRepository.ListarPersonasNaturales(); }
            catch (Exception ex) { throw new Exception("Error al listar personas naturales: " + ex.Message); }
        }

        public PersonaNatural ObtenerPersonaNaturalPorId(int idCliente)
        {
            try { return _clienteRepository.ObtenerPersonaNaturalPorId(idCliente); }
            catch (Exception ex) { throw new Exception("Error al obtener persona natural: " + ex.Message); }
        }

        public void RegistrarPersonaNatural(string dni, int genero, string nombre, string apellido, string telefono, string email, string direccion)
        {
            try
            {
                ValidarDatosPersonaNatural(dni, genero, nombre, apellido, telefono, email, direccion);
                _clienteRepository.RegistrarPersonaNatural(dni.Trim(), genero, nombre.Trim(), apellido.Trim(), telefono.Trim(), email.Trim(), direccion.Trim());
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public void ActualizarPersonaNatural(int idCliente, string dni, int genero, string nombre, string apellido, string telefono, string email, string direccion)
        {
            try
            {
                if (idCliente <= 0) throw new Exception("ID de cliente inválido.");
                ValidarDatosPersonaNatural(dni, genero, nombre, apellido, telefono, email, direccion);
                _clienteRepository.ActualizarPersonaNatural(idCliente, dni.Trim(), genero, nombre.Trim(), apellido.Trim(), telefono.Trim(), email.Trim(), direccion.Trim());
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public void CambiarEstadoPersonaNatural(int idCliente, bool estado)
        {
            try { _clienteRepository.CambiarEstadoPersonaNatural(idCliente, estado); }
            catch (Exception ex) { throw new Exception("Error al cambiar estado: " + ex.Message); }
        }

        // ==========================================
        // PERSONAS JURÍDICAS
        // ==========================================
        public List<PersonaJuridica> ObtenerPersonasJuridicas()
        {
            try { return _clienteRepository.ListarPersonasJuridicas(); }
            catch (Exception ex) { throw new Exception("Error al listar personas jurídicas: " + ex.Message); }
        }

        public PersonaJuridica ObtenerPersonaJuridicaPorId(int idCliente)
        {
            try { return _clienteRepository.ObtenerPersonaJuridicaPorId(idCliente); }
            catch (Exception ex) { throw new Exception("Error al obtener persona jurídica: " + ex.Message); }
        }

        public void RegistrarPersonaJuridica(string ruc, string razonSocial, string repreLegal, string telefono, string email, string direccion)
        {
            try
            {
                ValidarDatosPersonaJuridica(ruc, razonSocial, repreLegal, telefono, email, direccion);
                _clienteRepository.RegistrarPersonaJuridica(ruc.Trim(), razonSocial.Trim(), repreLegal.Trim(), telefono.Trim(), email.Trim(), direccion.Trim());
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public void ActualizarPersonaJuridica(int idCliente, string ruc, string razonSocial, string repreLegal, string telefono, string email, string direccion)
        {
            try
            {
                if (idCliente <= 0) throw new Exception("ID de cliente inválido.");
                ValidarDatosPersonaJuridica(ruc, razonSocial, repreLegal, telefono, email, direccion);
                _clienteRepository.ActualizarPersonaJuridica(idCliente, ruc.Trim(), razonSocial.Trim(), repreLegal.Trim(), telefono.Trim(), email.Trim(), direccion.Trim());
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public void CambiarEstadoPersonaJuridica(int idCliente, bool estado)
        {
            try { _clienteRepository.CambiarEstadoPersonaJuridica(idCliente, estado); }
            catch (Exception ex) { throw new Exception("Error al cambiar estado: " + ex.Message); }
        }

        // ==========================================
        // VALIDACIONES (REGLAS DE NEGOCIO)
        // ==========================================

        private void ValidarDatosPersonaNatural(string dni, int genero, string nombre, string apellido, string telefono, string email, string direccion)
        {
            if (string.IsNullOrWhiteSpace(dni) || !Regex.IsMatch(dni.Trim(), @"^\d{8}$"))
                throw new Exception("El DNI debe contener exactamente 8 dígitos numéricos.");
            if (genero <= 0)
                throw new Exception("Debe seleccionar un género válido.");
            if (string.IsNullOrWhiteSpace(nombre) || !Regex.IsMatch(nombre.Trim(), @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                throw new Exception("El nombre solo debe contener letras.");
            if (string.IsNullOrWhiteSpace(apellido) || !Regex.IsMatch(apellido.Trim(), @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                throw new Exception("El apellido solo debe contener letras.");
            ValidarCamposGeneralesCliente(telefono, email, direccion);
        }

        private void ValidarDatosPersonaJuridica(string ruc, string razonSocial, string repreLegal, string telefono, string email, string direccion)
        {
            if (string.IsNullOrWhiteSpace(ruc) || !Regex.IsMatch(ruc.Trim(), @"^\d{11}$"))
                throw new Exception("El RUC debe contener exactamente 11 dígitos numéricos.");
            if (string.IsNullOrWhiteSpace(razonSocial) || razonSocial.Trim().Length < 3)
                throw new Exception("La Razón Social debe tener al menos 3 caracteres.");
            if (string.IsNullOrWhiteSpace(repreLegal) || !Regex.IsMatch(repreLegal.Trim(), @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                throw new Exception("El representante legal solo debe contener letras.");
            ValidarCamposGeneralesCliente(telefono, email, direccion);
        }

        private void ValidarCamposGeneralesCliente(string telefono, string email, string direccion)
        {
            if (string.IsNullOrWhiteSpace(telefono) || !Regex.IsMatch(telefono.Trim(), @"^\d{9}$"))
                throw new Exception("El teléfono debe contener exactamente 9 dígitos numéricos.");
            string patronEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email.Trim(), patronEmail))
                throw new Exception("El formato del correo electrónico es incorrecto.");
            if (string.IsNullOrWhiteSpace(direccion) || direccion.Trim().Length < 5)
                throw new Exception("La dirección debe tener al menos 5 caracteres.");
        }
    }
}