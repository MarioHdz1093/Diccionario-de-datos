namespace Diccionario_Datos
{
    partial class VentanaAtributosEx
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
            this.Entidad = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.BAceptar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Entidad
            // 
            this.Entidad.AutoSize = true;
            this.Entidad.Location = new System.Drawing.Point(12, 98);
            this.Entidad.Name = "Entidad";
            this.Entidad.Size = new System.Drawing.Size(110, 13);
            this.Entidad.TabIndex = 2;
            this.Entidad.Text = "Selecciona Atributos: ";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(128, 95);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // BAceptar
            // 
            this.BAceptar.Location = new System.Drawing.Point(98, 200);
            this.BAceptar.Name = "BAceptar";
            this.BAceptar.Size = new System.Drawing.Size(75, 23);
            this.BAceptar.TabIndex = 4;
            this.BAceptar.Text = "Aceptar";
            this.BAceptar.UseVisualStyleBackColor = true;
            this.BAceptar.Click += new System.EventHandler(this.BAceptar_Click_1);
            // 
            // VentanaAtributosEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.BAceptar);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.Entidad);
            this.Name = "VentanaAtributosEx";
            this.Text = "VentanaAtributosEx";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Entidad;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button BAceptar;
    }
}