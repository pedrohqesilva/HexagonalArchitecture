using System;
using System.Collections.Generic;

namespace Core.Domain.ValueObjects
{
    public class Periodo : ValueObject
    {
        public DateTime? DataInicio { get; private set; }
        public DateTime? DataFim { get; private set; }

        public bool Ativo
        {
            get
            {
                return (!DataFim.HasValue || DateTime.Now <= DataFim);
            }
        }

        public Periodo(DateTime dataInicio, DateTime? dataFim = null)
        {
            DataInicio = dataInicio;
            DataFim = dataFim;
        }

        protected Periodo()
        {
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return DataInicio;
            yield return DataFim;
        }
    }
}