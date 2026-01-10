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
    public partial class Categoria : Form
    {
        public Categoria()
        {
            InitializeComponent();
        }

        bool insertar = true;
        int idCategoria;

        private void Categoria_Load(object sender, EventArgs e)
        {
            using (var conn = Conexion.GetConexion())
            {
                conn.Open();

                string sql = "SELECT * FROM categoria_productos ORDER BY id_categoria";
                using (var da = new NpgsqlDataAdapter(sql, conn))
                {

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvCategorias.DataSource = dt;
                }
            }
        }

        private void dgvCategorias_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            insertar = false;
            idCategoria =Convert.ToInt32(dgvCategorias.CurrentRow.Cells["id_categoria"].Value);
            txtNombre.Text = dgvCategorias.CurrentRow.Cells["nombre"].Value.ToString();
            txtDescripcion.Text = dgvCategorias.CurrentRow.Cells["descripcion"].Value.ToString();

            if(dgvCategorias.CurrentRow.Cells["activo"].Value.ToString() == "TRUE")
            {
                cbEstatus.SelectedIndex = 0;
            }
            else
            {
                cbEstatus.SelectedIndex = 1;
            }
        }
    }
}
