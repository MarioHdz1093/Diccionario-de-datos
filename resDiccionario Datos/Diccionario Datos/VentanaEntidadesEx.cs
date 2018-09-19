using Diccionario_De_Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Diccionario_Datos
{
    public partial class VentanaEntidadesEx : Form
    {
        public List<Entidad> listaEnt;
        public String nombre = "";
        public char[] nombreEntidadSeleccionada = { '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\n' };


        public VentanaEntidadesEx(List<Entidad> listaEntidad)
        {
            listaEnt = listaEntidad;
            InitializeComponent();
            rellenaLista();
            
        }


        public void rellenaLista()

        {
            foreach (Entidad e in listaEnt)
            {
                
                for (int k = 0; k < e.nombre.Length; k++)
                {
                    nombre += e.nombre[k].ToString();
                }
                //nombre = e.nombre.ToString();
                comboBox1.Items.Add(nombre);

                nombre = "";
            }
        }

        private void BAceptar_Click(object sender, EventArgs e)
        {
            for (int k = 0; k < comboBox1.Text.Length; k++)
            {
                nombreEntidadSeleccionada[k] = comboBox1.Text[k];

                
            }

            nombre = comboBox1.Text;
            Close();
        }

    }
}
