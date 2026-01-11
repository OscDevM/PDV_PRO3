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
    public partial class Usuario : Form
    {
        bool insertar = true;
        int idUsuario;

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
                "u.usuario, " +
                "u.password AS contraseña, " +
                "r.nombre AS rol, " +
                "u.activo " +
                "FROM usuarios u " +
                "INNER JOIN roles r ON u.id_rol = r.id_rol ORDER BY u.id_usuario;");

            cbRol.DisplayMember = "nombre";
            cbRol.ValueMember = "id_rol";
            cbRol.DataSource = Funciones.LlamarDatos("SELECT id_rol, nombre FROM roles;");
        }

        private void cbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void bttnGuardar_Click(object sender, EventArgs e)
        {
            if (Funciones.Verificar(this) == false)
            {
                MessageBox.Show("Porfavor llenar todos los campos");
                return;
            }

            int idRol = Convert.ToInt32(cbRol.SelectedValue);

            using (var conn = Conexion.GetConexion())
            {

                conn.Open();
                if (insertar)
                {
                    string sql = "INSERT INTO usuarios (nombre, usuario, password, id_rol) VALUES (@nombre, @usuario, @password , @id_rol);";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                        cmd.Parameters.AddWithValue("@password", txtContrasena.Text);
                        cmd.Parameters.AddWithValue("@id_rol", idRol);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    string sql = @"UPDATE usuarios 
                   SET nombre=@nombre, usuario=@usuario, password=@password, id_rol=@id_rol, activo = @activo 
                   WHERE id_usuario=@id_usuario";

                    bool activo;
                    if (cbEstatus.SelectedIndex == 0)
                    {
                        activo = true;
                    }
                    else
                    {
                        activo = false;
                    }
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                        cmd.Parameters.AddWithValue("@password", txtContrasena.Text);
                        cmd.Parameters.AddWithValue("@id_rol", idRol);
                        cmd.Parameters.AddWithValue("@activo", activo);
                        cmd.Parameters.AddWithValue("@id_usuario", idUsuario);
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            Funciones.Limpiar(this);
            insertar = true;
            cbEstatus.Visible = false;
            label4.Visible = false;
            LlenarDatos();
        }

        private void dgvUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(ClaseUsuario._idusuario == Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id_usuario"].Value))
            {
                MessageBox.Show("No puede editar el rol del mismo usuario en uso");
                cbRol.Enabled = false;
            }

            insertar = false;
            idUsuario = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id_usuario"].Value);
            txtNombre.Text = dgvUsuarios.CurrentRow.Cells["nombre"].Value.ToString();
            txtUsuario.Text = dgvUsuarios.CurrentRow.Cells["usuario"].Value.ToString();
            txtContrasena.Text = dgvUsuarios.CurrentRow.Cells["contraseña"].Value.ToString();
            cbRol.Text = dgvUsuarios.Rows[e.RowIndex].Cells["rol"].Value.ToString();
            if (Convert.ToBoolean(dgvUsuarios.CurrentRow.Cells["activo"].Value) == true)
            {
                cbEstatus.SelectedIndex = 0;
            }
            else
            {
                cbEstatus.SelectedIndex = 1;
            }
            cbEstatus.Visible = true;
            label4.Visible = true;
        }

        private void bttnCancelar_Click(object sender, EventArgs e)
        {
            Funciones.Limpiar(this);
            insertar = true;
            cbEstatus.Visible = false;
            label4.Visible = false;
            LlenarDatos();

        }
    }
}
