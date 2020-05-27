using Core.Domain.ValueObjects;

namespace Core.Domain.Constants
{
    public class Extensoes : Constant<string>
    {
        public Extensoes(string value)
            : base(value)
        {
        }

        public static Extensoes Doc => new Extensoes(".doc");
        public static Extensoes Docx => new Extensoes(".docx");
        public static Extensoes Pdf => new Extensoes(".pdf");
    }
}