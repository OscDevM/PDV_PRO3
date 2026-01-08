using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ReportesDAO
{
    // REPORTE: STOCK BAJO
    public DataTable ObtenerStockBajo(int umbral)
    {
        DataTable dt = new DataTable();

        using (NpgsqlConnection cn = new NpgsqlConnection(Conexion.cadena))
        {
            NpgsqlCommand cmd = new NpgsqlCommand(
                "SELECT * FROM ListarProductosBajoStock(@umbral)", cn);

            cmd.Parameters.AddWithValue("@umbral", umbral);

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            da.Fill(dt);
        }

        return dt;
    }

    // REPORTE: VENTAS DIARIAS
    public DataTable ObtenerVentasDiarias(DateTime fecha)
    {
        DataTable dt = new DataTable();

        using (NpgsqlConnection cn = new NpgsqlConnection(Conexion.cadena))
        {
            NpgsqlCommand cmd = new NpgsqlCommand(@"
                SELECT 
                    v.id_venta,
                    v.fecha,
                    c.nombre AS cliente,
                    v.tipo,
                    v.total,
                    v.estado
                FROM ventas v
                LEFT JOIN clientes c ON v.id_cliente = c.id_cliente
                WHERE DATE(v.fecha) = @fecha
                ORDER BY v.id_venta", cn);

            cmd.Parameters.AddWithValue("@fecha", fecha.Date);

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            da.Fill(dt);
        }

        return dt;
    }

    // TOTAL VENTAS DIARIAS
    public decimal ObtenerTotalVentas(DateTime fecha)
    {
        decimal total = 0;

        using (NpgsqlConnection cn = new NpgsqlConnection(Conexion.cadena))
        {
            NpgsqlCommand cmd = new NpgsqlCommand(@"
                SELECT COALESCE(SUM(total),0)
                FROM ventas
                WHERE DATE(fecha) = @fecha
                AND estado <> 'anulada'", cn);

            cmd.Parameters.AddWithValue("@fecha", fecha.Date);

            cn.Open();
            total = Convert.ToDecimal(cmd.ExecuteScalar());
        }

        return total;
    }
}
