﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    public class Persona
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonaID { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        
        public bool Equals(Persona p)
        {
            return this.Nombre.Equals(p.Nombre);
        }
    }
}