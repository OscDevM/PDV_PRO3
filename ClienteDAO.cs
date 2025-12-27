using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace PDV_PRO3
{
    public class ClienteDAO
    {
        public DataTable BuscarCliente(string campo, string valor)
        {
            DataTable dt = new DataTable();

            string sql = $@"
                SELECT 
                    id_cliente,
                    documento_identificacion AS cedula,
                    nombre,
                    telefono,
                    correo,
                    limite_credito,
                    activo
                FROM clientes
                WHERE activo = TRUE
                  AND {campo} ILIKE @valor
                ORDER BY nombre;
            ";

            using (var conexion = Conexion.GetConexion())
            {
                conexion.Open();

                using (var cmd = new NpgsqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@valor", "%" + valor + "%");

                    using (var da = new NpgsqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public bool AgregarCliente(string nombre, string cedula, string telefono, string correo, string direccion)
        {
            string sql = @"
                INSERT INTO clientes
                (
                    documento_identificacion,
                    nombre,
                    telefono,
                    correo,
                    direccion
                )
                VALUES
                (
                    @cedula,
                    @nombre,
                    @telefono,
                    @correo,
                    @direccion
                );
            ";

            using (var conexion = Conexion.GetConexion())
            {
                conexion.Open();

                using (var cmd = new NpgsqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@cedula", cedula);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@telefono", telefono);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@direccion", direccion);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool ExisteCedula(string cedula)
        {
            string sql = @"
                SELECT COUNT(1)
                FROM clientes
                WHERE documento_identificacion = @cedula;
            ";

            using (var conexion = Conexion.GetConexion())
            {
                conexion.Open(); 

                using (var cmd = new NpgsqlCommand(sql, conexion))
                {
                    
                    cmd.Parameters.AddWithValue("@cedula", cedula);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }
    }
}

