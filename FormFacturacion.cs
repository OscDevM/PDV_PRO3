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
            if (ValidarFactura() == false)
            {
                return;
            }


            int idCliente = 0;

            //verfificar si hay un cliente
            if (hayCliente)
            {
                idCliente = Convert.ToInt32(txtNombreCliente.Tag);
            }

            int idUsuario = ClaseUsuario._idusuario; // Llamar el usuario de esta manera
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
            double total = Convert.ToDouble(txtTotal.Text);
            double subtotal = Convert.ToDouble(txtSubtotal.Text);
            double pagado = Convert.ToDouble(txtPagado.Text);
            double itbis = Convert.ToDouble(txtITBIS.Text);

            using (var con = Conexion.GetConexion())
            {
                con.Open();

                var cmd = new NpgsqlCommand(
                    "SELECT RegistrarFactura(@id_cliente,@id_usuario,@tipo,@subtotal,@impuesto,@total,@pagado)", con);
                if (hayCliente)
                {
                    cmd.Parameters.AddWithValue("@id_cliente", NpgsqlTypes.NpgsqlDbType.Integer, idCliente);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@id_cliente", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@id_usuario", NpgsqlTypes.NpgsqlDbType.Integer, idUsuario);
                cmd.Parameters.AddWithValue("@tipo", NpgsqlTypes.NpgsqlDbType.Varchar, tipo);
                cmd.Parameters.AddWithValue("@subtotal", NpgsqlTypes.NpgsqlDbType.Numeric, subtotal);
                cmd.Parameters.AddWithValue("@impuesto", NpgsqlTypes.NpgsqlDbType.Numeric, itbis);
                cmd.Parameters.AddWithValue("@total", NpgsqlTypes.NpgsqlDbType.Numeric, total);
                cmd.Parameters.AddWithValue("@pagado", NpgsqlTypes.NpgsqlDbType.Numeric, pagado);

                idVenta = Convert.ToInt32(cmd.ExecuteScalar());

                int idProducto;
                int fila;
                foreach (DataGridViewRow row in dgvDetalle.Rows)
                {
                    fila = row.Index;
                    /*codigos para sacar id del producto en base al codigo de barras: el id no se suele mostrar en la factura ya que
                      es un valor propio de la empresa*/

                    var cmdID = new NpgsqlCommand("select id_producto from productos where codigo_barra = @codigo_barra", con);
                    cmdID.Parameters.AddWithValue("@codigo_barra", dgvDetalle.Rows[fila].Cells["Codigo_de_Barra"].Value);
                    idProducto = Convert.ToInt32(cmdID.ExecuteScalar());

                    var cmdDet = new NpgsqlCommand(
                        "SELECT RegistrarDetalleFactura(@id_venta,@id_producto,@cantidad,@precio_unitario,@impuesto,@descuento)", con);

                    cmdDet.Parameters.AddWithValue("@id_venta", NpgsqlTypes.NpgsqlDbType.Integer, idVenta);
                    cmdDet.Parameters.AddWithValue("@id_producto", NpgsqlTypes.NpgsqlDbType.Integer, idProducto);
                    cmdDet.Parameters.AddWithValue("@cantidad", NpgsqlTypes.NpgsqlDbType.Integer, row.Cells["cantidad"].Value);
                    cmdDet.Parameters.AddWithValue("@precio_unitario", NpgsqlTypes.NpgsqlDbType.Numeric, row.Cells["precio"].Value);
                    cmdDet.Parameters.AddWithValue("@impuesto", NpgsqlTypes.NpgsqlDbType.Numeric, row.Cells["itbis"].Value);
                    cmdDet.Parameters.AddWithValue("@descuento", NpgsqlTypes.NpgsqlDbType.Numeric, row.Cells["descuento"].Value);

                    cmdDet.ExecuteScalar();
                }
            }

            MessageBox.Show("Factura registrada correctamente");
            Funciones.Limpiar(this);
            dgvDetalle.DataSource = null;
            hayCliente = false;

        }

        public bool ValidarFactura()
        {
            if (cbTipoVenta.SelectedIndex == 0 && Convert.ToDouble(txtPagado.Text) < Convert.ToDouble(txtTotal.Text))
            {
                MessageBox.Show("Factura no puede ser al contado y el pago menor al total favor revisar");
                return false;
            }
            if (cbTipoVenta.SelectedIndex == 1 && Convert.ToDouble(txtPagado.Text) >= Convert.ToDouble(txtTotal.Text))
            {
                MessageBox.Show("Factura no puede ser a credito y el pago mayor o igual al total favor revisar");
                return false;
            }
            /*solo validar que cuando la factura sea a credito tenga cliente despues las facturas permiten el campo id_cliente null 
             para permitir que las facturas pasen sin la necesidad de tener que tener el cliente agregado, solo es necesario cuando es
             a credito*/
            if (cbTipoVenta.SelectedIndex == 1 && txtNombreCliente.Tag == null)
            {
                MessageBox.Show("Debe seleccionar cliente para poder continuar con el tipo de venta a credito");
                return false;
            }

            return true;
        }

       


        // =========================
        // EVENTOS VACÍOS (DESIGNER)
        // =========================
        private void gbCliente_Enter(object sender, EventArgs e) { }
        private void btnAnular_Click(object sender, EventArgs e) 
        {
            Funciones.Limpiar(this);
            dgvDetalle.DataSource = null;
            hayCliente = false;
        }

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
                            "p.codigo_barra as Codigo_de_Barra," +
                            " p.nombre as Nombre, " +
                            "p.precio as Precio, " +
                            "1 as cantidad, " +
                            "ROUND((p.precio * COALESCE(d.porcentaje_descuento, 0) / 100)) AS Descuento," +
                            "ROUND(p.precio - (p.precio * COALESCE(d.porcentaje_descuento, 0) / 100)) as Subtotal," +
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
                    dgvDetalle.Rows[fila].Cells["descuento"].Value = Convert.ToString(Convert.ToDouble(dgvDetalle.Rows[fila].Cells["cantidad"].Value) * Convert.ToDouble(dgvDetalle.Rows[fila].Cells["descuento"].Value));
                    
                    
                    //sacar el total y el ITBIS
                    double precio = Convert.ToDouble(dgvDetalle.Rows[fila].Cells["precio"].Value);
                    double cantidad = Convert.ToDouble(dgvDetalle.Rows[fila].Cells["cantidad"].Value);
                    double descuento = Convert.ToDouble(dgvDetalle.Rows[fila].Cells["descuento"].Value);
                    double subtotal = (cantidad * precio) - descuento;
                    double itbis = subtotal * 0.18;
                    double total = subtotal + itbis;
                    

                    dgvDetalle.Rows[fila].Cells["total"].Value = total;
                    
                    dgvDetalle.Rows[fila].Cells["itbis"].Value = itbis;

                    dgvDetalle.Rows[fila].Cells["subtotal"].Value =subtotal;
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

        private void lblUsuarioValor_Click(object sender, EventArgs e)
        {

        }

        private void lblUsuario_Click(object sender, EventArgs e)
        {

        }
    }
}
