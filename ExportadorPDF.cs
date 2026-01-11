using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows.Forms;

public static class ExportadorPDF
{
    public static void ExportarPDF(DataGridView dgv, string titulo)
    {
        SaveFileDialog sfd = new SaveFileDialog();
        sfd.Filter = "Archivo PDF (*.pdf)|*.pdf";

        if (sfd.ShowDialog() == DialogResult.OK)
        {
            Document doc = new Document(PageSize.A4.Rotate());
            PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
            doc.Open();

            Paragraph p = new Paragraph(titulo + "\n\n");
            p.Alignment = Element.ALIGN_CENTER;
            doc.Add(p);

            PdfPTable table = new PdfPTable(dgv.Columns.Count);

            foreach (DataGridViewColumn col in dgv.Columns)
                table.AddCell(col.HeaderText);

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                        table.AddCell(cell.Value?.ToString());
                }
            }

            doc.Add(table);
            doc.Close();
        }
    }
}

