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
    public partial class CRUD_Clientes : Form
    {

        public CRUD_Clientes()
        {
            InitializeComponent();
        }
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombreAgg.Text))
            {
                MessageBox.Show("El nombre es obligatorio");
                txtNombreAgg.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCedulaAgg.Text))
            {
                MessageBox.Show("La cédula es obligatoria");
                txtCedulaAgg.Focus();
                return false;
            }

            return true;
        }
        private void LimpiarCampos()
        {
            txtNombreAgg.Clear();
            txtCedulaAgg.Clear();
            txtTelefonoAgg.Clear();
            txtCorreoAgg.Clear();
            txtDireccionAgg.Clear();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                MessageBox.Show("Ingrese un valor para buscar", "Aviso");
                return;
            }

            string campo = "";

            if (rbNombre.Checked)
                campo = "nombre";
            else if (rbNumero.Checked)
                campo = "telefono";
            else if (rbCedula.Checked)
                campo = "documento_identificacion";
            else
            {
                MessageBox.Show("Seleccione un criterio de búsqueda");
                return;
            }

            ClienteDAO dao = new ClienteDAO();
            dgvBuscar.DataSource = dao.BuscarCliente(campo, txtBuscar.Text.Trim());

            ConfigurarGrid();
        }

        private void ConfigurarGrid()
        {
            dgvBuscar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBuscar.ReadOnly = true;
            dgvBuscar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBuscar.AllowUserToAddRows = false;
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (rbNumero.Checked || rbCedula.Checked)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnBuscar.PerformClick();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            ClienteDAO dao = new ClienteDAO();

            bool agregado = dao.AgregarCliente(
                txtNombreAgg.Text.Trim(),
                txtCedulaAgg.Text.Trim(),
                txtTelefonoAgg.Text.Trim(),
                txtCorreoAgg.Text.Trim(),
                txtDireccionAgg.Text.Trim()
            );

            if (agregado)
            {
                MessageBox.Show("Cliente agregado correctamente", "Éxito");
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el cliente", "Error");
            }

            if (dao.ExisteCedula(txtCedulaAgg.Text.Trim()))
            {
                MessageBox.Show("La cédula ya está registrada");
                return;
            }

        }

        private void txtSoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void CRUD_Clientes_Load(object sender, EventArgs e)
        {
            try
            {
                using (var conn = Conexion.GetConexion())
                {
                    conn.Open();
                    MessageBox.Show("Conexión exitosa con PostgreSQL ✅");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión:\n" + ex.Message);
            }
        }

        private void dgvEliminar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvEliminar_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = dgvEliminar.Rows[e.RowIndex];

            txtCedulaEliminar.Text = fila.Cells["cedula"].Value.ToString();
            txtNombreEliminar.Text = fila.Cells["nombre"].Value.ToString();
            txtTelefonoEliminar.Text = fila.Cells["telefono"].Value.ToString();
            txtCorreoEliminar.Text = fila.Cells["correo"].Value.ToString();
            txtDireccionEliminar.Text = fila.Cells["direccion"].Value.ToString();
        }
    }
}

