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
    public partial class FrmVentasDiarias : Form
    {
        ReportesDAOS dao = new ReportesDAOS();

        public FrmVentasDiarias()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvVentas.DataSource = dao.ObtenerVentasDiarias(dtpFecha.Value);
            lblTotal.Text = "Total vendido: RD$ " +
                dao.ObtenerTotalVentas(dtpFecha.Value).ToString("N2");
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvVentas.DataSource == null || dgvVentas.Rows.Count == 0)
            {
                MessageBox.Show(
                    "Primero genere el reporte de ventas.",
                    "Ventas diarias",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            Exportador.ExportarExcel(dgvVentas);
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            if (dgvVentas.DataSource == null || dgvVentas.Rows.Count == 0)
            {
                MessageBox.Show(
                    "Primero genere el reporte de ventas.",
                    "Ventas diarias",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            ExportadorPDF.ExportarPDF(
                dgvVentas,
                "Reporte de Ventas Diarias - " + dtpFecha.Value.ToShortDateString()
            );
        }
    }

}

