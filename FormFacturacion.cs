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
    public partial class FormFacturacion : Form
    {
        decimal subtotal = 0;
        decimal impuesto = 0;
        decimal total = 0;

        public FormFacturacion()
        {
            InitializeComponent();
        }

        private void FrmFacturacion_Load(object sender, EventArgs e)
        {
            lblFechaValor.Text = DateTime.Now.ToString("dd/MM/yyyy");
            CargarProductos();
            InicializarGrid();
        }

        // =========================
        // INICIALIZAR GRID
        // =========================
        private void InicializarGrid()
        {
            dgvDetalle.Rows.Clear();
            dgvDetalle.Columns.Clear();

            dgvDetalle.Columns.Add("colIdProducto", "ID");
            dgvDetalle.Columns.Add("colProducto", "Producto");
            dgvDetalle.Columns.Add("colCantidad", "Cantidad");
            dgvDetalle.Columns.Add("colPrecio", "Precio");
            dgvDetalle.Columns.Add("colDescuento", "Descuento");
            dgvDetalle.Columns.Add("colSubtotal", "Subtotal");

            dgvDetalle.Columns["colIdProducto"].Visible = false;
            dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // =========================
        // CARGAR PRODUCTOS
        // =========================
        private void CargarProductos()
        {
            using (var con = Conexion.GetConexion())
            {
                con.Open();
                string sql = "SELECT id_producto, nombre, precio FROM productos WHERE activo = true";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                cbProducto.DataSource = dt;
                cbProducto.DisplayMember = "nombre";
                cbProducto.ValueMember = "id_producto";
                cbProducto.SelectedIndex = -1;
            }
        }

        // =========================
        // BUSCAR CLIENTE
        // =========================
        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            using (var con = Conexion.GetConexion())
            {
                con.Open();
                string sql = "SELECT id_cliente, nombre FROM clientes WHERE documento_identificacion = @doc";
                var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@doc", txtDocumento.Text);

                var dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtNombreCliente.Text = dr["nombre"].ToString();
                    txtNombreCliente.Tag = dr["id_cliente"];
                }
                else
                {
                    MessageBox.Show("Cliente no encontrado");
                    txtNombreCliente.Clear();
                    txtNombreCliente.Tag = null;
                }
            }
        }

        // =========================
        // AGREGAR PRODUCTO
        // =========================
        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (cbProducto.SelectedIndex == -1)
                return;

            int idProducto = Convert.ToInt32(cbProducto.SelectedValue);
            string nombre = cbProducto.Text;
            int cantidad = (int)nudCantidad.Value;
            decimal descuento = string.IsNullOrEmpty(txtDescuento.Text) ? 0 : Convert.ToDecimal(txtDescuento.Text);
            decimal precio;

            using (var con = Conexion.GetConexion())
            {
                con.Open();
                string sql = "SELECT precio FROM productos WHERE id_producto = @id";
                var cmd = new NpgsqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", idProducto);
                precio = Convert.ToDecimal(cmd.ExecuteScalar());
            }

            decimal sub = (cantidad * precio) - descuento;

            dgvDetalle.Rows.Add(idProducto, nombre, cantidad, precio, descuento, sub);
            CalcularTotales();
        }

        // =========================
        // CALCULAR TOTALES
        // =========================
        private void CalcularTotales()
        {
            subtotal = 0;

            foreach (DataGridViewRow row in dgvDetalle.Rows)
            {
                subtotal += Convert.ToDecimal(row.Cells["colSubtotal"].Value);
            }

            impuesto = subtotal * 0.18m;
            total = subtotal + impuesto;

            txtSubtotal.Text = subtotal.ToString("N2");
            txtITBIS.Text = impuesto.ToString("N2");
            txtTotal.Text = total.ToString("N2");
        }

        // =========================
        // FACTURAR
        // =========================
        private void btnFacturar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.Rows.Count == 0 || txtNombreCliente.Tag == null)
            {
                MessageBox.Show("Debe seleccionar cliente y productos");
                return;
            }

            int idCliente = Convert.ToInt32(txtNombreCliente.Tag);
            int idUsuario = 1; // Usuario logueado (luego de que de haga y conecta al login)
            string tipo = "contado";

            int idVenta;

            using (var con = Conexion.GetConexion())
            {
                con.Open();

                var cmd = new NpgsqlCommand(
                    "SELECT RegistrarFactura(@c,@u,@t,@s,@i,@to)", con);

                cmd.Parameters.AddWithValue("@c", idCliente);
                cmd.Parameters.AddWithValue("@u", idUsuario);
                cmd.Parameters.AddWithValue("@t", tipo);
                cmd.Parameters.AddWithValue("@s", subtotal);
                cmd.Parameters.AddWithValue("@i", impuesto);
                cmd.Parameters.AddWithValue("@to", total);

                idVenta = Convert.ToInt32(cmd.ExecuteScalar());

                foreach (DataGridViewRow row in dgvDetalle.Rows)
                {
                    var cmdDet = new NpgsqlCommand(
                        "SELECT RegistrarDetalleFactura(@v,@p,@c,@pu,18,@d)", con);

                    cmdDet.Parameters.AddWithValue("@v", idVenta);
                    cmdDet.Parameters.AddWithValue("@p", row.Cells["colIdProducto"].Value);
                    cmdDet.Parameters.AddWithValue("@c", row.Cells["colCantidad"].Value);
                    cmdDet.Parameters.AddWithValue("@pu", row.Cells["colPrecio"].Value);
                    cmdDet.Parameters.AddWithValue("@d", row.Cells["colDescuento"].Value);

                    cmdDet.ExecuteScalar();
                }
            }

            MessageBox.Show("Factura registrada correctamente");
            this.Close();
        }

        // =========================
        // EVENTOS VACÍOS (DESIGNER)
        // =========================
        private void gbCliente_Enter(object sender, EventArgs e) { }
        private void btnAnular_Click(object sender, EventArgs e) { }
    }
}
