ï»¿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio
{
    public class Etapa
    {
        public int Id { get; set; }

        public override bool Equals(object obj)
        {
            Etapa e = (Etapa)obj;
            return e.Id == this.Id;
        }
    }
}
