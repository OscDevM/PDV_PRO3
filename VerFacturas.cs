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
    public partial class VerFacturas : Form
    {
        public VerFacturas()
        {
            InitializeComponent();
        }

        private void VerFacturas_Load(object sender, EventArgs e)
        {
            LlamarDatos();
            cbBuscarPor.SelectedIndex = 0;
        }

        public void LlamarDatos()
        {
            using (var conn = Conexion.GetConexion())
            {
                conn.Open();

                string sql = "SELECT * From Ventas;";

                using (var da = new NpgsqlDataAdapter(sql, conn))
                {

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvFacturas.DataSource = dt;
                }
            }
        }

        private void bttnBuscar_Click(object sender, EventArgs e)
        {
            if (Funciones.Verificar(this) == false)
            {
                MessageBox.Show("Porfavor llenar todos los campos");
                return;
            }

            if(cbBuscarPor.SelectedIndex == 0)
            {
                using (var conn = Conexion.GetConexion())
                {
                    conn.Open();

                     NpgsqlCommand cmd = new NpgsqlCommand("SELECT * From Ventas WHERE id_venta = @id_venta;", conn);

                    cmd.Parameters.AddWithValue("@id_venta", Convert.ToInt32(txtBuscar.Text));
                    using (var da = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvFacturas.DataSource = dt;
                    }
                }
            }
            else
            {
                using (var conn = Conexion.GetConexion())
                {
                    conn.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT * From Ventas WHERE id_cliente = @id_cliente;", conn);

                    cmd.Parameters.AddWithValue("@id_cliente", Convert.ToInt32(txtBuscar.Text));
                    using (var da = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvFacturas.DataSource = dt;
                    }
                }
            }
        }
    }
}
