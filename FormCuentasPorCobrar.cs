using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FrmFacturacion
{
    public partial class FormCuentasPorCobrar : Form
    {
        public FormCuentasPorCobrar()
        {
            InitializeComponent();
        }

        private void FrmCuentasPorCobrar_Load(object sender, EventArgs e)
        {
            CargarCxC();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            CargarCxC();
        }

        private void CargarCxC()
        {
            try
            {
                using (var con = Conexion.GetConexion())
                {
                    con.Open();

                    string sql = @"
                        SELECT 
                            cxc.id_cxc,
                            v.id_venta,
                            cl.nombre AS cliente,
                            cxc.total,
                            cxc.saldo,
                            cxc.fecha_vencimiento,
                            cxc.estado
                        FROM cxc
                        INNER JOIN ventas v ON v.id_venta = cxc.id_venta
                        INNER JOIN clientes cl ON cl.id_cliente = v.id_cliente
                        WHERE cxc.estado <> 'pagada'
                        ORDER BY cxc.fecha_vencimiento;
                    ";

                    using (var da = new NpgsqlDataAdapter(sql, con))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvCxC.DataSource = dt;
                    }
                }

                FormatearGrid();
                MarcarVencidas();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar las cuentas por cobrar:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void FormatearGrid()
        {
            if (dgvCxC.Columns.Count == 0) return;

            dgvCxC.Columns["id_cxc"].Visible = false;
            dgvCxC.Columns["id_venta"].HeaderText = "Factura";
            dgvCxC.Columns["cliente"].HeaderText = "Cliente";
            dgvCxC.Columns["total"].HeaderText = "Total";
            dgvCxC.Columns["saldo"].HeaderText = "Saldo";
            dgvCxC.Columns["fecha_vencimiento"].HeaderText = "Vence";
            dgvCxC.Columns["estado"].HeaderText = "Estado";

            dgvCxC.Columns["total"].DefaultCellStyle.Format = "C2";
            dgvCxC.Columns["saldo"].DefaultCellStyle.Format = "C2";
            dgvCxC.Columns["fecha_vencimiento"].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvCxC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void MarcarVencidas()
        {
            foreach (DataGridViewRow row in dgvCxC.Rows)
            {
                if (row.Cells["fecha_vencimiento"].Value == DBNull.Value) continue;

                DateTime vencimiento = Convert.ToDateTime(row.Cells["fecha_vencimiento"].Value);
                string estado = row.Cells["estado"].Value.ToString();

                if (vencimiento < DateTime.Today && estado != "pagada")
                {
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
            }
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (dgvCxC.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una factura");
                return;
            }

            int idCxC = Convert.ToInt32(dgvCxC.SelectedRows[0].Cells["id_cxc"].Value);
            decimal saldo = Convert.ToDecimal(dgvCxC.SelectedRows[0].Cells["saldo"].Value);

            if (saldo <= 0)
            {
                MessageBox.Show("La factura ya está pagada");
                return;
            }

            if (MessageBox.Show(
                "¿Desea marcar esta factura como pagada?",
                "Confirmar pago",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (var con = Conexion.GetConexion())
                {
                    con.Open();
                    using (var tx = con.BeginTransaction())
                    {
                        string sql = @"
                            UPDATE cxc
                            SET saldo = 0,
                                estado = 'pagada'
                            WHERE id_cxc = @id;
                        ";

                        using (var cmd = new NpgsqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@id", idCxC);
                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                    }
                }

                MessageBox.Show("Factura pagada correctamente");
                CargarCxC();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al pagar la factura:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvCxC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
