using Core.Infrastructure.CrossCutting.Auth.Interfaces;

namespace Core.Infrastructure.CrossCutting.Auth
{
    public class User : IUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string UnidadeId { get; set; }

        public User()
        {
        }

        public User(string id, string email, string cpf, string unidadeId)
        {
            Id = id;
            Email = email;
            Cpf = cpf;
            UnidadeId = unidadeId;
        }
    }
}