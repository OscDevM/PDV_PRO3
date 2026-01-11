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
    public partial class FrmStockBajo : Form
    {
        ReportesDAOS dao = new ReportesDAOS();

        public FrmStockBajo()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvStock.AutoGenerateColumns = true;

            dgvStock.DataSource = dao.ObtenerStockBajo((int)numUmbral.Value);

            if (dgvStock.Rows.Count == 0)
            {
                MessageBox.Show(
                    "No se encontraron productos con stock bajo.",
                    "Stock bajo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvStock.DataSource == null || dgvStock.Rows.Count == 0)
            {
                MessageBox.Show(
                    "Primero genere el reporte de stock bajo.",
                    "Stock bajo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            Exportador.ExportarExcel(dgvStock);
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            if (dgvStock.DataSource == null || dgvStock.Rows.Count == 0)
            {
                MessageBox.Show(
                    "Primero genere el reporte de stock bajo.",
                    "Stock bajo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            ExportadorPDF.ExportarPDF(
                dgvStock,
                "Reporte de Productos con Stock Bajo"
            );
        }
    }

}
