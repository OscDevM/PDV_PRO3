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
    public partial class Descuentos : Form
    {
        int idProducto;
        int idDescuento;
        bool insertar;

        public Descuentos()
        {
            InitializeComponent();
        }

        private void Descuentos_Load(object sender, EventArgs e)
        {
            LlamarDatos();
        }

        

        public void LlamarDatos()
        {
            using (var conn = Conexion.GetConexion())
            {
                conn.Open();

                string sql = "SELECT " +
                    "d.id_descuento, " +
                    "p.codigo_barra, " +
                    "p.nombre AS nombre_producto, " +
                    "d.descuento, " +
                    "d.porcentaje_descuento, " +
                    "d.activo " +
                    "FROM descuentos d " +
                    "INNER JOIN productos p ON d.id_producto = p.id_producto;";

                using (var da = new NpgsqlDataAdapter(sql, conn))
                {

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvDescuentos.DataSource = dt;
                }
            }
        }

        private void txtCodigoBarras_TextChanged(object sender, EventArgs e)
        {
            if(txtCodigoBarras.TextLength== 13)
            {
                using (var con = Conexion.GetConexion())
                {
                    con.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT " +
                        "id_producto, " +
                        "nombre, " +
                        "precio " +
                        "FROM productos WHERE codigo_barra = @codigo_barra;", con);

                    cmd.Parameters.AddWithValue("@codigo_barra", txtCodigoBarras.Text);

                    using (var da = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if(dt.Rows.Count == 1)
                        {
                            idProducto = Convert.ToInt32(dt.Rows[0]["id_producto"]);
                            txtNombre.Text = dt.Rows[0]["nombre"].ToString();
                            txtPrecio.Text = dt.Rows[0]["precio"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Producto no encontrado favor revisar codigo de barras");
                        }
                    }

                    
                }

                
            }
        }
        

        private void txtPorcentajeDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void dgvDescuentos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            insertar = false;
            idDescuento = Convert.ToInt32(dgvDescuentos.CurrentRow.Cells["id_descuento"].Value);
            txtCodigoBarras.Text = dgvDescuentos.CurrentRow.Cells["codigo_barra"].Value.ToString();
            txtDescuento.Text = dgvDescuentos.CurrentRow.Cells["descuento"].Value.ToString();
            txtPorcentajeDescuento.Text = dgvDescuentos.CurrentRow.Cells["porcentaje_descuento"].Value.ToString();

            if (Convert.ToBoolean(dgvDescuentos.CurrentRow.Cells["activo"].Value) == true)
            {
                cbEstatus.SelectedIndex = 0;
            }
            else
            {
                cbEstatus.SelectedIndex = 1;
            }
            cbEstatus.Visible = true;
        }

        private void bttnGuardar_Click(object sender, EventArgs e)
        {
            if (Funciones.Verificar(this) == false)
            {
                MessageBox.Show("Porfavor llenar todos los campos");
                return;
            }

            using (var conn = Conexion.GetConexion())
            {
                conn.Open();
                if (insertar)
                {
                    string sql = "INSERT INTO descuentos (descuento, porcentaje_descuento,id_producto) VALUES (@descuento, @porcentaje_descuento,@id_producto)";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@descuento", txtDescuento.Text);
                        cmd.Parameters.AddWithValue("@porcentaje_descuento", NpgsqlTypes.NpgsqlDbType.Integer, Convert.ToInt32(txtPorcentajeDescuento.Text));
                        cmd.Parameters.AddWithValue("@id_producto", idProducto);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    string sql = @"UPDATE descuentos 
                   SET descuento=@descuento, porcentaje_descuento=@porcentaje_descuento, id_producto=@id_producto, activo = @activo 
                   WHERE id_descuento=@id_descuento";

                    bool activo;
                    if (cbEstatus.SelectedIndex == 0)
                    {
                        activo = true;
                    }
                    else
                    {
                        activo = false;
                    }
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@descuento", txtDescuento.Text);
                        cmd.Parameters.AddWithValue("@porcentaje_descuento",NpgsqlTypes.NpgsqlDbType.Integer,Convert.ToInt32(txtPorcentajeDescuento.Text));
                        cmd.Parameters.AddWithValue("@activo", activo);
                        cmd.Parameters.AddWithValue("@id_producto", idProducto);
                        cmd.Parameters.AddWithValue("@id_descuento", idDescuento);
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            Funciones.Limpiar(this);
            insertar = true;
            cbEstatus.Visible = false;
            LlamarDatos();
        }

        private void bttnCancelar_Click(object sender, EventArgs e)
        {
            Funciones.Limpiar(this);
            insertar = true;
            cbEstatus.Visible = false;
            LlamarDatos();
        }
    }
}
