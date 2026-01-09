using System;
using System.Collections.Generic;
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
    }
}
