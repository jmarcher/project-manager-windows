using DominioInterfaz;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    public class Persona : IPersona
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonaID { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        
        public bool Equals(IPersona p)
        {
            return this.Nombre.Equals(p.Nombre);
        }

        public override string ToString()
        {
            return Nombre+" ["+Rol+"]";
        }
    }
}
