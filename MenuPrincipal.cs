using System;
using System.Drawing;
using System.Windows.Forms;

namespace PDV_PRO3
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnFacturación_Click(object sender, EventArgs e)
        {
            /*Form Formulario = new FormFacturacion();
            Formulario.Show();
            this.Hide();*/
        }

        private void BtnClientes_Click(object sender, EventArgs e)
        {
            /*Form Formulario = new FormClientes();
           Formulario.Show();
           this.Hide();*/
        }

        private void BtnUsuario_Click(object sender, EventArgs e)
        {
            /*Form Formulario = new FormUsuario();
            Formulario.Show();
            this.Hide();*/
        }

        private void BtnAcercade_Click(object sender, EventArgs e)
        {
            /*Form Formulario = new FormnAcercade();
             Formulario.Show();
             this.Hide();*/
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
