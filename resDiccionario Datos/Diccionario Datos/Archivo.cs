using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diccionario_De_Datos
{
    public class Archivo
    {

        private SaveFileDialog guardar;
        private OpenFileDialog abrir;
        public long cabecera = -1;
        public long inicioEntidad = 8;
        public string nombreArchivo;
        List<object> data = new List<object>();
        public char[] nombreAtributo = { '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\n' };
        char[] nombre = { '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\n' };
        public List<Entidad> lista;
        public List<Entidad> listaAux;
        char[] nombreAux = { '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\n' };
        readonly int tamAtributo = 63;
        readonly int tamEntidad = 62;
        public int posicionMemoria = 0;
        public int tamDato = 0;

        //const string fileName = "Mario";
        //lista de entidad crear en cadena
        char[] direccionAtributo = { '0', '0', '0', '0', '0', '0', '0', '0'};
        char[] direccionDato = { '0', '0', '0', '0', '0', '0', '0', '0'};
        char[] direccionInicial = { '0', '0', '0', '0', '0', '0', '0', '0'};
        char[] direccionSigEntidad = { '0', '0', '0', '0', '0', '0', '0', '0'};

        public Archivo()
        {
            guardar = new SaveFileDialog();
            abrir = new OpenFileDialog();
            nombreArchivo = "";
            lista = new List<Entidad>();
            listaAux = new List<Entidad>();

        }

        public void crearArchivo(int inicio)
        {
            if (inicio == 0)
            {
                nombreArchivo = Microsoft.VisualBasic.Interaction.InputBox(" Introduce nombre:  ", " Diccionario de datos");
            }

            FileStream stream = new FileStream(nombreArchivo, FileMode.Create, FileAccess.Write, FileShare.None);
            BinaryWriter writer = new BinaryWriter(stream);

            cabecera = inicioEntidad;
            data.Add(cabecera);
            writer.Write(cabecera);

            if (inicio == 0)
            {

                MessageBox.Show(" Archivo nuevo creado ");
                }
        }

        public void guardarArchivo(List<Entidad> listaEntidad)
        {
            int restaDeCadena = 8;
            string nombreaux = "";
            FileStream streamG = new FileStream(nombreArchivo, FileMode.Create);
            BinaryWriter writer = new BinaryWriter(streamG);

            for (int i = 0; i < listaEntidad.Count; i++)
            {
                
                cabecera = inicioEntidad;
                data.Add(cabecera);
                writer.Write(cabecera);


                for (int j = 0; j < listaEntidad.Count; j++)
                {

                    for (int k = 0; k < listaEntidad[j].nombre.Length; k++)
                    {

                        writer.Write(listaEntidad[j].nombre[k]);
                    }

                    //dirreccionAtributo
                    string nombreAux = string.Join("", listaEntidad[j].DA);

                    restaDeCadena = 7 - nombreAux.Length;

                    for (int k = 0; k <= restaDeCadena; k++)
                    {
                        direccionAtributo[k] = '0';
                    }

                    for (int k = 0; k < nombreAux.Length; k++)
                    {
                        direccionAtributo[k + restaDeCadena + 1] = nombreAux[k];
                    }

                    for (int k = 0; k < direccionAtributo.Length; k++)
                    {

                        writer.Write(direccionAtributo[k]);
                    }

                    //dirreccionDato
                    nombreaux = listaEntidad[j].DD.ToString();

                    for (int k = 0; k < nombreaux.Length; k++)
                    {
                        direccionDato[k] = nombreaux[k];
                    }

                    for (int k = 0; k < direccionDato.Length; k++)
                    {

                        writer.Write(direccionDato[k]);
                    }

                    //dirreccion de la entidad
                    nombreaux = listaEntidad[j].DE.ToString();

                    for (int k = 0; k < nombreaux.Length; k++)
                    {
                        direccionInicial[k] = nombreaux[k];
                    }

                    for (int k = 0; k < direccionInicial.Length; k++)
                    {

                        writer.Write(direccionInicial[k]);
                    }

                    //direccion de la sig entidad

                    nombreaux = listaEntidad[j].DSE.ToString();
                    

                    for (int k = 0; k < nombreaux.Length; k++)
                    {
                        direccionSigEntidad[k] = nombreaux[k];
                    }

                    for (int k = 0; k < direccionAtributo.Length; k++)
                    {

                        writer.Write(direccionSigEntidad[k]);
                    }

                    if (listaEntidad[j].DA != -1)
                    {
                        for (int l = 0; l < listaEntidad[j].listaAtributos.Count; l++)
                        {
                            for (int m = 0; m < listaEntidad[j].listaAtributos[l].nombre.Length; m++)
                            {
                                writer.Write(listaEntidad[j].listaAtributos[l].nombre[m]);
                            }

                            //direccionAtributoTipoDato
                            writer.Write(listaEntidad[j].listaAtributos[l].tipoDato.ToString());

                            //direccionAtributoLongitudDato
                            char[] direccionAtri = new char[4];
                            nombreaux = listaEntidad[j].listaAtributos[l].dirrecionAtributo.ToString();

                            for (int k = 0; k < nombreaux.Length; k++)
                            {
                                direccionAtri[k] = nombreaux[k];
                            }

                            for (int k = 0; k < direccionAtri.Length; k++)
                            {

                                writer.Write(direccionAtri[k]);
                            }

                            //direccionAtributoDondeEsta
                            nombreaux = listaEntidad[j].listaAtributos[l].dirrecionAtributo.ToString();
                            direccionAtributo = new char[8];

                            for (int k = 0; k < nombreaux.Length; k++)
                            {
                                direccionAtributo[k] = nombreaux[k];
                            }

                            for (int k = 0; k < direccionAtributo.Length; k++)
                            {

                                writer.Write(direccionAtributo[k]);
                            }

                            //indice

                            writer.Write(listaEntidad[j].listaAtributos[l].tipoIndice.ToString());

                            //dirrecion del indice del atributo
                            nombreaux = listaEntidad[j].listaAtributos[l].dirreccionIndice.ToString();
                            direccionAtributo = new char[8];

                            for (int k = 0; k < nombreaux.Length; k++)
                            {
                                direccionAtributo[k] = nombreaux[k];
                            }

                            for (int k = 0; k < direccionAtributo.Length; k++)
                            {

                                writer.Write(direccionAtributo[k]);
                            }

                            ////dirrecion del siguinete atributo
                            nombreaux = listaEntidad[j].listaAtributos[l].dirrecionSigAtributo.ToString();
                            direccionAtributo = new char[8];

                            for (int k = 0; k < nombreaux.Length; k++)
                            {
                                direccionAtributo[k] = nombreaux[k];
                            }

                            for (int k = 0; k < direccionAtributo.Length; k++)
                            {

                                writer.Write(direccionAtributo[k]);
                            }
                        }

                    }
                    //auxNombre = listaEntidad[j].nombre.ToString;
                    //MessageBox.Show("Entidad" + listaEntidad[j].nombre + "Guardada");

                }

                limpiaVariables();

            }
            streamG.Close();
            writer.Close();
            

            //MessageBox.Show(" Archivo guardado ");
        }

        public void abrirArchivo(List<Entidad> listaEntidad)
        {
            string apuntador =  " ";
            long tamArchivo = 0;
            posicionMemoria = 8;
            tamDato = 8;
            long posicion = 0;
            int posEntidad = 0;
            Entidad nEntidad = new Entidad(nombreAux, -1, -1, -1, -1);
            listaEntidad = new List<Entidad>();

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                FileStream streamR = new FileStream(abrir.FileName, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(streamR);

                nombreArchivo = abrir.FileName;

                Boolean bandCabecera = false;

                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    if (bandCabecera == false)

                    {
                        long header = reader.ReadInt64();
                        data.Add(header);
                        bandCabecera = true;
                        posicion = reader.BaseStream.Position;
                    }

                    //leer en la posicion de memoria
                    reader.BaseStream.Position = posicion;
                    if (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        for (int j = 0; j <= listaEntidad.Count; j++)
                        {
                            for (int k = 0; k < nombreAux.Length; k++)
                            {
                                if (reader.BaseStream.Position >= 7)
                                {
                                    char car = reader.ReadChar();
                                    if (k < 29)
                                    {
                                        nombreAux[k] = car;
                                    }
                                }
                       
                            }


                        for (int k = 0; k <= 7; k++)
                        {
                            char car = reader.ReadChar();
                            direccionAtributo[k] = car;
                                
                        }

                            //apuntador = direccionAtributo.ToString();
                            string result = string.Join("", direccionAtributo);
                            int apAtr32 = Int32.Parse(result);
                        long apAtr = apAtr32;

                        for (int k = 0; k <= 7; k++)
                        {
                            char car = reader.ReadChar();
                            direccionDato[k] = car;
                        }

                        long apDt = Int64.Parse(direccionDato.ToString());

                        for (int k = 0; k <= 7; k++)
                        {
                            char car = reader.ReadChar();
                            direccionInicial[k] = car;
                        }

                        long posIn = Int64.Parse(direccionInicial.ToString());

                        for (int k = 0; k <= 7; k++)
                        {
                            char car = reader.ReadChar();
                            direccionSigEntidad[k] = car;
                        }

                        long apSigE = Int64.Parse(direccionSigEntidad.ToString());

                        posicion = apSigE;

                        nEntidad = new Entidad(nombreAux, apAtr, apDt, posIn, apSigE);
                                
                        if (nEntidad.DA != -1)
                        {

                            for (int l = 0; l < listaEntidad[j].listaAtributos.Count; l++)
                            {
                                lee_atributos_de_entidad(streamR, reader, listaEntidad[j]);
                                        
                            }

                        }
                            //auxNombre = listaEntidad[j].nombre.ToString;
                            //MessageBox.Show("Entidad" + listaEntidad[j].nombre + "Guardada");
                               
                            
                    }
                        lista.Add(nEntidad);
                }

                    limpiaVariables();
                    tamArchivo = reader.BaseStream.Position;
                    posEntidad = posEntidad + 1;
                }

                guardarArchivo(lista);
                streamR.Close();
                reader.Close();

            }

            
        }

        void lee_atributos_de_entidad(FileStream f, BinaryReader r, Entidad ent)
        {
            Atributo nAtributo = new Atributo(nombreAtributo, ' ', -1, -1, -1, -1, -1);

            //Leer nombre de atributo
            for (int i = 0; i < nombreAtributo.Length; i++)
            {

                char car = r.ReadChar();
                nombreAtributo[i] = car;

            }

            char type = r.ReadChar();
            int lon = r.ReadInt32();
            long posAt = r.ReadInt64();
            int index = r.ReadInt32();
            long apIndx = r.ReadInt64();
            long apSigAt = r.ReadInt64();

            nAtributo = new Atributo(nombreAtributo, type, lon, posAt, index, apIndx, apSigAt);
            posicionMemoria = posicionMemoria + tamAtributo;

            ent.listaAtributos.Add(nAtributo);

            if (apSigAt != -2 && apSigAt != -4)
            {
                if (nAtributo.tipoDato == 'S')
                {
                    long bytes = nAtributo.longitudDato / 2;
                    tamDato += Convert.ToInt32(bytes);
                }
                else
                {
                    tamDato += Convert.ToInt32(nAtributo.longitudDato);
                }
            }

            nombreAtributo = new char[30];
            nombreAtributo[29] = '\n';

            if (apSigAt != -1 && apSigAt != -4)
            {
                lee_atributos_de_entidad(f, r, ent);
            }
            // 77777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777
            /*
            for (int i = 0; i == listaEntidad[posEntidad].nombre.Length; i++)
            {
                char car = reader.ReadChar();
                listaEntidad[posEntidad].nombre[i] = car;

            }

            long apAtr = reader.ReadInt64();
            long apDt = reader.ReadInt64();
            long posIn = reader.ReadInt64();
            long apSigE = reader.ReadInt64();

            Entidad nEntidad = new Entidad(listaEntidad[posEntidad].nombre, apAtr, apDt, posIn, apSigE);

            posicionMemoria = posicionMemoria + tamEntidad;
            //Verificar si la entidad tiene atributos

            if (nEntidad.DA != -1)
            {
                tamDato = 16;

                //lee_atributos_de_entidad(streamR, reader, nEntidad);
            }

            */


        }

        public void limpiaVariables()
        {
            for (int k = 0; k <= 7; k++)
            {
                direccionAtributo[k] = '0';
            }

            for (int k = 0; k <= 7; k++)
            {
                direccionDato[k] = '0';
            }

            for (int k = 0; k <= 7; k++)
            {
                direccionInicial[k] = '0';
            }

            for (int k = 0; k <= 7; k++)
            {
                direccionSigEntidad[k] = '0';
            }
        }

        public void eliminaTodo()
        {
            crearArchivo(1);
            
        }
    }
}
