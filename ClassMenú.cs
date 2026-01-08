using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDV_PRO3
{
    public class ClassMenú
    {
        private static readonly string cadenaConexion =
        "Host=db.gcdmhkypzedogttworxr.supabase.co;Port=5432;Database=postgres;Username=postgres;Password=Germo0112200;SSL Mode=Require;Trust Server Certificate=true;Pooling=false; Timeout=30; Command Timeout=30";
    
      public static NpgsqlConnection GetConexion()
        {
            return new NpgsqlConnection(cadenaConexion);
        }
    }
}
