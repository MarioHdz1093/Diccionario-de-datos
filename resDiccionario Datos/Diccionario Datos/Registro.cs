using Diccionario_De_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diccionario_Datos
{
    class Registro
    {
        char[] nombre = { '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\n' };
        Entidad entidadManejar;

        public Registro(Entidad EntidadEntrante)
        {
            entidadManejar = EntidadEntrante;
        }
    }
}
