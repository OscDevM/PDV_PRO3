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

        // aqui se almacena el formulario del login para que cuando se cierre el menu principal el login tambien 
        public Form login;

        //este formulario almacena el formulario que esta actualmente en el panel para asi poder cerrarlo cuando se abra otro
        private Form formularioPadre;

        //aqui se almacenara el apartado seleccionado del menustrip para ocultarlo y mostrar el anterior 
        ToolStripMenuItem apartadoSeleccionado = new ToolStripMenuItem();

        private void AbrirFormularioHijo(Form formularioHijo)
        {
            if(formularioPadre != null)
            {
                formularioPadre.Close();
            }
            formularioPadre = formularioHijo;
            formularioHijo.TopLevel = false;
            formularioHijo.FormBorderStyle = FormBorderStyle.None;
            formularioHijo.Dock = DockStyle.Fill;
            panel1.Controls.Add(formularioHijo);
            panel1.Tag = formularioHijo;
            formularioHijo.BringToFront();
            formularioHijo.Show();
        }

        private void cuentasPorCobrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = ClaseUsuario._usuario;

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            login.Close();
        }

        private void facturarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (apartadoSeleccionado != null)
            {

                apartadoSeleccionado.Visible = true;
            }
            AbrirFormularioHijo(new FormFacturacion());
            facturarToolStripMenuItem.Visible = false;
            apartadoSeleccionado = facturarToolStripMenuItem;
        }
    }
}
