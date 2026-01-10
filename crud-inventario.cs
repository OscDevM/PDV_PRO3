using System;
using System.Data;
using System.Windows.Forms;
using static PDV_PRO3.InventarioDAO;

namespace PDV_PRO3
{
    public partial class crud_inventario : Form
    {
        int idSeleccionado = 0; 

        public crud_inventario()
        {
            InitializeComponent();
        }

        private void crud_inventario_Load(object sender, EventArgs e)
        {
            CargarInventario();
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            Inventario inv = new Inventario
            {
                Lugar = TxtLugar.Text,
                Tramo = TxtTramo.Text
            };

            InventarioDAO.Insertar(inv);
            CargarInventario();
            Limpiar();
        }

        private void DgvInventario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DgvInventario.CurrentRow != null)
            {
                idSeleccionado = Convert.ToInt32(
                    DgvInventario.CurrentRow.Cells["id_inventario"].Value);

                TxtLugar.Text = DgvInventario.CurrentRow.Cells["lugar"].Value.ToString();
                TxtTramo.Text = DgvInventario.CurrentRow.Cells["tramo"].Value.ToString();
            }
        }


        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un registro");
                return;
            }

            Inventario inv = new Inventario
            {
                IdInventario = idSeleccionado,
                Lugar = TxtLugar.Text,
                Tramo = TxtTramo.Text
            };

            InventarioDAO.Actualizar(inv);
            CargarInventario();
            Limpiar();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un registro");
                return;
            }

            InventarioDAO.Eliminar(idSeleccionado);
            CargarInventario();
            Limpiar();
        }

        private void CargarInventario()
        {
            DgvInventario.DataSource = InventarioDAO.Listar();
            DgvInventario.Columns["id_inventario"].Visible = false;
        }

        private void Limpiar()
        {
            TxtLugar.Clear();
            TxtTramo.Clear();
            idSeleccionado = 0;
        }

        private void crud_inventario_Load_1(object sender, EventArgs e)
        {

        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();

        }
    }
}
