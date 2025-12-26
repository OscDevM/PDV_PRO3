using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDV_PRO3
{
        public class Conexion
        {
            public static string cadena = "Host=db.gcdmhkypzedogttworxr.supabase.co;Database=postgres;Username=postgres;Password=Germo01122006@;SSL Mode=Require;Trust Server Certificate=true;Pooling=false";

            public static NpgsqlConnection GetConexion()
            {
                try
                {
                    var conexion = new NpgsqlConnection(cadena);
                    conexion.Open();
                    return conexion;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al conectar con la base de datos: " + ex.Message);
                }
            }
        }
}

