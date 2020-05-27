using System;
using System.Collections.Generic;

namespace Core.Domain.ValueObjects
{
    public class UploadPdf : ValueObject
    {
        public Guid CodigoExterno { get; private set; }
        public long TipoArquivoId { get; private set; }
        public long UnidadeId { get; private set; }

        public UploadPdf(Guid codigoExterno, long tipoArquivoId, long unidadeId)
        {
            CodigoExterno = codigoExterno;
            TipoArquivoId = tipoArquivoId;
            UnidadeId = unidadeId;
        }

        public static implicit operator Guid(UploadPdf uploadPdf)
        {
            return uploadPdf.CodigoExterno;
        }

        public static implicit operator string(UploadPdf uploadPdf)
        {
            return uploadPdf.CodigoExterno.ToString();
        }

        protected UploadPdf()
        {
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return CodigoExterno;
            yield return TipoArquivoId;
            yield return UnidadeId;
        }
    }
}