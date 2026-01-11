using System.IO;
using System.Text;
using System.Windows.Forms;

public static class Exportador
{
    public static void ExportarExcel(DataGridView dgv)
    {
        SaveFileDialog sfd = new SaveFileDialog();
        sfd.Filter = "Archivo CSV (*.csv)|*.csv";

        if (sfd.ShowDialog() == DialogResult.OK)
        {
            StringBuilder sb = new StringBuilder();

            foreach (DataGridViewColumn col in dgv.Columns)
                sb.Append(col.HeaderText + ",");

            sb.AppendLine();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                        sb.Append(cell.Value + ",");
                    sb.AppendLine();
                }
            }

            File.WriteAllText(sfd.FileName, sb.ToString());
        }
    }
}

