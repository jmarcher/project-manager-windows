using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    interface IFechas
    {
        DateTime FechaInicio { get; set;}
        DateTime FechaFinalizacion { get; }
    }
}
