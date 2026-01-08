using Login_con_Métodos_en_C_2;
using Npgsql;
using System;
using System.Windows.Forms;

namespace PDV_PRO3
{
    public partial class FormLogin : Form
    {
        private readonly string cadenaConexion;

        public FormLogin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {

        }



        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void BtnIniciar_sesión_Click(object sender, EventArgs e)
        {
            {
                ClaDatos Datos = new ClaDatos();
                if (Datos.Entrar(TxtUsuario.Text, TxtContraseña.Text) == true)
                {
                    Form Formulario = new FormMenuPrincipal();
                    Formulario.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Usuario o Clave incorrecta, Por favor verificar");
                    TxtUsuario.Focus();
                    TxtUsuario.Clear();
                    TxtContraseña.Clear();
                }
            }

        }

        private void BtnSalir_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}