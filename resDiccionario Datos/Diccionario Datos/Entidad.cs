using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diccionario_De_Datos
{
    
    public class Entidad : List<Atributo>
    {
        public char[] nombre = { '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\n' };

        public long direccion;
        public long DE;
        public long DA;
        public long DD;
        public long DSE;
        public List<Atributo> listaAtributos;
        public bool band1;
        public bool band2;
        public Atributo aux1;

        public Entidad(char[] unNombre,long dirEntidad,long dirAtr,long dirDato,long dirSigDato)
        {
            listaAtributos = new List<Atributo>();
            nombre = unNombre;
            band1 = false;
            band2 = false;
            DE = dirEntidad;
            DA = dirAtr;
            DD = dirDato;
            DSE = dirSigDato;
            
        }

        public void agregaAtributo(Atributo atributo)
        {
            foreach (Atributo a in this.listaAtributos)
            {
                if (a.Equals(atributo) == true)
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
                //thislistaatr.Add(atributo);
                this.listaAtributos.Add(atributo);
                if (listaAtributos.Count > 1)
                {
                    this.listaAtributos[listaAtributos.Count - 2].dirrecionSigAtributo = this.listaAtributos[listaAtributos.Count -1].dirrecionAtributo;
                }
            }
            if (band1 == true)
            {
                MessageBox.Show(" Atributo que desea insertar ya existe, Ingrese otro  ");
                band1 = false;
            }

        }

        public void eliminaAtributo(char[] nombre)
        {
            
            foreach (Atributo a in this.listaAtributos)
            {
                
                if (a.nombre.Equals(nombre) == true)
                {
                    Atributo aux1 = new Atributo(a.nombre, a.tipoDato, a.longitudDato, a.dirrecionAtributo, a.tipoIndice, a.dirreccionIndice, a.dirrecionSigAtributo);
                    aux1 = a;
                    this.Remove(aux1);
                    break;
                }
            }
        }

        public void eliminaAtributos()
        {
            this.Clear();
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

        public void modificarAtributo(char[] modificar, char[] nuevo)
        {
            foreach (Atributo a in listaAtributos)
            {
                if (compara(a.nombre,modificar))
                {
                    a.nombre = nuevo;
                    break;
                }
                else
                {
                    band1 = true;
                }
            }
            if (band1 == false)
            {
                MessageBox.Show(" Atributo: " + modificar.ToString() + " Modificado: " + nuevo.ToString());
            }
            if (band1 == true)
            {
                MessageBox.Show(" Atributo no encontrado ");
                band1 = false;
            }
        }
    }
}
