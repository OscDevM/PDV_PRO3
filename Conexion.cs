using Npgsql;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net;


namespace PDV_PRO3
{

    public class Conexion
    {
            public static NpgsqlConnection GetConexion()
            {
                string cadena =
                    "User Id=postgres.gcdmhkypzedogttworxr;Password=Germo01122006@;Server=aws-1-us-east-1.pooler.supabase.com;Port=6543;Database=postgres";

                return new NpgsqlConnection(cadena);
            }
    }
}