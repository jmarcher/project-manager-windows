using System.Collections.Generic;
using Dominio;

namespace DominioInterfaz
{
    public interface IPersonificable
    {
        List<Persona> Personas { get; set; }

        void AgregarPersona(Persona persona);
    }
}