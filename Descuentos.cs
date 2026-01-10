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
            idProducto = Convert.ToInt32(dgvDescuentos.CurrentRow.Cells["id_categoria"].Value);
            txtCodigoBarras.Text = dgvDescuentos.CurrentRow.Cells["nombre"].Value.ToString();
            txtNombre.Text = dgvDescuentos.CurrentRow.Cells["nombre"].Value.ToString();
            //txtDescripcion.Text = dgvDescuentos.CurrentRow.Cells["descripcion"].Value.ToString();

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
    }
}
