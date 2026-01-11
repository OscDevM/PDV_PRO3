namespace PDV_PRO3
{
    partial class crud_inventario
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.TxtLugar = new System.Windows.Forms.TextBox();
            this.TxtTramo = new System.Windows.Forms.TextBox();
            this.LblLugar = new System.Windows.Forms.Label();
            this.LblTramo = new System.Windows.Forms.Label();
            this.BtnAgregar = new System.Windows.Forms.Button();
            this.BtnEditar = new System.Windows.Forms.Button();
            this.BtnEliminar = new System.Windows.Forms.Button();
            this.DgvInventario = new System.Windows.Forms.DataGridView();
            this.BtnLimpiar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DgvInventario)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtLugar
            // 
            this.TxtLugar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TxtLugar.Location = new System.Drawing.Point(380, 38);
            this.TxtLugar.Name = "TxtLugar";
            this.TxtLugar.Size = new System.Drawing.Size(183, 25);
            this.TxtLugar.TabIndex = 0;
            // 
            // TxtTramo
            // 
            this.TxtTramo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.TxtTramo.Location = new System.Drawing.Point(90, 41);
            this.TxtTramo.Name = "TxtTramo";
            this.TxtTramo.Size = new System.Drawing.Size(162, 25);
            this.TxtTramo.TabIndex = 1;
            // 
            // LblLugar
            // 
            this.LblLugar.AutoSize = true;
            this.LblLugar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblLugar.Location = new System.Drawing.Point(320, 41);
            this.LblLugar.Name = "LblLugar";
            this.LblLugar.Size = new System.Drawing.Size(44, 19);
            this.LblLugar.TabIndex = 2;
            this.LblLugar.Text = "Lugar";
            // 
            // LblTramo
            // 
            this.LblTramo.AutoSize = true;
            this.LblTramo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.LblTramo.Location = new System.Drawing.Point(25, 44);
            this.LblTramo.Name = "LblTramo";
            this.LblTramo.Size = new System.Drawing.Size(47, 19);
            this.LblTramo.TabIndex = 3;
            this.LblTramo.Text = "Tramo";
            // 
            // BtnAgregar
            // 
            this.BtnAgregar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.BtnAgregar.Location = new System.Drawing.Point(23, 261);
            this.BtnAgregar.Name = "BtnAgregar";
            this.BtnAgregar.Size = new System.Drawing.Size(100, 30);
            this.BtnAgregar.TabIndex = 4;
            this.BtnAgregar.Text = "Agregar";
            this.BtnAgregar.UseVisualStyleBackColor = true;
            this.BtnAgregar.Click += new System.EventHandler(this.BtnAgregar_Click);
            // 
            // BtnEditar
            // 
            this.BtnEditar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.BtnEditar.Location = new System.Drawing.Point(152, 261);
            this.BtnEditar.Name = "BtnEditar";
            this.BtnEditar.Size = new System.Drawing.Size(100, 30);
            this.BtnEditar.TabIndex = 5;
            this.BtnEditar.Text = "Editar";
            this.BtnEditar.UseVisualStyleBackColor = true;
            this.BtnEditar.Click += new System.EventHandler(this.BtnEditar_Click);
            // 
            // BtnEliminar
            // 
            this.BtnEliminar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.BtnEliminar.Location = new System.Drawing.Point(292, 261);
            this.BtnEliminar.Name = "BtnEliminar";
            this.BtnEliminar.Size = new System.Drawing.Size(100, 30);
            this.BtnEliminar.TabIndex = 6;
            this.BtnEliminar.Text = "Eliminar";
            this.BtnEliminar.UseVisualStyleBackColor = true;
            this.BtnEliminar.Click += new System.EventHandler(this.BtnEliminar_Click);
            // 
            // DgvInventario
            // 
            this.DgvInventario.AllowUserToAddRows = false;
            this.DgvInventario.AllowUserToDeleteRows = false;
            this.DgvInventario.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvInventario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvInventario.Location = new System.Drawing.Point(23, 81);
            this.DgvInventario.MultiSelect = false;
            this.DgvInventario.Name = "DgvInventario";
            this.DgvInventario.ReadOnly = true;
            this.DgvInventario.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvInventario.Size = new System.Drawing.Size(540, 159);
            this.DgvInventario.TabIndex = 7;
            this.DgvInventario.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvInventario_CellClick);
            // 
            // BtnLimpiar
            // 
            this.BtnLimpiar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.BtnLimpiar.Location = new System.Drawing.Point(432, 261);
            this.BtnLimpiar.Name = "BtnLimpiar";
            this.BtnLimpiar.Size = new System.Drawing.Size(100, 30);
            this.BtnLimpiar.TabIndex = 8;
            this.BtnLimpiar.Text = "Limpiar";
            this.BtnLimpiar.UseVisualStyleBackColor = true;
            this.BtnLimpiar.Click += new System.EventHandler(this.BtnLimpiar_Click);
            // 
            // crud_inventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 301);
            this.Controls.Add(this.BtnLimpiar);
            this.Controls.Add(this.DgvInventario);
            this.Controls.Add(this.BtnEliminar);
            this.Controls.Add(this.BtnEditar);
            this.Controls.Add(this.BtnAgregar);
            this.Controls.Add(this.LblTramo);
            this.Controls.Add(this.LblLugar);
            this.Controls.Add(this.TxtTramo);
            this.Controls.Add(this.TxtLugar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "crud_inventario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Inventario";
            this.Load += new System.EventHandler(this.crud_inventario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvInventario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtLugar;
        private System.Windows.Forms.TextBox TxtTramo;
        private System.Windows.Forms.Label LblLugar;
        private System.Windows.Forms.Label LblTramo;
        private System.Windows.Forms.Button BtnAgregar;
        private System.Windows.Forms.Button BtnEditar;
        private System.Windows.Forms.Button BtnEliminar;
        private System.Windows.Forms.DataGridView DgvInventario;
        private System.Windows.Forms.Button BtnLimpiar;
    }
}
