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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        ClaseUsuario _claseUsuario = new ClaseUsuario();

        private void bttnAceptar_Click(object sender, EventArgs e)
        {
            if(txtUsuario.TextLength == 0 || txtPassword.TextLength == 0)
            {
                MessageBox.Show("Porfavor llenar todos los campos");
                return;
            }

            if (_claseUsuario.VerificarUsuario(txtUsuario.Text, txtPassword.Text))
            {
                MessageBox.Show(ClaseUsuario._idusuario+"");
                Form1 menuprincipal = new Form1();
                menuprincipal.login = this;
                menuprincipal.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o Contraseña equivocada");
            }
            
        }
    }
}
