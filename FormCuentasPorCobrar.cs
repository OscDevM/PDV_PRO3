using Npgsql;
using PDV_PRO3;
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

        // EVENTO LOAD DEL FORM
        private void FrmCuentasPorCobrar_Load(object sender, EventArgs e)
        {
            CargarCxC();
        }

        // BOTÓN PARA RECARGAR LAS CUENTAS POR COBRAR
        private void btnCargar_Click(object sender, EventArgs e)
        {
            CargarCxC();
        }

        // CARGA TODAS LAS FACTURAS DE CUENTAS POR COBRAR
        private void CargarCxC()
        {
            try
            {
                using (var con = Conexion.GetConexion())
                {
                    con.Open();

                    // SALDO = MONTO PAGADO
                    // PENDIENTE = TOTAL - SALDO

                    string sql = @"
                                  SELECT 
                                      c.id_cxc,
                                      v.id_venta,
                                      cl.nombre AS cliente,
                                      c.total,
                                      c.saldo AS pagado,
                                     (c.total - c.saldo) AS pendiente,
                                         c.fecha_vencimiento,
                                      c.estado
                                     FROM cxc c
                                    INNER JOIN ventas v ON v.id_venta = c.id_venta
                                    INNER JOIN clientes cl ON cl.id_cliente = v.id_cliente
                                    WHERE 1=1
                                    ";

                    if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
                    {
                        if (cboFiltro.Text == "Cliente")
                            sql += " AND LOWER(cl.nombre) LIKE LOWER(@valor)";
                        else if (cboFiltro.Text == "Factura")
                            sql += " AND v.id_venta::text LIKE @valor";
                        else if (cboFiltro.Text == "Estado")
                            sql += " AND c.estado = @valor";
                    }


                    using (var da = new NpgsqlDataAdapter(sql, con))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvCxC.DataSource = dt;
                    }
                }

                // FORMATEAR GRID Y MARCAR FACTURAS VENCIDAS
                FormatearGrid();

                // RESALTAR FACTURAS VENCIDAS 

                MarcarVencidas();

            }
            // MENSAJE ERROR POR PROBLEMAS DE CONEXION PARA CARGAR LAS CUENTAS

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

        // FORMATO DEL DATAGRIDVIEW
        private void FormatearGrid()
        {
            if (dgvCxC.Columns.Count == 0) return;

            dgvCxC.Columns["id_cxc"].Visible = false;

            dgvCxC.Columns["id_venta"].HeaderText = "Factura";
            dgvCxC.Columns["cliente"].HeaderText = "Cliente";
            dgvCxC.Columns["total"].HeaderText = "Total";
            dgvCxC.Columns["pagado"].HeaderText = "Pagado";
            dgvCxC.Columns["pendiente"].HeaderText = "Pendiente";
            dgvCxC.Columns["fecha_vencimiento"].HeaderText = "Vence";
            dgvCxC.Columns["estado"].HeaderText = "Estado";

            dgvCxC.Columns["total"].DefaultCellStyle.Format = "C2";
            dgvCxC.Columns["pagado"].DefaultCellStyle.Format = "C2";
            dgvCxC.Columns["pendiente"].DefaultCellStyle.Format = "C2";
            dgvCxC.Columns["fecha_vencimiento"].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvCxC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        // MARCAR FACTURAS VENCIDAS (SOLO VISUAL)
        private void MarcarVencidas()
        {
            foreach (DataGridViewRow row in dgvCxC.Rows)
            {
                if (row.Cells["fecha_vencimiento"].Value == DBNull.Value) continue;

                DateTime vencimiento = Convert.ToDateTime(row.Cells["fecha_vencimiento"].Value);
                string estado = row.Cells["estado"].Value.ToString();

                //VENCIDA Y NO PAGA

                if (vencimiento < DateTime.Today && estado != "pagada")
                {
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
            }
        }

        // BOTÓN PAGAR FACTURA
        private void btnPagar_Click_1(object sender, EventArgs e)
        {
            // Validar selección
            if (dgvCxC.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una cuenta por cobrar");
                return;
            }

            DataGridViewRow fila = dgvCxC.SelectedRows[0];

            int idCxC = Convert.ToInt32(fila.Cells["id_cxc"].Value);
            int idVenta = Convert.ToInt32(fila.Cells["id_venta"].Value);
            string cliente = fila.Cells["cliente"].Value.ToString();
            decimal total = Convert.ToDecimal(fila.Cells["total"].Value);
            decimal pagado = Convert.ToDecimal(fila.Cells["pagado"].Value);
            decimal pendiente = Convert.ToDecimal(fila.Cells["pendiente"].Value);

            // Si ya está pagada, no permitir pago
            if (pendiente <= 0)
            {
                MessageBox.Show("Esta factura ya está saldada");
                return;
            }

            // Abrir formulario de pago
            using (var frm = new FrmPagoCxC(
                idCxC,
                idVenta,
                cliente,
                total,
                pagado,
                pendiente
            ))
            {
                frm.ShowDialog();
            }

            // Refrescar listado al volver
            CargarCxC();
        }
        private void dgvCxC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evento requerido por el Designer (no se usa)
        }

    }
}
      

        
    

