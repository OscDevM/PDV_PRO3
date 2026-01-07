namespace PDV_PRO3
{
    partial class FrmPagoCxC
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
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblFactura = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblPagado = new System.Windows.Forms.Label();
            this.lblPendiente = new System.Windows.Forms.Label();
            this.btnAgregarPago = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(30, 58);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(49, 13);
            this.lblCliente.TabIndex = 0;
            this.lblCliente.Text = "lblCliente";
            // 
            // lblFactura
            // 
            this.lblFactura.AutoSize = true;
            this.lblFactura.Location = new System.Drawing.Point(117, 58);
            this.lblFactura.Name = "lblFactura";
            this.lblFactura.Size = new System.Drawing.Size(53, 13);
            this.lblFactura.TabIndex = 1;
            this.lblFactura.Text = "lblFactura";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(234, 58);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(41, 13);
            this.lblTotal.TabIndex = 2;
            this.lblTotal.Text = "lblTotal";
            // 
            // lblPagado
            // 
            this.lblPagado.AutoSize = true;
            this.lblPagado.Location = new System.Drawing.Point(332, 58);
            this.lblPagado.Name = "lblPagado";
            this.lblPagado.Size = new System.Drawing.Size(54, 13);
            this.lblPagado.TabIndex = 3;
            this.lblPagado.Text = "lblPagado";
            // 
            // lblPendiente
            // 
            this.lblPendiente.AutoSize = true;
            this.lblPendiente.Location = new System.Drawing.Point(478, 58);
            this.lblPendiente.Name = "lblPendiente";
            this.lblPendiente.Size = new System.Drawing.Size(65, 13);
            this.lblPendiente.TabIndex = 4;
            this.lblPendiente.Text = "lblPendiente";
            // 
            // btnAgregarPago
            // 
            this.btnAgregarPago.Location = new System.Drawing.Point(104, 118);
            this.btnAgregarPago.Name = "btnAgregarPago";
            this.btnAgregarPago.Size = new System.Drawing.Size(109, 23);
            this.btnAgregarPago.TabIndex = 5;
            this.btnAgregarPago.Text = "Agregar Pago";
            this.btnAgregarPago.UseVisualStyleBackColor = true;
            this.btnAgregarPago.Click += new System.EventHandler(this.btnAgregarPago_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(434, 118);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(109, 23);
            this.btnCerrar.TabIndex = 6;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            // 
            // FrmPagoCxC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 207);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnAgregarPago);
            this.Controls.Add(this.lblPendiente);
            this.Controls.Add(this.lblPagado);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblFactura);
            this.Controls.Add(this.lblCliente);
            this.Name = "FrmPagoCxC";
            this.Text = "FrmPagoCxC";
            this.Load += new System.EventHandler(this.FrmPagoCxC_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblFactura;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblPagado;
        private System.Windows.Forms.Label lblPendiente;
        private System.Windows.Forms.Button btnAgregarPago;
        private System.Windows.Forms.Button btnCerrar;
    }
}