using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDV_PRO3
{
    internal class Funciones
    {
        public static void Limpiar(Form C)
        {
            foreach (var c in C.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Clear();
                }
                if (c is ComboBox)
                {
                    ((ComboBox)c).SelectedIndex = 0;
                }
                if (c is GroupBox)
                {
                    foreach (var i in ((GroupBox)c).Controls)
                    {
                        if (i is TextBox)
                        {
                            ((TextBox)i).Clear();
                        }
                    }
                }
            }

        }

        public static bool Verificar(Form formulario)
        {
            foreach(var c in formulario.Controls)
            {
                if(c is TextBox)
                {
                    if(((TextBox)c).TextLength == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static DataTable LlamarDatos(string sql)
        {
            using (var conn = Conexion.GetConexion())
            {
                conn.Open();

                using (var da = new NpgsqlDataAdapter(sql, conn))
                {

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                    
                }
            }
        }
    }
}
