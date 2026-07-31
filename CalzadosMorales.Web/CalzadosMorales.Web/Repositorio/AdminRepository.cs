using CalzadosMorales.Web.Datos;
using CalzadosMorales.Web.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CalzadosMorales.Web.Repositorio
{
    public class AdminRepository
    {
        private readonly string _cadena;

        public AdminRepository(ConexionBD conexion)
        {
            _cadena = conexion.ObtenerCadena();
        }

        public decimal ObtenerCajaHoy()
        {
            decimal total = 0;
            using (var cn = new SqlConnection(_cadena))
            {
                cn.Open();
                using (var cmd = new SqlCommand("sp_AdminCajaHoy", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var res = cmd.ExecuteScalar();
                    if (res != null && res != DBNull.Value) total = Convert.ToDecimal(res);
                }
            }
            return total;
        }

        public int ObtenerCantidadVentasHoy()
        {
            int cantidad = 0;
            using (var cn = new SqlConnection(_cadena))
            {
                cn.Open();
                using (var cmd = new SqlCommand("sp_AdminCantidadVentasHoy", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var res = cmd.ExecuteScalar();
                    if (res != null && res != DBNull.Value) cantidad = Convert.ToInt32(res);
                }
            }
            return cantidad;
        }

        public int ObtenerStockCritico()
        {
            int cantidad = 0;
            using (var cn = new SqlConnection(_cadena))
            {
                cn.Open();
                using (var cmd = new SqlCommand("sp_AdminStockCritico", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var res = cmd.ExecuteScalar();
                    if (res != null && res != DBNull.Value) cantidad = Convert.ToInt32(res);
                }
            }
            return cantidad;
        }

        public int ObtenerClientesNuevosMes()
        {
            int cantidad = 0;
            using (var cn = new SqlConnection(_cadena))
            {
                cn.Open();
                using (var cmd = new SqlCommand("sp_AdminClientesNuevosMes", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var res = cmd.ExecuteScalar();
                    if (res != null && res != DBNull.Value) cantidad = Convert.ToInt32(res);
                }
            }
            return cantidad;
        }

        public decimal ObtenerTicketPromedio()
        {
            decimal promedio = 0;
            using (var cn = new SqlConnection(_cadena))
            {
                cn.Open();
                using (var cmd = new SqlCommand("sp_AdminTicketPromedio", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var res = cmd.ExecuteScalar();
                    if (res != null && res != DBNull.Value) promedio = Convert.ToDecimal(res);
                }
            }
            return promedio;
        }

        public List<VentaSemanaVM> ObtenerVentasSemanales()
        {
            var lista = new List<VentaSemanaVM>();
            using (var cn = new SqlConnection(_cadena))
            {
                cn.Open();
                using (var cmd = new SqlCommand("sp_AdminVentasSemanales", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new VentaSemanaVM
                            {
                                FechaEtiqueta = dr["fecha_etiqueta"].ToString(),
                                TotalDia = Convert.ToDecimal(dr["total_dia"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<StockCategoriaVM> ObtenerStockPorCategoria()
        {
            var lista = new List<StockCategoriaVM>();
            using (var cn = new SqlConnection(_cadena))
            {
                cn.Open();
                using (var cmd = new SqlCommand("sp_AdminStockPorCategoria", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new StockCategoriaVM
                            {
                                Categoria = dr["categoria"].ToString(),
                                TotalStock = Convert.ToInt32(dr["total_stock"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<TopVendedorVM> ObtenerTopVendedores()
        {
            var lista = new List<TopVendedorVM>();
            using (var cn = new SqlConnection(_cadena))
            {
                cn.Open();
                using (var cmd = new SqlCommand("sp_AdminTopVendedores", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new TopVendedorVM
                            {
                                Vendedor = dr["vendedor"].ToString(),
                                CantidadOperaciones = Convert.ToInt32(dr["cantidad_operaciones"]),
                                MontoTotal = Convert.ToDecimal(dr["monto_total"]),
                                Variedad = Convert.ToInt32(dr["variedad"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

    }
}