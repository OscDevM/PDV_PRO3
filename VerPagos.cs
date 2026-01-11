using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDV_PRO3
{
    public partial class VerPagos : Form
    {
        public VerPagos()
        {
            InitializeComponent();
        }

        private void VerPagos_Load(object sender, EventArgs e)
        {
            LlenarDatagrid();
            cbBuscarPor.SelectedIndex = 0;
        }

        public void LlenarDatagrid()
        {
            dgvCXC.DataSource = Funciones.LlamarDatos("SELECT " +
                "c.id_cxc, " +
                "v.id_venta, " +
                "cl.nombre AS cliente, " +
                "c.total, " +
                "c.saldo AS pagado, " +
                "(c.total - c.saldo) AS pendiente, " +
                "c.fecha_vencimiento, " +
                "c.estado " +
                "FROM cxc c " +
                "INNER JOIN ventas v ON v.id_venta = c.id_venta " +
                "INNER JOIN clientes cl ON cl.id_cliente = v.id_cliente " +
                "WHERE 1 = 1;");
        }

        private void bttnBuscar_Click(object sender, EventArgs e)
        {
            if (Funciones.Verificar(this) == false)
            {
                MessageBox.Show("Porfavor llenar todos los campos");
                return;
            }

            if (cbBuscarPor.SelectedIndex == 0)
            {
                using (var conn = Conexion.GetConexion())
                {
                    conn.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT " +
                        "c.id_cxc, " +
                        "v.id_venta, " +
                        "cl.nombre AS cliente, " +
                        "c.total, " +
                        "c.saldo AS pagado, " +
                        "(c.total - c.saldo) AS pendiente, " +
                        "c.fecha_vencimiento, " +
                        "c.estado " +
                        "FROM cxc c " +
                        "INNER JOIN ventas v ON v.id_venta = c.id_venta " +
                        "INNER JOIN clientes cl ON cl.id_cliente = v.id_cliente " +
                        "WHERE v.id_venta = @id_venta;", conn);

                    cmd.Parameters.AddWithValue("@id_venta", Convert.ToInt32(txtBuscar.Text));
                    using (var da = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvCXC.DataSource = dt;
                    }
                }
            }
            else
            {
                using (var conn = Conexion.GetConexion())
                {
                    conn.Open();

                    NpgsqlCommand cmd = new NpgsqlCommand("SELECT " +
                        "c.id_cxc, " +
                        "v.id_venta, " +
                        "cl.nombre AS cliente, " +
                        "c.total, " +
                        "c.saldo AS pagado, " +
                        "(c.total - c.saldo) AS pendiente, " +
                        "c.fecha_vencimiento, " +
                        "c.estado " +
                        "FROM cxc c " +
                        "INNER JOIN ventas v ON v.id_venta = c.id_venta " +
                        "INNER JOIN clientes cl ON cl.id_cliente = v.id_cliente " +
                        "WHERE cl.id_cliente = @id_cliente;", conn);

                    cmd.Parameters.AddWithValue("@id_cliente", Convert.ToInt32(txtBuscar.Text));
                    using (var da = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvCXC.DataSource = dt;
                    }
                }
            }
        }

        private void bttnCancelar_Click(object sender, EventArgs e)
        {
            Funciones.Limpiar(this);
            LlenarDatagrid();
            dgvPagos.DataSource = null;
        }

        private void dgvCXC_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            using (var conn = Conexion.GetConexion())
            {
                conn.Open();

                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * from pagos WHERE id_venta =@id_venta;", conn);

                cmd.Parameters.AddWithValue("@id_venta", Convert.ToInt32(dgvCXC.CurrentRow.Cells["id_venta"].Value));
                using (var da = new NpgsqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvPagos.DataSource = dt;
                }
            }
        }
    }
}
