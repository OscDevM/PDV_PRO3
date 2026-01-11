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
    public partial class FrmProductos : Form
    {
        ProductoDAO productoDAO = new ProductoDAO();
        int idProductoSeleccionado = 0;

        public FrmProductos()
        {
            InitializeComponent();
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            CargarGrid();
            CargarCombos();
            LimpiarCampos();
        }

        // CARGAS INICIALES
        private void CargarGrid()
        {
            dgvProductos.DataSource = productoDAO.ListarProductos();
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CargarCombos()
        {
            cmbCategoria.DataSource = productoDAO.ListarCategorias();
            cmbCategoria.DisplayMember = "nombre";
            cmbCategoria.ValueMember = "id_categoria";
            cmbCategoria.SelectedIndex = -1;

            cmbInventario.DataSource = productoDAO.ListarInventarios();
            cmbInventario.DisplayMember = "lugar";
            cmbInventario.ValueMember = "id_inventario";
            cmbInventario.SelectedIndex = -1;

            cmbImpuesto.Items.Clear();
            cmbImpuesto.Items.Add("Exento");
            cmbImpuesto.Items.Add("Sujeto");
            cmbImpuesto.SelectedIndex = -1;

            dgvProductos.Columns["id_producto"].Visible = false;
            dgvProductos.Columns["id_categoria"].Visible = false;
            dgvProductos.Columns["id_inventario"].Visible = false;
        }

        // BOTÓN GUARDAR
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarFormulario()) return;

                productoDAO.CrearProducto(
                    txtCodigoBarra.Text.Trim(),
                    txtNombre.Text.Trim(),
                    txtDescripcion.Text.Trim(),
                    decimal.Parse(txtPrecio.Text),
                    decimal.Parse(txtCosto.Text),
                    int.Parse(txtStock.Text),
                    (int)cmbCategoria.SelectedValue,
                    cmbImpuesto.Text,
                    ClaseUsuario._idusuario,
                    "Stock inicial",
                    (int)cmbInventario.SelectedValue
                );

                MessageBox.Show("Producto registrado correctamente");
                CargarGrid();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("productos_codigo_barra_key"))
                {
                    MessageBox.Show(
                        "❌ Ya existe un producto con ese código de barra",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    txtCodigoBarra.Focus();
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // DOBLE CLICK GRID
        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProductos.CurrentRow == null) return;

            DataGridViewRow fila = dgvProductos.CurrentRow;

            idProductoSeleccionado = Convert.ToInt32(fila.Cells["id_producto"].Value);

            txtCodigoBarra.Text = fila.Cells["codigo_barra"].Value.ToString();
            txtNombre.Text = fila.Cells["nombre"].Value.ToString();
            txtDescripcion.Text = fila.Cells["descripcion"].Value.ToString();
            txtPrecio.Text = fila.Cells["precio"].Value.ToString();
            txtCosto.Text = fila.Cells["costo"].Value.ToString();
            txtStock.Text = fila.Cells["stock"].Value.ToString();

            cmbImpuesto.Text = fila.Cells["impuestos"].Value.ToString();

            cmbCategoria.SelectedValue =
                Convert.ToInt32(fila.Cells["id_categoria"].Value);

            cmbInventario.SelectedValue =
                Convert.ToInt32(fila.Cells["id_inventario"].Value);
        }

        // AGREGAR STOCK
        private void btnAgregarStock_Click(object sender, EventArgs e)
        {
            if (idProductoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un producto con doble click");
                return;
            }

            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Cantidad inválida");
                return;
            }

            productoDAO.AgregarStock(
                idProductoSeleccionado,
                cantidad,
                "Reposición",
                ClaseUsuario._idusuario
            );

            MessageBox.Show("Stock actualizado");
            CargarGrid();
            txtCantidad.Clear();
        }

        // ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idProductoSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un producto");
                return;
            }

            productoDAO.EliminarProducto(idProductoSeleccionado);
            MessageBox.Show("Producto eliminado correctamente");
            CargarGrid();
            LimpiarCampos();
        }

        // LIMPIAR
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtCodigoBarra.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtCosto.Clear();
            txtStock.Clear();
            txtCantidad.Clear();

            cmbCategoria.SelectedIndex = -1;
            cmbInventario.SelectedIndex = -1;
            cmbImpuesto.SelectedIndex = -1;

            idProductoSeleccionado = 0;
            txtCodigoBarra.Focus();
        }

        // VALIDACIONES
        private bool ValidarFormulario()
        {
            if (txtNombre.Text.Trim() == "" ||
                txtPrecio.Text.Trim() == "" ||
                txtStock.Text.Trim() == "" ||
                cmbCategoria.SelectedIndex == -1 ||
                cmbInventario.SelectedIndex == -1 ||
                cmbImpuesto.SelectedIndex == -1)
            {
                MessageBox.Show("Complete todos los campos obligatorios");
                return false;
            }

            if (!decimal.TryParse(txtPrecio.Text, out _) ||
                !decimal.TryParse(txtCosto.Text, out _) ||
                !int.TryParse(txtStock.Text, out _))
            {
                MessageBox.Show("Formato numérico incorrecto");
                return false;
            }

            return true;
        }
    }
}
