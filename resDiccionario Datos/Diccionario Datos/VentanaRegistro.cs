using Diccionario_De_Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Diccionario_Datos
{
    public partial class VentanaRegistro : Form
    {
        public Entidad muestraEntidad;
        public VentanaRegistro(Entidad entidadEntrante)
        {
            InitializeComponent();
            muestraEntidad = entidadEntrante;
            manejo_dataGrid();
            guardarArchivo();
        }

        public void manejo_dataGrid()
        {
            //posicionMemoria = 8;
            //tamDato = 8;

            //Refrescar el dataGribView
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            dataGridView1.ColumnCount = muestraEntidad.listaAtributos.Count;
            dataGridView1.ColumnHeadersVisible = true;

            for(int i=0; i <muestraEntidad.listaAtributos.Count;i++)
            {
                string s = new string(muestraEntidad.listaAtributos[i].nombre);
                dataGridView1.Columns[i].Name = s;
            }
        }

        private string cambiaToString(char[] cambia)
        {
            int y = 0;
            for (int i = 0; i < cambia.Length; i++)
            {
                if (cambia[i] == '\0')
                {
                    y = i;
                    break;
                }
            }

            char[] nuevo = new char[y];

            for (int i = 0; i < y; i++)
            {
                nuevo[i] = cambia[i];
            }

            string nombreaux = new string(nuevo);

            return nombreaux;
        }

        public void guardarArchivo()
        {
            int restaDeCadena = 8;
            string nombreaux = cambiaToString(muestraEntidad.nombre);

            //string result = System.Text.Encoding.UTF8.GetString(muestraEntidad.nombre);

            //nombreaux = nombreaux + ".index";

            FileStream streamR = new FileStream(nombreaux + ".index", FileMode.Create, FileAccess.Write);
            BinaryWriter writer = new BinaryWriter(streamR);

            streamR.Close();
            writer.Close();
        }
        }
}
