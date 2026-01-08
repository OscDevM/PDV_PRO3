namespace PDV_PRO3
{
    partial class MenuPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuPrincipal));
            this.PanelVertical = new System.Windows.Forms.Panel();
            this.panelRegistrosSubmenu1 = new System.Windows.Forms.Panel();
            this.BtnSalir = new System.Windows.Forms.Button();
            this.BtnAcercade = new System.Windows.Forms.Button();
            this.BtnProductos = new System.Windows.Forms.Button();
            this.BtnUsuario = new System.Windows.Forms.Button();
            this.BtnClientes = new System.Windows.Forms.Button();
            this.BtnConsultarFactura = new System.Windows.Forms.Button();
            this.BtnFacturación = new System.Windows.Forms.Button();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.LblMenuPrincipal = new System.Windows.Forms.Label();
            this.PanelPrincipal = new System.Windows.Forms.Panel();
            this.PanelVertical.SuspendLayout();
            this.panelRegistrosSubmenu1.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelVertical
            // 
            this.PanelVertical.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(42)))));
            this.PanelVertical.Controls.Add(this.panelRegistrosSubmenu1);
            this.PanelVertical.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelVertical.ForeColor = System.Drawing.SystemColors.ControlText;
            this.PanelVertical.Location = new System.Drawing.Point(0, 82);
            this.PanelVertical.Name = "PanelVertical";
            this.PanelVertical.Size = new System.Drawing.Size(174, 453);
            this.PanelVertical.TabIndex = 10;
            // 
            // panelRegistrosSubmenu1
            // 
            this.panelRegistrosSubmenu1.AutoSize = true;
            this.panelRegistrosSubmenu1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(117)))), ((int)(((byte)(255)))));
            this.panelRegistrosSubmenu1.Controls.Add(this.BtnSalir);
            this.panelRegistrosSubmenu1.Controls.Add(this.BtnAcercade);
            this.panelRegistrosSubmenu1.Controls.Add(this.BtnProductos);
            this.panelRegistrosSubmenu1.Controls.Add(this.BtnUsuario);
            this.panelRegistrosSubmenu1.Controls.Add(this.BtnClientes);
            this.panelRegistrosSubmenu1.Controls.Add(this.BtnConsultarFactura);
            this.panelRegistrosSubmenu1.Controls.Add(this.BtnFacturación);
            this.panelRegistrosSubmenu1.Location = new System.Drawing.Point(0, 42);
            this.panelRegistrosSubmenu1.Name = "panelRegistrosSubmenu1";
            this.panelRegistrosSubmenu1.Size = new System.Drawing.Size(174, 343);
            this.panelRegistrosSubmenu1.TabIndex = 6;
            // 
            // BtnSalir
            // 
            this.BtnSalir.BackColor = System.Drawing.Color.DarkOrchid;
            this.BtnSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSalir.Cursor = System.Windows.Forms.Cursors.Default;
            this.BtnSalir.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnSalir.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.BtnSalir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Beige;
            this.BtnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSalir.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnSalir.Location = new System.Drawing.Point(0, 294);
            this.BtnSalir.Name = "BtnSalir";
            this.BtnSalir.Size = new System.Drawing.Size(174, 49);
            this.BtnSalir.TabIndex = 4;
            this.BtnSalir.Text = "Salir";
            this.BtnSalir.UseVisualStyleBackColor = false;
            this.BtnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
            // 
            // BtnAcercade
            // 
            this.BtnAcercade.BackColor = System.Drawing.Color.DarkOrchid;
            this.BtnAcercade.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnAcercade.Cursor = System.Windows.Forms.Cursors.Default;
            this.BtnAcercade.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnAcercade.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.BtnAcercade.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Beige;
            this.BtnAcercade.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAcercade.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnAcercade.Location = new System.Drawing.Point(0, 245);
            this.BtnAcercade.Name = "BtnAcercade";
            this.BtnAcercade.Size = new System.Drawing.Size(174, 49);
            this.BtnAcercade.TabIndex = 5;
            this.BtnAcercade.Text = "Acerca de";
            this.BtnAcercade.UseVisualStyleBackColor = false;
            this.BtnAcercade.Click += new System.EventHandler(this.BtnAcercade_Click);
            // 
            // BtnProductos
            // 
            this.BtnProductos.BackColor = System.Drawing.Color.DarkOrchid;
            this.BtnProductos.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnProductos.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.BtnProductos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnProductos.ForeColor = System.Drawing.Color.White;
            this.BtnProductos.Location = new System.Drawing.Point(0, 196);
            this.BtnProductos.Name = "BtnProductos";
            this.BtnProductos.Size = new System.Drawing.Size(174, 49);
            this.BtnProductos.TabIndex = 6;
            this.BtnProductos.Text = "Productos";
            this.BtnProductos.UseVisualStyleBackColor = false;
            this.BtnProductos.Click += new System.EventHandler(this.button1_Click);
            // 
            // BtnUsuario
            // 
            this.BtnUsuario.BackColor = System.Drawing.Color.DarkOrchid;
            this.BtnUsuario.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnUsuario.Cursor = System.Windows.Forms.Cursors.Default;
            this.BtnUsuario.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnUsuario.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.BtnUsuario.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Beige;
            this.BtnUsuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnUsuario.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnUsuario.Location = new System.Drawing.Point(0, 147);
            this.BtnUsuario.Name = "BtnUsuario";
            this.BtnUsuario.Size = new System.Drawing.Size(174, 49);
            this.BtnUsuario.TabIndex = 3;
            this.BtnUsuario.Text = "Usuarios";
            this.BtnUsuario.UseVisualStyleBackColor = false;
            this.BtnUsuario.Click += new System.EventHandler(this.BtnUsuario_Click);
            // 
            // BtnClientes
            // 
            this.BtnClientes.BackColor = System.Drawing.Color.DarkOrchid;
            this.BtnClientes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnClientes.Cursor = System.Windows.Forms.Cursors.Default;
            this.BtnClientes.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnClientes.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.BtnClientes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Beige;
            this.BtnClientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnClientes.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnClientes.Location = new System.Drawing.Point(0, 98);
            this.BtnClientes.Name = "BtnClientes";
            this.BtnClientes.Size = new System.Drawing.Size(174, 49);
            this.BtnClientes.TabIndex = 2;
            this.BtnClientes.Text = "Clientes";
            this.BtnClientes.UseVisualStyleBackColor = false;
            this.BtnClientes.Click += new System.EventHandler(this.BtnClientes_Click);
            // 
            // BtnConsultarFactura
            // 
            this.BtnConsultarFactura.BackColor = System.Drawing.Color.DarkOrchid;
            this.BtnConsultarFactura.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnConsultarFactura.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.BtnConsultarFactura.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnConsultarFactura.ForeColor = System.Drawing.Color.White;
            this.BtnConsultarFactura.Location = new System.Drawing.Point(0, 49);
            this.BtnConsultarFactura.Name = "BtnConsultarFactura";
            this.BtnConsultarFactura.Size = new System.Drawing.Size(174, 49);
            this.BtnConsultarFactura.TabIndex = 7;
            this.BtnConsultarFactura.Text = "Consultar Factura";
            this.BtnConsultarFactura.UseVisualStyleBackColor = false;
            // 
            // BtnFacturación
            // 
            this.BtnFacturación.BackColor = System.Drawing.Color.DarkOrchid;
            this.BtnFacturación.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnFacturación.Cursor = System.Windows.Forms.Cursors.Default;
            this.BtnFacturación.Dock = System.Windows.Forms.DockStyle.Top;
            this.BtnFacturación.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.BtnFacturación.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Beige;
            this.BtnFacturación.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnFacturación.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnFacturación.Location = new System.Drawing.Point(0, 0);
            this.BtnFacturación.Name = "BtnFacturación";
            this.BtnFacturación.Size = new System.Drawing.Size(174, 49);
            this.BtnFacturación.TabIndex = 0;
            this.BtnFacturación.Text = "Facturación";
            this.BtnFacturación.UseVisualStyleBackColor = false;
            this.BtnFacturación.Click += new System.EventHandler(this.BtnFacturación_Click);
            // 
            // panelContenido
            // 
            this.panelContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(42)))));
            this.panelContenido.Controls.Add(this.LblMenuPrincipal);
            this.panelContenido.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelContenido.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelContenido.Location = new System.Drawing.Point(0, 0);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(1022, 82);
            this.panelContenido.TabIndex = 11;
            // 
            // LblMenuPrincipal
            // 
            this.LblMenuPrincipal.AutoSize = true;
            this.LblMenuPrincipal.Font = new System.Drawing.Font("MV Boli", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMenuPrincipal.ForeColor = System.Drawing.Color.White;
            this.LblMenuPrincipal.Location = new System.Drawing.Point(21, 31);
            this.LblMenuPrincipal.Name = "LblMenuPrincipal";
            this.LblMenuPrincipal.Size = new System.Drawing.Size(136, 21);
            this.LblMenuPrincipal.TabIndex = 0;
            this.LblMenuPrincipal.Text = "Menu Principal";
            // 
            // PanelPrincipal
            // 
            this.PanelPrincipal.Location = new System.Drawing.Point(194, 97);
            this.PanelPrincipal.Name = "PanelPrincipal";
            this.PanelPrincipal.Size = new System.Drawing.Size(816, 426);
            this.PanelPrincipal.TabIndex = 12;
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1022, 535);
            this.Controls.Add(this.PanelPrincipal);
            this.Controls.Add(this.PanelVertical);
            this.Controls.Add(this.panelContenido);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MenuPrincipal";
            this.Text = " Menu";
            this.PanelVertical.ResumeLayout(false);
            this.PanelVertical.PerformLayout();
            this.panelRegistrosSubmenu1.ResumeLayout(false);
            this.panelContenido.ResumeLayout(false);
            this.panelContenido.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelVertical;
        private System.Windows.Forms.Panel panelRegistrosSubmenu1;
        private System.Windows.Forms.Button BtnSalir;
        private System.Windows.Forms.Button BtnAcercade;
        private System.Windows.Forms.Button BtnUsuario;
        private System.Windows.Forms.Button BtnClientes;
        private System.Windows.Forms.Button BtnFacturación;
        private System.Windows.Forms.Panel panelContenido;
        private System.Windows.Forms.Label LblMenuPrincipal;
        private System.Windows.Forms.Button BtnProductos;
        private System.Windows.Forms.Panel PanelPrincipal;
        private System.Windows.Forms.Button BtnConsultarFactura;
    }
}

