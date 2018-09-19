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
    public partial class VentanaAtributos : Form
    {
        List<char> tiposDato = new List<char>();
        public char[] nombreAtributo = { '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\n' };
        public char tipo_Dato_Atributo = ' ';
        public int longitud_tipo_dato_Atributo;
        public long dirrecion_Atributo;
        public int tipo_indice_Atributo;
        public long direccion_indice_Atributo;
        public long dirrecion_sig_Atributo;

        public VentanaAtributos( char[] nombre,char tipoDato,int longitudTipoDato,int tipoIndice)
        {
            InitializeComponent();
            rellena_lista_tipo();
            nombreAtributo = nombre;
            tipo_Dato_Atributo = tipoDato;
            longitud_tipo_dato_Atributo =longitudTipoDato;
            tipo_indice_Atributo = tipoIndice;
        }

        public void rellena_lista_tipo()

        {
            comboBox1.Text = "I";
            TBlongitud.Text = "4";
            comboBox2.Text = "0";

            char[] tipos = { 'I', 'C'};
            for (int i = 0; i < tipos.Length; i++)
            {
                tiposDato.Add(tipos[i]);
            }
            for (int i = 0; i < tiposDato.Count; i++)
            {
                comboBox1.Items.Add(tiposDato[i]);

            }
            comboBox2.Items.Add("0");
            comboBox2.Items.Add("1");
            comboBox2.Items.Add("2");
            comboBox2.Items.Add("3");
            comboBox2.Items.Add("4");

        }

        private void guardarAtributos_Click(object sender, EventArgs e)
        {
            for (int k = 0; k < TBnombre.Text.Length; k++)
            {
                nombreAtributo[k] = TBnombre.Text[k];
            }

            for (int k = 0; k < comboBox1.Text.Length; k++)
            {
                tipo_Dato_Atributo = comboBox1.Text[k];
            }

            if (tipo_Dato_Atributo.Equals("I") == true)
            {
                TBlongitud.Text = "4";
                //Pedir como bloquear una casilla
                //TBlongitud.
            }
            else
            {
                longitud_tipo_dato_Atributo = Int32.Parse(TBlongitud.Text);
            }
            tipo_indice_Atributo = Int32.Parse(comboBox2.Text);

            TBnombre.Text = "";
            TBlongitud.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";

            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "C")
            {
                TBlongitud.Text = "";
            }

            if (comboBox1.Text == "I")
            {
                TBlongitud.Text = "4";
            }
        }
    }
}
