using CalzadosMorales.Web.Models;
using CalzadosMorales.Web.Repositorio;

namespace CalzadosMorales.Web.Servicios
{
    public class AdminService
    {
        private readonly AdminRepository _adminRepository;

        public AdminService(AdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public decimal ObtenerIngresosHoy() => _adminRepository.ObtenerCajaHoy();
        public int ObtenerVentasHoy() => _adminRepository.ObtenerCantidadVentasHoy();
        public int ObtenerStockCritico() => _adminRepository.ObtenerStockCritico();
        public int ObtenerClientesNuevos() => _adminRepository.ObtenerClientesNuevosMes();
        public decimal ObtenerTicketPromedio() => _adminRepository.ObtenerTicketPromedio();
        public List<VentaSemanaVM> ObtenerVentasSemanales() => _adminRepository.ObtenerVentasSemanales();
        public List<StockCategoriaVM> ObtenerStockPorCategoria() => _adminRepository.ObtenerStockPorCategoria();
        public List<TopVendedorVM> ObtenerTopVendedores() => _adminRepository.ObtenerTopVendedores();
    }
}
