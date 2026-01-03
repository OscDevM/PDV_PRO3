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

        int idClienteEditar = 0;
        int idClienteEliminar = 0;

        public CRUD_Clientes()
        {
            InitializeComponent();
            CargarClientes();
        }

        private void CargarClientes()
        {
            dgvBuscar.DataSource = dao.ListarClientes();
            dgvEditar.DataSource = dao.ListarClientes();
            dgvEliminar.DataSource = dao.ListarClientes();
        }

        private void dgvEditar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var fila = dgvEditar.Rows[e.RowIndex];

            idClienteEditar = Convert.ToInt32(fila.Cells["id_cliente"].Value);

            txtNombreEditar.Text = fila.Cells["nombre"].Value.ToString();
            txtCedulaEditar.Text = fila.Cells["documento_identificacion"].Value.ToString();
            txtTelefonoEditar.Text = fila.Cells["telefono"].Value.ToString();
            txtCorreoEditar.Text = fila.Cells["correo"].Value.ToString();
            txtDireccionEditar.Text = fila.Cells["direccion"].Value.ToString();
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (idClienteEditar == 0)
            {
                MessageBox.Show("Seleccione un cliente para editar");
                return;
            }

            bool ok = dao.EditarCliente(
                idClienteEditar,
                txtNombreEditar.Text,
                txtCedulaEditar.Text,
                txtTelefonoEditar.Text,
                txtCorreoEditar.Text,
                txtDireccionEditar.Text
            );

            if (ok)
            {
                LimpiarEditar();
                CargarClientes();
                MessageBox.Show("Cliente actualizado correctamente");
            }
            else
            {
                MessageBox.Show("Error al actualizar cliente");
            }
        }

        private void dgvEliminar_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var fila = dgvEliminar.Rows[e.RowIndex];

            idClienteEliminar = Convert.ToInt32(fila.Cells["id_cliente"].Value);

            txtNombreEliminar.Text = fila.Cells["nombre"].Value.ToString();
            txtCedulaEliminar.Text = fila.Cells["documento_identificacion"].Value.ToString();
            txtTelefonoEliminar.Text = fila.Cells["telefono"].Value.ToString();
            txtCorreoEliminar.Text = fila.Cells["correo"].Value.ToString();
            txtDireccionEliminar.Text = fila.Cells["direccion"].Value.ToString();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idClienteEliminar == 0)
            {
                MessageBox.Show("Seleccione un cliente para eliminar");
                return;
            }

            if (MessageBox.Show(
                    "¿Seguro que desea eliminar este cliente?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                ) == DialogResult.Yes)
            {
                if (dao.EliminarCliente(idClienteEliminar))
                {
                    LimpiarEliminar();
                    CargarClientes();
                    MessageBox.Show("Cliente eliminado correctamente");
                }
            }
        }

        private void LimpiarEditar()
        {
            idClienteEditar = 0;
            txtNombreEditar.Clear();
            txtCedulaEditar.Clear();
            txtTelefonoEditar.Clear();
            txtCorreoEditar.Clear();
            txtDireccionEditar.Clear();
        }

        private void LimpiarEliminar()
        {
            idClienteEliminar = 0;
            txtNombreEliminar.Clear();
            txtCedulaEliminar.Clear();
            txtTelefonoEliminar.Clear();
            txtCorreoEliminar.Clear();
            txtDireccionEliminar.Clear();
        }
    }
}

