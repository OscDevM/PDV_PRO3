using Npgsql;
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
        ClienteDAO dao = new ClienteDAO();
        int idClienteSeleccionado = 0;
        int idClienteEditar = 0;


        public CRUD_Clientes()
        {
            InitializeComponent();
        }

        private void CRUD_Clientes_Load(object sender, EventArgs e)
        {
            CargarGridEliminar();
            dgvBuscar.DataSource = dao.ListarClientes();
            ConfigurarGrid();
            CargarGridEditar();
        }

        // AGREGAR CLIENT

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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            if (dao.ExisteCedula(txtCedulaAgg.Text))
            {
                MessageBox.Show("Esta cédula ya está registrada", "Aviso");
                return;
            }

            bool ok = dao.AgregarCliente(
                txtNombreAgg.Text,
                txtCedulaAgg.Text,
                txtTelefonoAgg.Text,
                txtCorreoAgg.Text,
                txtDireccionAgg.Text
            );

            if (ok)
            {
                MessageBox.Show("Cliente agregado correctamente");
                LimpiarCampos();
                CargarGridEliminar();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el cliente intenta de nuevo palomo");
            }
        }



        // BUSCAR CLIENTE
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                MessageBox.Show("Ingrese un valor para buscar");
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
                MessageBox.Show("Seleccione un criterio");
                return;
            }

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

        // ELIMNAR CLIENTE
        private void CargarGridEliminar()
        {
            dgvEliminar.DataSource = dao.ListarClientes();
            dgvEliminar.Columns["id_cliente"].Visible = false;
        }

        private void dgvEliminar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = dgvEliminar.Rows[e.RowIndex];

            idClienteSeleccionado = Convert.ToInt32(fila.Cells["id_cliente"].Value);

            txtNombreEliminar.Text = fila.Cells["nombre"].Value.ToString();
            txtCedulaEliminar.Text = fila.Cells["documento_identificacion"].Value.ToString();
            txtTelefonoEliminar.Text = fila.Cells["telefono"].Value.ToString();
            txtCorreoEliminar.Text = fila.Cells["correo"].Value.ToString();
            txtDireccionEliminar.Text = fila.Cells["direccion"].Value.ToString();
        }

        private void LimpiarEliminar()
        {
            txtNombreEliminar.Clear();
            txtCedulaEliminar.Clear();
            txtTelefonoEliminar.Clear();
            txtCorreoEliminar.Clear();
            txtDireccionEliminar.Clear();
            idClienteSeleccionado = 0;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idClienteSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un cliente");
                return;
            }

            if (MessageBox.Show(
                "¿Está seguro de eliminar este cliente?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (dao.EliminarCliente(idClienteSeleccionado))
                {
                    MessageBox.Show("Cliente eliminado correctamente");
                    LimpiarEliminar();
                    CargarGridEliminar();
                }
                else
                {
                    MessageBox.Show("Error al eliminar");
                }
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        // EDITAR CLIENTES WAOS
        private void CargarGridEditar()
        {
            dgvEditar.DataSource = dao.ListarClientes();
            dgvEditar.Columns["id_cliente"].Visible = false;

            dgvEditar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEditar.ReadOnly = true;
            dgvEditar.AllowUserToAddRows = false;
        }

        private void dgvEditar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow fila = dgvEditar.Rows[e.RowIndex];

            idClienteEditar = Convert.ToInt32(fila.Cells["id_cliente"].Value);

            txtNombreEditar.Text = fila.Cells["nombre"].Value.ToString();
            txtCedulaEditar.Text = fila.Cells["documento_identificacion"].Value.ToString();
            txtTelefonoEditar.Text = fila.Cells["telefono"].Value.ToString();
            txtCorreoEditar.Text = fila.Cells["correo"].Value.ToString();
            txtDireccionEditar.Text = fila.Cells["direccion"].Value.ToString();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (idClienteEditar == 0)
            {
                MessageBox.Show("Seleccione un cliente");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNombreEditar.Text) ||
                string.IsNullOrWhiteSpace(txtCedulaEditar.Text))
            {
                MessageBox.Show("Nombre y cédula son obligatorios");
                return;
            }

            DialogResult r = MessageBox.Show(
                "¿Desea guardar los cambios?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (r == DialogResult.Yes)
            {
                bool editado = dao.EditarCliente(
                    idClienteEditar,
                    txtNombreEditar.Text.Trim(),
                    txtCedulaEditar.Text.Trim(),
                    txtTelefonoEditar.Text.Trim(),
                    txtCorreoEditar.Text.Trim(),
                    txtDireccionEditar.Text.Trim()
                );

                if (editado)
                {
                    MessageBox.Show("Cliente actualizado correctamente");
                    LimpiarEditar();
                    CargarGridEditar();
                    CargarGridEliminar();
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar");
                }
            }
        }
        private void LimpiarEditar()
        {
            txtNombreEditar.Clear();
            txtCedulaEditar.Clear();
            txtTelefonoEditar.Clear();
            txtCorreoEditar.Clear();
            txtDireccionEditar.Clear();
            idClienteEditar = 0;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage4)
            {
                CargarGridEditar();
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void txtTelefonoEditar_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

