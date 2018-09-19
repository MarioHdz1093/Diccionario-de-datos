namespace Diccionario_Datos
{
    partial class VentanaEntidadesEx
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Entidad = new System.Windows.Forms.Label();
            this.BAceptar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(121, 102);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(138, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // Entidad
            // 
            this.Entidad.AutoSize = true;
            this.Entidad.Location = new System.Drawing.Point(12, 105);
            this.Entidad.Name = "Entidad";
            this.Entidad.Size = new System.Drawing.Size(105, 13);
            this.Entidad.TabIndex = 1;
            this.Entidad.Text = "Selecciona Entidad: ";
            // 
            // BAceptar
            // 
            this.BAceptar.Location = new System.Drawing.Point(62, 204);
            this.BAceptar.Name = "BAceptar";
            this.BAceptar.Size = new System.Drawing.Size(134, 23);
            this.BAceptar.TabIndex = 2;
            this.BAceptar.Text = "Aceptar";
            this.BAceptar.UseVisualStyleBackColor = true;
            this.BAceptar.Click += new System.EventHandler(this.BAceptar_Click);
            // 
            // VentanaEntidadesEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.BAceptar);
            this.Controls.Add(this.Entidad);
            this.Controls.Add(this.comboBox1);
            this.Name = "VentanaEntidadesEx";
            this.Text = "VentanaEntidadesEx";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label Entidad;
        private System.Windows.Forms.Button BAceptar;
    }
}