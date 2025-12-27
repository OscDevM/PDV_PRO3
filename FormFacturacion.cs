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

        //para saber si en la factura habra un cliente o no
        bool hayCliente = false;


        public FormFacturacion()
        {
            InitializeComponent();
        }

        private void FrmFacturacion_Load(object sender, EventArgs e)
        {
            lblFechaValor.Text = DateTime.Now.ToString("dd/MM/yyyy");
            cbTipoVenta.SelectedIndex = 0;
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

                    //si hay un cliente
                    hayCliente = true;
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
        // FACTURAR
        // =========================
        private void btnFacturar_Click(object sender, EventArgs e)
        {
            /*solo validar que cuando la factura sea a credito tenga cliente despues las facturas permiten el campo id_cliente null 
             para permitir que las facturas pasen sin la necesidad de tener que tener el cliente agregado, solo es necesario cuando es
             a credito*/ 
            if (cbTipoVenta.SelectedIndex == 1 || txtNombreCliente.Tag == null)
            {
                MessageBox.Show("Debe seleccionar cliente para poder continual con el tipo de venta a credito");
                return;
            }

            int idCliente = 0;

            //verfificar si hay un cliente
            if (hayCliente)
            {
                idCliente = Convert.ToInt32(txtNombreCliente.Tag);
            }
            
            int idUsuario = 1; // Usuario logueado (luego de que de haga y conecta al login), Sebastian: Usuario admin agregado con el ID 1 para pruebas
            string tipo;
            if (cbTipoVenta.SelectedIndex == 0)
            {
                tipo = "contado";
            }
            else
            {
                tipo = "credito";
            }

                int idVenta;


            using (var con = Conexion.GetConexion())
            {
                con.Open();

                var cmd = new NpgsqlCommand(
                    "SELECT RegistrarFactura(@id_cliente,@id_usuario,@tipo,@subtotal,@impuesto,@total,@pago)", con);
                if (hayCliente)
                {

                    cmd.Parameters.AddWithValue("@id_cliente", idCliente);
                }
                else
                {

                    cmd.Parameters.AddWithValue("@id_cliente", "null");
                }
                    cmd.Parameters.AddWithValue("@id_usuario", idUsuario);
                cmd.Parameters.AddWithValue("@tipo", tipo);
                cmd.Parameters.AddWithValue("@subtotal", txtSubtotal.Text);
                cmd.Parameters.AddWithValue("@impuesto", txtITBIS.Text);
                cmd.Parameters.AddWithValue("@total", txtTotal.Text);
                cmd.Parameters.AddWithValue("@pagado", txtPagado.Text);

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
                        NpgsqlCommand cmd = new NpgsqlCommand("SELECT " +
                            "p.id_producto as id," +
                            "p.codigo_barra as Codigo_de_Barra," +
                            " p.nombre as Nombre, " +
                            "p.precio as Precio, " +
                            "1 as cantidad, " +
                            "COALESCE(d.porcentaje_descuento, 0) AS Descuento," +
                            "p.precio - (p.precio * COALESCE(d.porcentaje_descuento, 0) / 100) as Subtotal," +
                            "CASE WHEN p.impuestos = 'Sujeto' THEN ROUND((p.precio - (p.precio * COALESCE(d.porcentaje_descuento, 0) / 100)) * 0.18, 2) ELSE 0 END AS ITBIS, " +
                            "ROUND( (p.precio - (p.precio * COALESCE(d.porcentaje_descuento, 0) / 100))  + CASE WHEN p.impuestos = 'Sujeto' THEN (p.precio - (p.precio * COALESCE(d.porcentaje_descuento, 0) / 100)) * 0.18 ELSE 0 END, 2 ) AS total " +
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
                    
                    //sacar el total y el ITBIS
                    
                    double precio = Convert.ToDouble(dgvDetalle.Rows[fila].Cells["precio"].Value);
                    double cantidad = Convert.ToDouble(dgvDetalle.Rows[fila].Cells["cantidad"].Value);
                    double descuento = Convert.ToDouble(dgvDetalle.Rows[fila].Cells["descuento"].Value);
                    double subtotal = cantidad * precio;
                    double itbis = (subtotal - (subtotal * (descuento / 100))) * 0.18;
                    double total = (subtotal - (subtotal * (descuento / 100))) + itbis;
                    

                    dgvDetalle.Rows[fila].Cells["total"].Value = total;
                    
                    dgvDetalle.Rows[fila].Cells["itbis"].Value = itbis;

                    dgvDetalle.Rows[fila].Cells["subtotal"].Value = total - itbis;
                }
                txtProducto.Clear();

                txtSubtotal.Text = SacarSubTotal().ToString("0.00");
                txtITBIS.Text = SacarITBIS().ToString("0.00");
                txtTotal.Text = Convert.ToString(Convert.ToDouble(txtSubtotal.Text) + Convert.ToDouble(txtITBIS.Text));
            } 
        }

        private double SacarSubTotal()
        {
            int fila;
            double subtotal = 0;
            foreach(DataGridViewRow Row in dgvDetalle.Rows)
            {
                fila = Row.Index;
                subtotal += Convert.ToDouble(dgvDetalle.Rows[fila].Cells["subtotal"].Value);
            }
            return subtotal;
        }
        
        private double SacarITBIS()
        {
            int fila;
            double ITBIS = 0;
            foreach (DataGridViewRow Row in dgvDetalle.Rows)
            {
                fila = Row.Index;
                ITBIS += Convert.ToDouble(dgvDetalle.Rows[fila].Cells["itbis"].Value);
            }
            return ITBIS;
        }

    }
}
