using Npgsql;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net;

public class Conexion
{
    private static string cadena =
        "Host=db.gcdmhkypzedogttworxr.supabase.co;Database=postgres;Username=postgres;Password=Germo0112200;SSL Mode=Require;Trust Server Certificate=true";

    public static NpgsqlConnection GetConexion()
    {
        return new NpgsqlConnection(cadena);
    }
}
