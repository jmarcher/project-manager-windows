namespace Dominio
{
    public class Persona
    {
        public string Nombre { get; set; }
        public string Rol { get; set; }
        
        public bool Equals(Persona p)
        {
            return this.Nombre.Equals(p.Nombre);
        }
    }
}
