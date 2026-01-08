using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Npgsql;

public class Conexion
{
    public static string cadena =
        "Host=db.gcdmhkypzedogttworxr.supabase.co;Database=postgres;Username=postgres;Password=Germo0112200;SSL Mode=Require;Trust Server Certificate=true";

    public static NpgsqlConnection GetConexion()
    {
        return new NpgsqlConnection(cadena);
    }
}
