namespace DominioInterfaz
{
    public interface IPersona
    {
        string Nombre { get; set; }
        int PersonaID { get; set; }
        string Rol { get; set; }

        bool Equals(IPersona p);
        string ToString();
    }
}