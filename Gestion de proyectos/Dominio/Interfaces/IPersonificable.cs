using System.Collections.Generic;

namespace Dominio
{
    public interface IPersonificable
    {
        List<Persona> Personas { get; set; }

        void AgregarPersona(Persona persona);
    }
}