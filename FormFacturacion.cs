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
        //declaracion de datatable al principion  para almacenar todos los productos
        DataTable dt = new DataTable();

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

        //buscar los productos en base al codigo de barras
        private void txtProducto_TextChanged(object sender, EventArgs e)
        {
            //se utilizara para saber si el producto esta ya en el datagridview
            bool productoEsta = false;

            //almacenaremos el indice en caso de que si este
            int fila = 0;

            //valida que sean 13 numeros como todos los codigos de barras
            if(txtProducto.TextLength == 13)
            {
                foreach(DataGridViewRow Row in dgvDetalle.Rows)
                {
                    fila = Row.Index;
                    
                    //almacena el codigo de barras del indice actual
                    string codigoDeBarras = Row.Cells[0].Value.ToString();
                    if(codigoDeBarras == txtProducto.Text)
                    {
                        productoEsta = true;
                        break;
                    }
                }

                if (productoEsta == false)
                {
                    using (var con = Conexion.GetConexion())
                    {
                        con.Open();

                        //select que busca el producto analiza si tiene descuento 
                        NpgsqlCommand cmd = new NpgsqlCommand("SELECT p.codigo_barra as Codigo_de_Barra," +
                            " p.nombre as Nombre, " +
                            "p.precio as Precio, " +
                            "1 as cantidad, " +
                            "CASE WHEN p.impuestos = 'Sujeto' THEN 0.18 * p.precio ELSE 0 END AS ITBIS, " +
                            "COALESCE(d.porcentaje_descuento, 0) AS Descuento," +
                            "(p.precio + CASE WHEN p.impuestos = 'Sujeto' THEN 0.18 * p.precio ELSE 0 END - (p.precio * COALESCE(d.porcentaje_descuento, 0) / 100) ) AS total " +
                            "FROM productos p LEFT JOIN descuentos d ON p.id_producto = d.id_producto AND d.activo = TRUE " +
                            "WHERE p.codigo_barra = @codigo_barras;", con);

                        cmd.Parameters.AddWithValue("@codigo_barras", txtProducto.Text);
                        NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                        adapter.Fill(dt);
                        dgvDetalle.DataSource = dt;
                        con.Close();
                    }
                }
                else
                {
                    //aumenta la cantidad de el producto
                    dgvDetalle.Rows[fila].Cells["cantidad"].Value = Convert.ToString(Convert.ToInt32(dgvDetalle.Rows[fila].Cells["cantidad"].Value) + 1);
                    dgvDetalle.Rows[fila].Cells["itbis"].Value = Convert.ToString((Convert.ToDecimal(dgvDetalle.Rows[fila].Cells["precio"].Value) * Convert.ToDecimal(0.18)) * Convert.ToDecimal(dgvDetalle.Rows[fila].Cells["cantidad"].Value));
                    dgvDetalle.Rows[fila].Cells["total"].Value = Convert.ToString((Convert.ToDecimal(dgvDetalle.Rows[fila].Cells["precio"].Value) * Convert.ToDecimal(dgvDetalle.Rows[fila].Cells["cantidad"].Value) + Convert.ToDecimal(dgvDetalle.Rows[fila].Cells["itbis"].Value)));
                }

            } 
        }
    }
}
