using System;
using System.Collections.Generic;

namespace Core.Domain.ValueObjects
{
    public class Protocolo : ValueObject
    {
        public long Id { get; private set; }
        public short Ano { get; private set; } = (short)DateTime.Now.Year;

        public Protocolo(string protocolo)
        {
            Id = ObterId(protocolo);
            Ano = ObterAno(protocolo);
        }

        public Protocolo(long id, short ano)
        {
            Id = id;
            Ano = ano;
        }

        public static implicit operator string(Protocolo protocolo)
        {
            return protocolo.ToString();
        }

        public static implicit operator Protocolo(string protocolo)
        {
            var ano = ObterAno(protocolo);
            var id = ObterId(protocolo);
            return new Protocolo(id, ano);
        }

        public override string ToString()
        {
            return $"{Id}{Ano}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Ano;
        }

        private static long ObterId(string protocolo)
        {
            if (protocolo?.Length > 4)
            {
                return long.Parse(protocolo.Substring(0, protocolo.Length - 4));
            }
            return 0;
        }

        private static short ObterAno(string protocolo)
        {
            if (protocolo?.Length > 4)
            {
                return short.Parse(protocolo.Substring(protocolo.Length - 4));
            }
            return 0;
        }
    }
}