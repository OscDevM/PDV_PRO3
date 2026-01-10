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
        // BUSCAR CLIENTE
        public DataTable BuscarCliente(string campo, string valor)
        {
            DataTable dt = new DataTable();

            string columna;

            switch (campo)
            {
                case "nombre":
                case "documento_identificacion":
                case "telefono":
                case "correo":
                    columna = campo;
                    break;
                default:
                    throw new Exception("Campo de búsqueda no válido");
            }

            string sql = $@"
                SELECT 
                    id_cliente,
                    documento_identificacion,
                    nombre,
                    telefono,
                    correo,
                    direccion
                FROM clientes
                WHERE activo = TRUE
                  AND {columna} ILIKE @valor
                ORDER BY nombre;
            ";

            using (var conexion = Conexion.Con())
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

        // AGREGAR CLIENTE
        public bool AgregarCliente(string nombre, string cedula, string telefono, string correo, string direccion)
        {
            string sql = @"
                INSERT INTO clientes
                (
                    documento_identificacion,
                    nombre,
                    telefono,
                    correo,
                    direccion,
                    activo
                )
                VALUES
                (
                    @cedula,
                    @nombre,
                    @telefono,
                    @correo,
                    @direccion,
                    TRUE
                );
            ";

            using (var conexion = Conexion.Con())
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

        // VALIDAR CÉDULA
        public bool ExisteCedula(string cedula)
        {
            string sql = @"
                SELECT COUNT(1)
                FROM clientes
                WHERE documento_identificacion = @cedula
                  AND activo = TRUE;
            ";

            using (var conexion = Conexion.Con())
            {
                conexion.Open();

                using (var cmd = new NpgsqlCommand(sql, conexion))
                {
                    cmd.Parameters.AddWithValue("@cedula", cedula);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
        }

        // LISTAR CLIENTES
        public DataTable ListarClientes()
        {
            DataTable dt = new DataTable();

            string sql = @"
                SELECT 
                    id_cliente,
                    documento_identificacion,
                    nombre,
                    telefono,
                    correo,
                    direccion
                FROM clientes
                WHERE activo = TRUE
                ORDER BY nombre;
            ";

            using (var con = Conexion.Con())
            {
                con.Open();

                using (var cmd = new NpgsqlCommand(sql, con))
                using (var da = new NpgsqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        // ELIMINAR CLIENTE
        public bool EliminarCliente(int idCliente)
        {
            string sql = @"
                UPDATE clientes
                SET activo = FALSE
                WHERE id_cliente = @id;
            ";

            using (var con = Conexion.Con())
            {
                con.Open();

                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", idCliente);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // EDITAR CLIENTE
        public bool EditarCliente(
            int idCliente,
            string nombre,
            string cedula,
            string telefono,
            string correo,
            string direccion
        )
        {
            string sql = @"
                UPDATE clientes
                SET
                    nombre = @nombre,
                    documento_identificacion = @cedula,
                    telefono = @telefono,
                    correo = @correo,
                    direccion = @direccion
                WHERE id_cliente = @id
                  AND activo = TRUE;
            ";

            using (var con = Conexion.Con())
            {
                con.Open();

                using (var cmd = new NpgsqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", idCliente);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@cedula", cedula);
                    cmd.Parameters.AddWithValue("@telefono", telefono);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@direccion", direccion);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}

