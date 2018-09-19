using Diccionario_Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diccionario_De_Datos
{
    public partial class Form1 : Form
    {
        //herramientas basicas
        public int direccion;
        public int cabecera;
        public bool band1;
        public bool band2;
        public bool band3;
        public bool band4;
        public string elNombre = "";
        public Atributo atri;
        public Atributo aux;
        public Entidad ee;
        Entidad llenaEntidadAux;
        public Entidad eAux;
        public Archivo f;
        private Graphics g;
        private SolidBrush b;
        private SolidBrush b2;
        private Font ff;
        private Point pf;
        List<object> data = new List<object>();
        public char[] nombreAtributo2 = { '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\n' };
        public char[] nombreEntidad2 = { '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\n' };
        public char[] limpiaNombre = { '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\n' };
        readonly int tamAtributo = 63;
        readonly int tamEntidad = 62;
        long posicionMemoria = 8;
        int tamDato = 8;

        //parte atributo
        public char[] nombreAtributo = { '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\n' };
        public char tipo_Dato_Atributo = ' ';
        public int longitud_tipo_dato_Atributo;
        public long dirrecion_Atributo;
        public int tipo_indice_Atributo;
        public long direccion_indice_Atributo;
        public long dirrecion_sig_Atributo;


        //parte entidad
        public char[] nombreEntidad = { '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\n' };
        public long direccionEntidad;
        public long direccionEntAtributo;
        public long direccionSigEntidad;
        public long direccionEntidadDatos;

        //listas principales
        public List<Entidad> listaEntidad;
        public List<Entidad> entidadesLeidas = new List<Entidad>();
        public List<Entidad> listaEntAux;

        public Form1()
        {
            InitializeComponent();

            ee = new Entidad(nombreEntidad, direccionEntidad, direccionEntAtributo, direccionEntidadDatos, direccionSigEntidad);

            //entidad
            direccionEntidad = 0;
            direccionEntAtributo = 0;
            direccionSigEntidad = 0;
            direccionEntidadDatos = 0;


            //atributos

            longitud_tipo_dato_Atributo = 0;
            dirrecion_Atributo = 0;
            tipo_indice_Atributo = 0;
            direccion_indice_Atributo = 0;
            dirrecion_sig_Atributo = 0;

            listaEntidad = new List<Entidad>();
            listaEntAux = new List<Entidad>();
            llenaEntidadAux = new Entidad(nombreEntidad, 0, 0, 0, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            direccion = 8;
            band1 = false;
            band2 = false;
            band3 = false;
            band4 = false;
            g = CreateGraphics();
            b = new SolidBrush(Color.Gray); //brocha atributo 
            b2 = new SolidBrush(Color.Blue);  //brocha entidad
            ff = new Font("The ninho", 14, FontStyle.Bold);
            //nombreEntidad = new string(' ', 1);

            //ee = new Entidad(nombreEntidad,direccionEntidad,direccionEntAtributo,direccionEntidadDatos,direccionSigEntidad);
            pf = new Point(50, 70);
            this.KeyDown += new KeyEventHandler(this.frmBase_KeyDown);
            dataGridView1.ReadOnly = false;
            dataGridView2.ReadOnly = false;
        }

        private void archivo(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.AccessibleName)
            {
                case "nuevo":
                    archivoToolStripMenuItem.HideDropDown();
                    f = new Archivo();
                    f.crearArchivo(0);
                    this.activarMenus();
                    break;

                case "guardar":
                    archivoToolStripMenuItem.HideDropDown();
                    f.guardarArchivo(listaEntidad);
                    break;

                case "abrir":
                    archivoToolStripMenuItem.HideDropDown();
                    f = new Archivo();
                    f.abrirArchivo(listaEntAux);
                    listaEntidad = f.lista;
                    //inertarEntidad(0);
                    this.activarMenus();
                    band4 = true;
                    break;
            }

        }

        private void agregar(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.AccessibleName)
            {
                case "entidad":
                    inertarEntidad();
                    break;
                case "atributo":
                    insertarAtributo();
                    break;

            }
        }

        private void eliminar(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.AccessibleName)
            {
                case "entidad":
                    this.eliminaEntidad();
                    break;

                case "atributo":
                    this.eliminarAtributo();
                    break;

                case "todo":
                    this.eliminarTodo();
                    break;
            }

        }

        private void modificar(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.AccessibleName)
            {
                case "atributo":
                    this.modificarAtributo();
                    break;

                case "entidad":
                    this.modificarEntidad();
                    break;
            }
        }

        private void mostrar(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.AccessibleName)
            {
                case "atributo":
                    this.mostrarAtributo();
                    break;
                case "entidad":
                    this.mostrarEntidad();
                    break;

                case "todo":
                    band4 = true;
                    Form1_Paint(this, null);
                    break;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {


            manejo_dataGrid();

            //manejo_dataGrid_atributos(entidadEncontrada);


        }

        //Entidad
        private void btnClickInputBox_Click(object sender, EventArgs e)
        {
            nombreEntidad = new char[30];
            nombreEntidad[29] = '\n';
            string nombreaux = "";
            nombreaux = Microsoft.VisualBasic.Interaction.InputBox(" Introduce nombre de entidad. ", "Diccionario De Datos. ", "");
            for (int k = 0; k < nombreaux.Length; k++)
            {
                nombreEntidad[k] = nombreaux[k];
            }


        }

        //Atributo
        private void btnClickInputBox_Click2(object sender, EventArgs e)
        {
            nombreAtributo = new char[30];
            nombreAtributo[29] = '\n';
            VentanaAtributos v = new VentanaAtributos(nombreAtributo, tipo_Dato_Atributo, longitud_tipo_dato_Atributo, tipo_indice_Atributo);
            v.ShowDialog();
            //listaEntidad = v.listaEntidad;

            nombreAtributo = v.nombreAtributo;
            tipo_Dato_Atributo = v.tipo_Dato_Atributo;
            longitud_tipo_dato_Atributo = v.longitud_tipo_dato_Atributo;
            tipo_indice_Atributo = v.tipo_indice_Atributo;



        }

        //Nuevo Atributo
        private void btnClickInputBox_Click3(object sender, EventArgs e)
        {
            nombreAtributo2 = new char[30];
            nombreAtributo2[29] = '\n';
            //nombreAtributo2 = Microsoft.VisualBasic.Interaction.InputBox(" Introduce nuevo nombre de atributo ", "Diccionario De Datos. ", "");
            string nombreaux = "";
            nombreaux = Microsoft.VisualBasic.Interaction.InputBox(" Introduce nombre de atributo ", "Diccionario De Datos. ", "");

            for (int k = 0; k < nombreaux.Length; k++)
            {
                nombreAtributo2[k] = nombreaux[k];
            }
        }

        //Entidad
        private void btnClickInputBox_Click4(object sender, EventArgs e)
        {
            nombreEntidad2 = new char[30];
            nombreEntidad2[29] = '\n';
            //nombreEntidad2 = Microsoft.VisualBasic.Interaction.InputBox(" Introduce nuevo nombre de entidad ", "Diccionario De Datos ", "");
            string nombreaux = "";
            nombreaux = Microsoft.VisualBasic.Interaction.InputBox(" Introduce nombre de entidad. ", "Diccionario De Datos. ", "");
            for (int k = 0; k < nombreaux.Length; k++)
            {
                nombreEntidad2[k] = nombreaux[k];
            }
        }

        public void inertarEntidad()
        {
            //Nombre de la Entidad
            
            band1 = false;
            nombreEntidad = new char[30];
            nombreEntidad[29] = '\n';
            this.btnClickInputBox_Click(this, null);


            foreach (Entidad e in listaEntidad)
            {
                if (compara(e.nombre,nombreEntidad) == true)
                {
                    band1 = true;

                }
                else
                {
                    band1 = false;
                }
            }

            if (band1 == false)
            {
                llenaEntidadAux = new Entidad(nombreEntidad, direccionEntidad, direccionEntAtributo, direccionEntidadDatos, direccionSigEntidad);

                llenaEntidadAux.DE = posicionMemoria;
                posicionMemoria = posicionMemoria + tamEntidad;

                listaEntidad.Add(llenaEntidadAux);
                acomodaApuntador(nombreEntidad);
                f.guardarArchivo(listaEntidad);
                MessageBox.Show(" Entidad agregada ");

            }

            if (band1 == true)
            {
                MessageBox.Show(" Entidad ya existente en lista, Inserte otra ");
                band1 = false;
            }

            manejo_dataGrid();
            

            nombreEntidad = new char[30];
            nombreEntidad[29] = '\n';

        }

        private void eliminaEntidad()
        {
            int posicion = 0;
            //Nombre de la entidad
            VentanaEntidadesEx v = new VentanaEntidadesEx(listaEntidad);
            v.ShowDialog();

            for (int k = 0; k < v.nombre.Length; k++)
            {
                nombreEntidad[k] = v.nombre[k];

            }

            for (int i = 0; i < listaEntidad.Count; i++)
            {
                if (compara(listaEntidad[i].nombre, nombreEntidad))
                {
                    posicion = i;
                }
            }


            foreach (Entidad e in listaEntidad)
            {
                if (compara(nombreEntidad, e.nombre))
                {
                    e.DA = 0;
                    e.DD = 0;
                    e.DE = 0;
                    e.DSE = 0;
                    e.listaAtributos = new List<Atributo>();
                    band1 = false;
                    break;
                }
                else
                {
                    band1 = true;
                }
            }
            if (band1 == false)
            {
                //listaEntidad.Remove(ee);
                MessageBox.Show(" Entidad eliminada correctamente ");
            }
            if (band1 == true)
            {
                MessageBox.Show(" Entidad que deseas eliminar no exite en lista ");
                band1 = false;
            }

            f.guardarArchivo(listaEntidad);
            manejo_dataGrid();
            nombreEntidad = new char[30];
            nombreEntidad[29] = '\n';

        }


        private void insertarAtributo()
        {
            //Nombre de la entidad donde se insertara el atributo
            band1 = false;
            nombreEntidad = new char[30];
            nombreEntidad[29] = '\n';
            //this.btnClickInputBox_Click(this, null);
            ////7777777777777777777777777777777777777777777777777777777777777777777777777777777777777777777
            VentanaEntidadesEx v = new VentanaEntidadesEx(listaEntidad);
            v.ShowDialog();

            //nombreEntidad = v.nombreEntidadSeleccionada;

            for (int k = 0; k < v.nombre.Length; k++)
            {
                nombreEntidad[k] = v.nombre[k];
            }

            for (int i = 0; i < listaEntidad.Count; i++)
            {
                if (compara(nombreEntidad, listaEntidad[i].nombre) == true)
                {
                    
                    //Nombre del atributo
                    btnClickInputBox_Click2(this, null);



                    atri = new Atributo(nombreAtributo, tipo_Dato_Atributo, longitud_tipo_dato_Atributo, dirrecion_Atributo, tipo_indice_Atributo, direccion_indice_Atributo, dirrecion_sig_Atributo);

                    atri.dirreccion = posicionMemoria;
                    atri.dirrecionAtributo = posicionMemoria;
                    if (listaEntidad[i].listaAtributos.Count == 0)
                    {
                        listaEntidad[i].DA = atri.dirreccion;
                    }
                    posicionMemoria = posicionMemoria + tamAtributo;

                    listaEntidad[i].agregaAtributo(atri);
                    //listaEntidad.Add(entidad);
                    band1 = false;
                    break;
                }
                else
                {
                    band1 = true;
                }
            }
            if (band1 == false)
            {
                MessageBox.Show(" Atributo agregado ");
            }
            if (band1 == true)
            {
                MessageBox.Show(" La entida donde desea insertar el atributo no existe ");
                band1 = false;
            }

            f.guardarArchivo(listaEntidad);
            
            manejo_dataGrid();

            nombreEntidad = new char[30];
            nombreEntidad[29] = '\n';
        }

        private void eliminarAtributo()
        {
            int posicion = 0, posicion2 = 0;
            //this.btnClickInputBox_Click(this, null);
            VentanaEntidadesEx v = new VentanaEntidadesEx(listaEntidad);
            v.ShowDialog();

            //nombreEntidad = v.nombreEntidadSeleccionada;

            for (int k = 0; k < v.nombre.Length; k++)
            {
                nombreEntidad[k] = v.nombre[k];

            }

            for (int i = 0; i < listaEntidad.Count; i++)
            {
                if (compara(listaEntidad[i].nombre, nombreEntidad))
                {
                    posicion = i;
                }
            }

            VentanaAtributosEx atr = new VentanaAtributosEx(listaEntidad[posicion].listaAtributos);
            atr.ShowDialog();

            for (int k = 0; k < atr.nombre.Length; k++)
            {
                nombreAtributo[k] = atr.nombre[k];

            }

            foreach (Atributo at in listaEntidad[posicion].listaAtributos)
            {
                if (compara(at.nombre, nombreAtributo))
                {
                    //posicion2 = i;

                    atri = at;
                    band1 = false;
                }

                else
                {
                    band1 = true;
                }

            }

            if (band1 == false)
            {
                listaEntidad[posicion].listaAtributos.Remove(atri);

                MessageBox.Show(" Se elimino el atributo correctamente");
            }
            if (band1 == true)
            {
                band1 = false;
                MessageBox.Show(" Atributo no existente en lista ");
            }

            f.guardarArchivo(listaEntidad);
            manejo_dataGrid();


            nombreEntidad = new char[30];
            nombreEntidad[29] = '\n';

            nombreAtributo = new char[30];
            nombreAtributo[29] = '\n';
        }

        private void eliminarTodo()
        {
            foreach (Entidad e in listaEntidad)
            {
                e.eliminaAtributos();
                cabecera = -1;
                posicionMemoria = -1;
            }

            f.guardarArchivo(listaEntidad);
            manejo_dataGrid();
            this.listaEntidad.Clear();
        }

        private void activarMenus()
        {
            aToolStripMenuItem.Enabled = true;
            entidadToolStripMenuItem.Enabled = true;
            entidadToolStripMenuItem1.Enabled = true;
            atributoToolStripMenuItem.Enabled = true;
            atributoToolStripMenuItem1.Enabled = true;
            atributoToolStripMenuItem2.Enabled = true;
            groupBox1.Enabled = true;
            todoToolStripMenuItem.Enabled = true;
            registroToolStripMenuItem.Enabled = true;

        }

        private void mostrarAtributo()
        {
            int count = 0;
            //llamar a declarar el nombre a mostrar la en+tidad
            this.btnClickInputBox_Click(this, null);
            //llamar a la atributo que esta en alguna entidad
            this.btnClickInputBox_Click3(this, null);
            foreach (Entidad entidad in listaEntidad)
            {
                if (nombreEntidad.Equals(entidad.nombre) == true)
                {
                    foreach (Atributo atributo in entidad)
                    {

                        if (nombreAtributo2.Equals(entidad.listaAtributos[count].nombre) == true)
                        {
                            atri = atributo;
                            band1 = false;
                            break;
                        }

                        count = count + 1;
                    }
                    break;
                }
                else
                {
                    band1 = true;
                }
            }
            if (band1 == false)
            {
                band2 = true;
                // cambiar este 77777777777777777777777777777777777777777777
                this.Form1_Paint(this, null);

            }
            if (band1 == true)
            {
                band1 = false;
                MessageBox.Show(" Atributo no existente ");
            }

            manejo_dataGrid();

            nombreEntidad = new char[30];
            nombreEntidad[29] = '\n';

            nombreAtributo = new char[30];
            nombreAtributo[29] = '\n';
        }

        private void mostrarEntidad()
        {
            this.btnClickInputBox_Click(this, null);
            foreach (Entidad entidad in this.listaEntidad)
            {
                if (nombreEntidad.Equals(entidad.nombre) == true)
                {
                    ee = entidad;
                    band1 = false;
                    break;
                }
                else
                {
                    band1 = true;
                }
            }
            if (band1 == false)
            {
                band3 = true;
                // cambiar este 777777777777777777777777777777777777777
                manejo_dataGrid();
                //this.Form1_Paint(this, null);//
            }
            if (band1 == true)
            {
                MessageBox.Show(" Entidad no existente ");
                band1 = false;
            }

            manejo_dataGrid();

            nombreEntidad = new char[30];
            nombreEntidad[29] = '\n';
        }

        private void modificarAtributo()
        {
            //Entidad que contiene el atributo a modificar
            //this.btnClickInputBox_Click(this, null);
            //Pide Nombre A modificar

            VentanaEntidadesEx v = new VentanaEntidadesEx(listaEntidad);
            v.ShowDialog();

            int posicion = 0;
            nombreAtributo = new char[30];
            nombreAtributo[29] = '\n';

            for (int k = 0; k < v.nombre.Length; k++)
            {
                nombreEntidad[k] = v.nombre[k];

            }

            for (int i = 0; i < listaEntidad.Count; i++)
            {
                if (compara(listaEntidad[i].nombre, nombreEntidad))
                {
                    posicion = i;
                }
            }

            //Pide nuevo nombre
            //this.btnClickInputBox_Click3(this, null);

            VentanaAtributosEx atr = new VentanaAtributosEx(listaEntidad[posicion].listaAtributos);
            atr.ShowDialog();

            for (int k = 0; k < atr.nombre.Length; k++)
            {
                nombreAtributo[k] = atr.nombre[k];

            }

            string nombreaux = "";
            nombreaux = Microsoft.VisualBasic.Interaction.InputBox(" Introduce nombre a modificar del atributo: ", "Diccionario De Datos. ", "");

            for (int k = 0; k < nombreaux.Length; k++)
            {
                nombreAtributo2[k] = nombreaux[k];
            }

            foreach (Atributo at in listaEntidad[posicion].listaAtributos)
            {
                if (compara(at.nombre, nombreAtributo))
                {
                    //posicion2 = i;

                    listaEntidad[posicion].modificarAtributo(nombreAtributo, nombreAtributo2);
                }

                else
                {
                    band1 = true;
                }

            }

            if (band1 == true)
            {
                MessageBox.Show(" Entidad no existente ");
            }

            f.guardarArchivo(listaEntidad);
            manejo_dataGrid();

            nombreEntidad = new char[30];
            nombreEntidad[29] = '\n';

            nombreAtributo = new char[30];
            nombreAtributo[29] = '\n';
        }

        private void modificarEntidad()
        {
            //Nombre de la entidad a modificar retorna nombreEntidad
            VentanaEntidadesEx v = new VentanaEntidadesEx(listaEntidad);
            v.ShowDialog();

            //nombreEntidad = v.nombreEntidadSeleccionada;

            for (int k = 0; k < v.nombre.Length; k++)
            {
                nombreEntidad[k] = v.nombre[k];

            }

            //nombre nuevo de la entidad
            this.btnClickInputBox_Click4(this, null);

            foreach (Entidad entidad in this.listaEntidad)
            {
                if (compara(entidad.nombre, nombreEntidad))
                {
                    entidad.nombre = nombreEntidad2;
                    band1 = false;
                    break;
                }
                else
                {
                    band1 = true;
                }
            }
            if (band1 == false)
            {
                MessageBox.Show(" Entidad modificada correctamente ");
            }
            if (band1 == true)
            {
                MessageBox.Show(" Entidad no existente ");
                band1 = false;
            }

            f.guardarArchivo(listaEntidad);
            manejo_dataGrid();
            nombreEntidad = new char[30];
            nombreEntidad[29] = '\n';
        }

        public void manejo_dataGrid()
        {
            //posicionMemoria = 8;
            //tamDato = 8;

            
            data.Clear();
            //listaEntidad.Clear();
            entidadesLeidas.Clear();

            //Refrescar el dataGribView
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            //Se inicializa el dataGribView correspondiente

            dataGridView1.ColumnCount = 5;
            dataGridView1.ColumnHeadersVisible = true;

            dataGridView1.Columns[0].Name = "Nombre";
            dataGridView1.Columns[1].Name = "Ap. Atributos";
            dataGridView1.Columns[2].Name = "Ap. Datos";
            dataGridView1.Columns[3].Name = "Pos.Inicial";
            dataGridView1.Columns[4].Name = "Ap.Sig.Entidad";



            // Se popula el primer dataGridView con los datos de las entidades
            List<String[]> filas = new List<string[]>();
            String[] fila;
            String nombreEnt = "";
            long apAt = 0;
            long apDat = 0;
            long posInic = 0;
            long apSigAt = 0;

            foreach (Entidad ent in listaEntidad)
            {
                if (ent.DSE > -2)

                {
                    nombreEnt = new string(ent.nombre);
                    apAt = ent.DA;
                    apDat = ent.DD;
                    posInic = ent.DE;
                    apSigAt = ent.DSE;

                    fila = new string[] { nombreEnt, apAt.ToString(), apDat.ToString(), posInic.ToString(), apSigAt.ToString() };

                    apAt = 0;
                    apDat = 0;
                    posInic = 0;
                    apSigAt = 0;

                    filas.Add(fila);
                }
            }

            foreach (string[] arr in filas)
            {
                dataGridView1.Rows.Add(arr);
            }
            //seAbrio = true;

        }


        public void manejo_dataGrid_atributos(Entidad ent)
        {
            
            //refresca o limpia el data gr
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();

            dataGridView2.ColumnCount = 7;
            dataGridView2.ColumnHeadersVisible = true;

            dataGridView2.Columns[0].Name = "Nombre";
            dataGridView2.Columns[1].Name = "Tipo";
            dataGridView2.Columns[2].Name = "Longitud";
            dataGridView2.Columns[3].Name = "Dir. del Atributo";
            dataGridView2.Columns[4].Name = "Tipo Indice";
            dataGridView2.Columns[5].Name = "Dir. Indice";
            dataGridView2.Columns[6].Name = "Dir. Sig. Atributo";

            List<String[]> filas = new List<string[]>();
            String[] fila = new string[] { };
            String nombreAtributo = "";
            char tipoDato;
            long longitud = 0;
            long posAt = 0;
            long apSigAt = 0;
            int tipoIndice = 0;
            long dirIndice = 0;
          
            filas = new List<string[]>();
            foreach (Atributo at in ent.listaAtributos)
            {
                if (at.dirrecionSigAtributo != -2 && at.dirrecionSigAtributo != -4)
                {
                    nombreAtributo = new String(at.nombre);
                    tipoDato = at.tipoDato;
                    longitud = at.longitudDato;
                    posAt = at.dirrecionAtributo;
                    apSigAt = at.dirrecionSigAtributo;
                    tipoIndice = at.tipoIndice;
                    dirIndice = at.dirreccionIndice;

                    if (apSigAt < -1)
                    {
                        apSigAt = -1;
                    }

                    if (at.tipoDato == 'S')
                    {
                        longitud = longitud / 2;
                    }

                    fila = new string[] { nombreAtributo, tipoDato.ToString(), longitud.ToString(), posAt.ToString(), tipoIndice.ToString(), dirIndice.ToString(), apSigAt.ToString() };

                    filas.Add(fila);
                }
            }

            foreach (string[] arr in filas)
            {
                dataGridView2.Rows.Add(arr);
            }
            
        }



        private void frmBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                f = new Archivo();
                f.abrirArchivo(listaEntidad);
                this.activarMenus();
            }
            if (e.Control && e.KeyCode == Keys.G)
            {
                //f.guardarArchivo(listaEntidad);
            }
            if (e.Control && e.KeyCode == Keys.N)
            {
                f = new Archivo();
                f.crearArchivo(0);
                this.activarMenus();
                band4 = true;
                this.Form1_Paint(this, null);//
            }
        }


        /*
        void lee_atributos_de_entidad(FileStream f, BinaryReader r, Entidad ent)

        {
            Atributo nAtributo = new Atributo(nombreAtributo, tipo_Dato_Atributo, longitud_tipo_dato_Atributo, dirrecion_Atributo, tipo_indice_Atributo, direccion_indice_Atributo, dirrecion_sig_Atributo); 

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

        }*/

        public bool comparaCadena(Char[] nombre1, Char[] nombre2)
        {
            for (int i = 0; i < nombre1.Length; i++)
            {

            }
            return false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string IdSeleccionado = dataGridView1.Rows[e.RowIndex].Cells["Nombre"].Value.ToString();

            //MessageBox.Show(IdSeleccionado);

            for (int k = 0; k < IdSeleccionado.Length; k++)
            {
                nombreEntidad[k] = IdSeleccionado[k];
            }
            for (int i = 0; i < listaEntidad.Count; i++)
            {
                if (compara(nombreEntidad, listaEntidad[i].nombre) == true)
                {
                    manejo_dataGrid_atributos(listaEntidad[i]);
                }
            }

                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            manejo_dataGrid();
        }

        private bool compara(char[] compra1, char[] compara2)
        {

            for (int k = 0; k < compara2.Length; k++)
            {
                if (compra1[k] == compara2[k])
                {

                }
                else
                    return false;
            }

            return true;
        }

        private bool menorCadena(char[] compra1, char[] compara2)
        {
            bool cadena = false;
            for (int k = 0; k < compara2.Length; k++)
            {
                if (compra1[k] > compara2[k])
                {
                    cadena = true;
                    break;
                }
                else
                    cadena = false;
            }
            return cadena;
        }

        private void acomodaApuntador(char[] nombAcomadar)
        {
            int posicion = 0;
            int apuntador = 0;
            for (int i = 0; i < listaEntidad.Count; i++)
            {
                if (compara(nombAcomadar, listaEntidad[i].nombre))
                {
                    posicion = i;
                }
            }

            if (listaEntidad.Count == 1)
            {

            }
            else
            {
                for (int i = 0; i < listaEntidad.Count; i++)
                {
                    if (menorCadena(listaEntidad[i].nombre, nombAcomadar))
                    {
                        buscaApuntador(listaEntidad[i].DE, listaEntidad[posicion].DE);
                        listaEntidad[posicion].DSE = listaEntidad[i].DE;
                        
                    }
                    else
                    {
                        if (listaEntidad[i].DSE == -1)
                        {
                            listaEntidad[i].DSE = listaEntidad[posicion].DE;
                            listaEntidad[posicion].DSE = -1;


                        }
                        else
                        {
                            //listaEntidad[posicion].DSE = listaEntidad[i].DE;
                        }
                    }
                }
            }
        }

        private void buscaApuntador(long x, long y)
        {
            for (int i = 0; i < listaEntidad.Count; i++)
            {
                if (x == listaEntidad[i].DSE)
                {
                    listaEntidad[i].DSE = y;
                }
            }
        }


        private void acomodarListaEnt()
        {
            
            for (int i = 0; i < listaEntidad.Count; i++)
            {
                //f.guardarArchivo(listaEntidad[i]);
            }
        }
        /*  public long dameAnterior(long buscado)
          {
              long encontrado;

              foreach (Entidad e in listaEntidad)
              {
                 // if()
              }


          }*/
    }
}


//COSAS QUE FALTAN
//muestra toma el dato que se maneja 
//
//ABRIR archivo
//ordenar atributos y metodos
//crear registros
//mostrar cabecera
