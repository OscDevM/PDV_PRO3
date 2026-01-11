
using FrmFacturacion;
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
    public partial class FrmPagoCxC : Form
    {
        public FrmPagoCxC()
        {
            InitializeComponent();
        }
        private void FrmPagoCxC_Load(object sender, EventArgs e)
        {

        }
        private void RegistrarPago(decimal monto)
        {
            if (monto <= 0 || monto > _pendiente)
            {
                MessageBox.Show("Monto inválido");
                return;
            }

            try
            {
                using (var con = Conexion.GetConexion())
                {
                    con.Open();
                    using (var tx = con.BeginTransaction())
                    {
                        // INSERTAR PAGO
                        string sqlPago = @"
                    INSERT INTO pagos (id_venta, metodo, monto, usuario)
                    VALUES (@venta, 'efectivo', @monto, @usuario);
                ";

                        var r = MessageBox.Show(
                            $"¿Confirmar pago de {monto:C2}?",
                            "Confirmar pago",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (r != DialogResult.Yes)
                            return;

                        using (var cmd = new NpgsqlCommand(sqlPago, con))
                        {
                            cmd.Parameters.AddWithValue("@venta", _idVenta);
                            cmd.Parameters.AddWithValue("@monto", monto);
                            cmd.Parameters.AddWithValue("@usuario", ClaseUsuario._idusuario);
                            cmd.ExecuteNonQuery();
                        }

                        // ACTUALIZAR CxC
                        string sqlCxC = @"
                    UPDATE cxc
                    SET saldo = saldo + @monto,
                        estado = CASE
                            WHEN saldo + @monto >= total THEN 'pagada'
                            ELSE 'parcial'
                        END
                    WHERE id_cxc = @id;
                ";

                        using (var cmd = new NpgsqlCommand(sqlCxC, con))
                        {
                            cmd.Parameters.AddWithValue("@monto", monto);
                            cmd.Parameters.AddWithValue("@id", _idCxC);
                            cmd.ExecuteNonQuery();
                        }

                        tx.Commit();
                    }
                }

                MessageBox.Show("Pago registrado correctamente");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al registrar el pago:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            //  MENSAJE PRIMERO
            MessageBox.Show(
                "Pago registrado correctamente",
                "Pago",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

          
            Close();
        }


        private int _idCxC;
        private int _idVenta;
        private decimal _pendiente;

        public FrmPagoCxC(
            int idCxC,
            int idVenta,
            string cliente,
            decimal total,
            decimal pagado,
            decimal pendiente)
        {
            InitializeComponent();

            _idCxC = idCxC;
            _idVenta = idVenta;
            _pendiente = pendiente;

            lblCliente.Text = cliente;
            lblFactura.Text = idVenta.ToString();
            lblTotal.Text = total.ToString("C2");
            lblPagado.Text = pagado.ToString("C2");
            lblPendiente.Text = pendiente.ToString("C2");
        }

        private void btnAgregarPago_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmInputMonto(_pendiente))
            {
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                decimal monto = frm.Monto;

                // CONFIRMACIÓN DEL USUARIO
                var resp = MessageBox.Show(
                    $"¿Confirmar pago de {monto:C2}?",
                    "Confirmar pago",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (resp != DialogResult.Yes)
                    return;

                RegistrarPago(monto);
            }
        }
        // BOTÓN CERRAR → VOLVER A CxC
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();

            // Volver a Cuentas por Cobrar
            FormCuentasPorCobrar frm = new FormCuentasPorCobrar();
            frm.Show();
        }
    }
}
