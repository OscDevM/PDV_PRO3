using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace Login_con_Métodos_en_C_2
{
    public class ClaDatos
    {
        private static readonly string cadenaConexion =

    "Host=db.gcdmhkypzedogttworxr.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=Germo0112200;SSL Mode=Require;Trust Server Certificate=true;Pooling=false; Timeout=30; Command Timeout=30";

        public bool Entrar(string usuario, string clave)
        {
            {
                try
                {
                    using (var conn = new NpgsqlConnection(cadenaConexion))
                    {
                        conn.Open();

                        string sql = @"SELECT 1 
                                   FROM usuarios 
                                   WHERE usuario = @usuario 
                                   AND password = @password";

                        using (var cmd = new NpgsqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("@usuario", usuario);
                            cmd.Parameters.AddWithValue("@password", clave);

                            object result = cmd.ExecuteScalar();
                            return result != null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de conexión: " + ex.Message);
                    return false;

                }
            }
        }
    }
}