using System.Collections.Generic;
using System.Linq;

namespace Core.Domain.ValueObjects
{
    public class CpfCnpj : ValueObject
    {
        public string ValorTexto { get; private set; }
        public long ValorInteiro { get; private set; }
        public bool IsCpf
        {
            get
            {
                return VerificarCpf(ValorTexto);
            }
        }

        public bool IsCnpj
        {
            get
            {
                return VerificarCnpj(ValorTexto);
            }
        }
        
        public CpfCnpj(string valor)
        {
            if (Validar(valor))
            {
                ValorTexto = valor;
                ValorInteiro = long.Parse(valor);
            }
        }

        public CpfCnpj(long valor)
        {
            if (Validar(valor.ToString()))
            {
                ValorInteiro = valor;
                ValorTexto = valor.ToString();
            }
        }

        protected CpfCnpj()
        {
        }

        public static implicit operator long?(CpfCnpj cpfCnpj)
        {
            return cpfCnpj?.ValorInteiro;
        }

        public static implicit operator string(CpfCnpj cpfCnpj)
        {
            return cpfCnpj?.ValorTexto;
        }

        public static implicit operator CpfCnpj(string valor)
        {
            return new CpfCnpj(valor);
        }

        public static implicit operator CpfCnpj(long valor)
        {
            return new CpfCnpj(valor);
        }

        private bool Validar(string cpfCnpj)
        {
            if (VerificarCpf(cpfCnpj))
                return ValidarCpf(cpfCnpj);
            else if (VerificarCnpj(cpfCnpj))
                return ValidarCnpj(cpfCnpj);
            return false;
        }

        private bool ValidarCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Distinct().Count() == 1)
                return false;

            if (cpf.Length < 11)
                cpf = cpf.PadLeft(11, '0');

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf += digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();
            return cpf.EndsWith(digito);
        }

        private bool ValidarCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length > 11 && cnpj.Length < 14)
            {
                cnpj = cnpj.PadLeft(14, '0');
            }

            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj += digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();
            return cnpj.EndsWith(digito);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return ValorTexto;
            yield return ValorInteiro;
        }
        private bool VerificarCpf(string valor)
        {
            return valor?.Length == 11;
        }
        private bool VerificarCnpj(string valor)
        {
            return valor?.Length == 14;
        }
    }
}