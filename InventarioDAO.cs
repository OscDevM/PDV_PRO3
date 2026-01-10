using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PDV_PRO3.InventarioDAO;

namespace PDV_PRO3
{
    internal class InventarioDAO
    {
   
        public static void Insertar(Inventario inv)
        {
            using (var conn = Conexion.GetConexion())
            {
                conn.Open();

                string sql = "INSERT INTO inventario (lugar, tramo) VALUES (@lugar, @tramo)";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@lugar", inv.Lugar);
                    cmd.Parameters.AddWithValue("@tramo", inv.Tramo);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static DataTable Listar()
        {
            using (var conn = Conexion.GetConexion())
            {
                conn.Open();

                string sql = "SELECT * FROM inventario ORDER BY id_inventario";
                using (var da = new NpgsqlDataAdapter(sql, conn))
                {

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
        public static void Actualizar(Inventario inv)
        {
            using (var conn = Conexion.GetConexion())
            {
                conn.Open();

                string sql = @"UPDATE inventario 
                   SET lugar=@lugar, tramo=@tramo 
                   WHERE id_inventario=@id";

                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@lugar", inv.Lugar);
                    cmd.Parameters.AddWithValue("@tramo", inv.Tramo);
                    cmd.Parameters.AddWithValue("@id", inv.IdInventario);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void Eliminar(int id)
        {
            using (var conn = Conexion.GetConexion())
            {
                conn.Open();

                string sql = "DELETE FROM inventario WHERE id_inventario=@id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

            }

        }
    }
}