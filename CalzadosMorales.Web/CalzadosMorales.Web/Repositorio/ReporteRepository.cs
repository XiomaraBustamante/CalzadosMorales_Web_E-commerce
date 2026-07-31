using CalzadosMorales.Web.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CalzadosMorales.Web.Repositorio
{
    public class ReporteRepository
    {
        private readonly string _cadenaConexion;

        public ReporteRepository(IConfiguration configuration)
        {
            _cadenaConexion = configuration.GetConnectionString("CadenaSQL");
        }

        // 1. Análisis Horario de Ventas (sp_AdminAnalisisHorarioVentas)
        public List<AnalisisHorarioVM> ObtenerAnalisisHorarioVentas()
        {
            var lista = new List<AnalisisHorarioVM>();
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var cmd = new SqlCommand("sp_AdminAnalisisHorarioVentas", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conexion.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new AnalisisHorarioVM
                            {
                                HoraDelDia = Convert.ToInt32(dr["hora_del_dia"]),
                                BloqueHorario = dr["bloque_horario"].ToString(),
                                NumeroDeVentas = Convert.ToInt32(dr["numero_de_ventas"]),
                                MontoRecaudado = Convert.ToDecimal(dr["monto_recaudado"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // 2. Historial General de Ventas (sp_AdminHistorialGeneralVentas)
        public List<ReporteVentaVM> ListarHistorialGeneralVentas()
        {
            var lista = new List<ReporteVentaVM>();
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var cmd = new SqlCommand("sp_AdminHistorialGeneralVentas", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conexion.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteVentaVM
                            {
                                Id = Convert.ToInt32(dr["id"]),
                                Fecha = dr["fecha"].ToString(),
                                Vendedor = dr["vendedor"].ToString(),
                                Cliente = dr["cliente"].ToString(),
                                Comprobante = dr["comprobante"].ToString(),
                                Total = Convert.ToDecimal(dr["total"]),
                                Estado = dr["estado"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // 3. Consulta Stock con Filtros (sp_ConsultaStockFiltros)
        public List<ConsultaStockVM> ConsultarStockFiltros(int idCategoria, string nombreTalla)
        {
            var lista = new List<ConsultaStockVM>();
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var cmd = new SqlCommand("sp_ConsultaStockFiltros", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_id_categoria", idCategoria);
                    cmd.Parameters.AddWithValue("@p_nombre_talla", string.IsNullOrEmpty(nombreTalla) ? (object)DBNull.Value : nombreTalla);

                    conexion.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ConsultaStockVM
                            {
                                IdProducto = Convert.ToInt32(dr["id_producto"]),
                                Nombre = dr["nombre"].ToString(),
                                NombreCategoria = dr["nombre_categoria"].ToString(),
                                NombreTalla = dr["nombre_talla"].ToString(),
                                NombreColor = dr["nombre_color"].ToString(),
                                Precio = Convert.ToDecimal(dr["precio"]),
                                Stock = Convert.ToInt32(dr["stock"]),
                                IndicadorEstado = dr["indicador_estado"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // 4. Reporte de Ventas por Fechas (sp_AdminReporteVentasPorFechas)
        public List<ReporteVentasRangoVM> ReporteVentasPorFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            var lista = new List<ReporteVentasRangoVM>();
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var cmd = new SqlCommand("sp_AdminReporteVentasPorFechas", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_fecha_inicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@p_fecha_fin", fechaFin);

                    conexion.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteVentasRangoVM
                            {
                                Fecha = dr["fecha"].ToString(),
                                Vendedor = dr["vendedor"].ToString(),
                                Cliente = dr["cliente"].ToString(),
                                Comprobante = dr["comprobante"].ToString(),
                                Monto = Convert.ToDecimal(dr["monto"]),
                                Estado = dr["estado"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // 5. Sumatoria Total de Ventas en Rango (sp_AdminSumatoriaVentasRango)
        public decimal ObtenerSumatoriaVentasRango(DateTime fechaInicio, DateTime fechaFin)
        {
            decimal totalPeriodo = 0;
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var cmd = new SqlCommand("sp_AdminSumatoriaVentasRango", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_fecha_inicio", fechaInicio);
                    cmd.Parameters.AddWithValue("@p_fecha_fin", fechaFin);

                    conexion.Open();
                    var resultado = cmd.ExecuteScalar();
                    if (resultado != null && resultado != DBNull.Value)
                    {
                        totalPeriodo = Convert.ToDecimal(resultado);
                    }
                }
            }
            return totalPeriodo;
        }
    }
}
