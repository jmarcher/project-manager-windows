using System;

namespace Dominio
{
    public class TareaSimple : Tarea
    {
        private DateTime _FechaFinalizacion;

        public override DateTime FechaFinalizacion
        {
            get
            {
                return _FechaFinalizacion;
            }

            set
            {
                if (FechaNula(FechaInicio) || (!FechaNula(FechaInicio) && FechaEsMenor(FechaInicio, value)))
                {
                    _FechaFinalizacion = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }

            }

        }
        public TareaSimple() : base()
        {

        }

        public override int CalcularDuracionPendiente()
        {
            return DuracionPendiente;
        }
    }
}
