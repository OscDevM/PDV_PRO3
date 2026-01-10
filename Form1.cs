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
        public Form formularioOriginal;

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
            AbrirFormularioHijo(new Vista_MenuPrincipal());
            inicioToolStripMenuItem.Visible = false;
            apartadoSeleccionado = inicioToolStripMenuItem;

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(formularioOriginal != null)
            {
                formularioOriginal.Close();
            }
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

        private void lblUsuario_Click(object sender, EventArgs e)
        {

        }

        private void inicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (apartadoSeleccionado != null)
            {
                apartadoSeleccionado.Visible = true;
            }
            AbrirFormularioHijo(new Vista_MenuPrincipal());
            inicioToolStripMenuItem.Visible = false;
            apartadoSeleccionado = inicioToolStripMenuItem;
        }

        private void cerrarProgramaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¿Deseas continuar?", "Confirmacion",MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                ClaseUsuario._usuario = "";
                ClaseUsuario._idusuario = 0;
                formularioOriginal.Show();
                formularioOriginal = null;
                this.Close();
            }
        }

        private void inventario1ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
      
            if (apartadoSeleccionado != null)
            {
                apartadoSeleccionado.Visible = true;
            }
            AbrirFormularioHijo(new crud_inventario());
            inventarioToolStripMenuItem1.Visible = false;
            apartadoSeleccionado = inventarioToolStripMenuItem1;
        }
        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        
        }

        private void facturasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (apartadoSeleccionado != null)
            {
                apartadoSeleccionado.Visible = true;
            }
            AbrirFormularioHijo(new FrmProductos());
            productosToolStripMenuItem.Visible = false;
            apartadoSeleccionado = productosToolStripMenuItem;
        }

        private void clientesToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (apartadoSeleccionado != null)
            {
                apartadoSeleccionado.Visible = true;
            }
            AbrirFormularioHijo(new CRUD_Clientes());
            clientesToolStripMenuItem.Visible = false;
            apartadoSeleccionado = clientesToolStripMenuItem;
        }

        private void categoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (apartadoSeleccionado != null)
            {
                apartadoSeleccionado.Visible = true;
            }
            AbrirFormularioHijo(new Categoria());
            categoriaToolStripMenuItem.Visible = false;
            apartadoSeleccionado = categoriaToolStripMenuItem;
        }

        private void descuentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (apartadoSeleccionado != null)
            {
                apartadoSeleccionado.Visible = true;
            }
            AbrirFormularioHijo(new Descuentos());
            descuentosToolStripMenuItem.Visible = false;
            apartadoSeleccionado = descuentosToolStripMenuItem;
        }

        private void verFacturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (apartadoSeleccionado != null)
            {
                apartadoSeleccionado.Visible = true;
            }
            AbrirFormularioHijo(new VerFacturas());
            verFacturasToolStripMenuItem.Visible = false;
            apartadoSeleccionado = verFacturasToolStripMenuItem;
        }
    }
}
