using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDV_PRO3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {

        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

      
        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void BtnIniciar_sesión_Click(object sender, EventArgs e)
        {
            {
                if (TxtUsuario.Text == "" || TxtContraseña.Text == "")
                {
                    MessageBox.Show("Complete todos los campos");
                    return;
                }
                /*Form Formulario = new formMenuPrincipal();
                Formulario.Show();
                this.Hide();*/
            }

        }
    }
}