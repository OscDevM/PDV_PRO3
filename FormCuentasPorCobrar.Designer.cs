namespace FrmFacturacion
{
    partial class FormCuentasPorCobrar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.dgvCxC = new System.Windows.Forms.DataGridView();
            this.colIdVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSaldo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVencimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbPago = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnPagar = new System.Windows.Forms.Button();
            this.cbMetodoPago = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMontoPago = new System.Windows.Forms.TextBox();
            this.txtSaldo = new System.Windows.Forms.TextBox();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.txtIdVenta = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCxC)).BeginInit();
            this.gbPago.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(252, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cuentas por Cobrar";
            // 
            // dgvCxC
            // 
            this.dgvCxC.AllowUserToAddRows = false;
            this.dgvCxC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCxC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIdVenta,
            this.colCliente,
            this.colTotal,
            this.colSaldo,
            this.colVencimiento,
            this.colEstado});
            this.dgvCxC.Location = new System.Drawing.Point(27, 114);
            this.dgvCxC.Name = "dgvCxC";
            this.dgvCxC.ReadOnly = true;
            this.dgvCxC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCxC.Size = new System.Drawing.Size(644, 150);
            this.dgvCxC.TabIndex = 1;
            this.dgvCxC.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCxC_CellContentClick);
            // 
            // colIdVenta
            // 
            this.colIdVenta.HeaderText = "ID Venta";
            this.colIdVenta.Name = "colIdVenta";
            this.colIdVenta.ReadOnly = true;
            // 
            // colCliente
            // 
            this.colCliente.HeaderText = "Cliente";
            this.colCliente.Name = "colCliente";
            this.colCliente.ReadOnly = true;
            // 
            // colTotal
            // 
            this.colTotal.HeaderText = "Total";
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;
            // 
            // colSaldo
            // 
            this.colSaldo.HeaderText = "Saldo";
            this.colSaldo.Name = "colSaldo";
            this.colSaldo.ReadOnly = true;
            // 
            // colVencimiento
            // 
            this.colVencimiento.HeaderText = "Vencimiento";
            this.colVencimiento.Name = "colVencimiento";
            this.colVencimiento.ReadOnly = true;
            // 
            // colEstado
            // 
            this.colEstado.HeaderText = "Estado";
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            // 
            // gbPago
            // 
            this.gbPago.Controls.Add(this.label6);
            this.gbPago.Controls.Add(this.btnPagar);
            this.gbPago.Controls.Add(this.cbMetodoPago);
            this.gbPago.Controls.Add(this.label5);
            this.gbPago.Controls.Add(this.label4);
            this.gbPago.Controls.Add(this.label3);
            this.gbPago.Controls.Add(this.label2);
            this.gbPago.Controls.Add(this.txtMontoPago);
            this.gbPago.Controls.Add(this.txtSaldo);
            this.gbPago.Controls.Add(this.txtCliente);
            this.gbPago.Controls.Add(this.txtIdVenta);
            this.gbPago.Location = new System.Drawing.Point(44, 296);
            this.gbPago.Name = "gbPago";
            this.gbPago.Size = new System.Drawing.Size(488, 212);
            this.gbPago.TabIndex = 2;
            this.gbPago.TabStop = false;
            this.gbPago.Text = "Registrar Pago";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(283, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Método:";
            // 
            // btnPagar
            // 
            this.btnPagar.Location = new System.Drawing.Point(335, 118);
            this.btnPagar.Name = "btnPagar";
            this.btnPagar.Size = new System.Drawing.Size(109, 53);
            this.btnPagar.TabIndex = 9;
            this.btnPagar.Text = "Pagar";
            this.btnPagar.UseVisualStyleBackColor = true;
            // 
            // cbMetodoPago
            // 
            this.cbMetodoPago.FormattingEnabled = true;
            this.cbMetodoPago.Items.AddRange(new object[] {
            "Efectivo",
            "Tarjeta",
            "Transferencia"});
            this.cbMetodoPago.Location = new System.Drawing.Point(344, 52);
            this.cbMetodoPago.Name = "cbMetodoPago";
            this.cbMetodoPago.Size = new System.Drawing.Size(121, 21);
            this.cbMetodoPago.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(57, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Monto:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Saldo:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Cliente:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Factura #:";
            // 
            // txtMontoPago
            // 
            this.txtMontoPago.Location = new System.Drawing.Point(134, 170);
            this.txtMontoPago.Name = "txtMontoPago";
            this.txtMontoPago.Size = new System.Drawing.Size(100, 20);
            this.txtMontoPago.TabIndex = 3;
            // 
            // txtSaldo
            // 
            this.txtSaldo.Location = new System.Drawing.Point(134, 131);
            this.txtSaldo.Name = "txtSaldo";
            this.txtSaldo.Size = new System.Drawing.Size(100, 20);
            this.txtSaldo.TabIndex = 2;
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(134, 91);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(100, 20);
            this.txtCliente.TabIndex = 1;
            // 
            // txtIdVenta
            // 
            this.txtIdVenta.Location = new System.Drawing.Point(134, 48);
            this.txtIdVenta.Name = "txtIdVenta";
            this.txtIdVenta.Size = new System.Drawing.Size(100, 20);
            this.txtIdVenta.TabIndex = 0;
            // 
            // FrmCuentasPorCobrar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 537);
            this.Controls.Add(this.gbPago);
            this.Controls.Add(this.dgvCxC);
            this.Controls.Add(this.label1);
            this.Name = "FrmCuentasPorCobrar";
            this.Text = "D";
            this.Load += new System.EventHandler(this.FrmCuentasPorCobrar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCxC)).EndInit();
            this.gbPago.ResumeLayout(false);
            this.gbPago.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvCxC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIdVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSaldo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVencimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.GroupBox gbPago;
        private System.Windows.Forms.TextBox txtMontoPago;
        private System.Windows.Forms.TextBox txtSaldo;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.TextBox txtIdVenta;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnPagar;
        private System.Windows.Forms.ComboBox cbMetodoPago;
    }
}