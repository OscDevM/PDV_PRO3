using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDV_PRO3
{
    internal class ClaseUsuario
    {
        public static string _usuario;
        public static int _idusuario = 0;

        public bool VerificarUsuario(string usuario, string clave)
        {
            try
            {
                using (var conn = Conexion.GetConexion())
                {
                    conn.Open();

                    string sql = @"SELECT id_usuario 
                                   FROM usuarios 
                                   WHERE usuario = @usuario 
                                   AND password = @password";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@password", clave);

                        _idusuario = Convert.ToInt32(cmd.ExecuteScalar());
                        _usuario = usuario;
                        return _idusuario > 0;
                    } 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }
    }
}
