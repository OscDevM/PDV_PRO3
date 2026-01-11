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
    public partial class Usuario : Form
    {
        bool insertar;
        int idUsuario;
        int idRol;

        public Usuario()
        {
            InitializeComponent();
        }

        private void Usuario_Load(object sender, EventArgs e)
        {
            LlenarDatos();
        }

        private void LlenarDatos()
        {
            dgvUsuarios.DataSource = Funciones.LlamarDatos("SELECT " +
                "u.id_usuario, " +
                "u.nombre, " +
                "u.usuario," +
                "u.activo, " +
                "r.nombre AS rol " +
                "FROM usuarios u " +
                "INNER JOIN roles r ON u.id_rol = r.id_rol;");

            cbRol.DataSource = Funciones.LlamarDatos("SELECT nombres FROM roles;");
        }
    }
}
