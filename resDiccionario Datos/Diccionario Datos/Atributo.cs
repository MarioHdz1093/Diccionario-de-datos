using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diccionario_De_Datos
{
    
    public class Atributo
    {
        public char[] nombre = { '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\n' };

        public long dirreccion;
        public char tipoDato;
        public int longitudDato;
        public long dirrecionAtributo;
        public int tipoIndice;
        public long dirreccionIndice;
        public long dirrecionSigAtributo;

        public Atributo(char[] unNombre,char tip_Dat, int long_Dat, long dir_Atr, int t_Indice, long dir_Indi, long dir_Sig_Atr)
        {
            nombre = unNombre;
            tipoDato = tip_Dat;
            longitudDato =  long_Dat;
            dirrecionAtributo = dir_Atr;
            tipoIndice = t_Indice;
            dirreccionIndice = dir_Indi;
            dirrecionSigAtributo = dir_Sig_Atr;
        }
    }
}
