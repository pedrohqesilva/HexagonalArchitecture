namespace Core.Infrastructure.CrossCutting.Auth.Interfaces
{
    public interface IUser
    {
        string Id { get; set; }
        string UnidadeId { get; set; }
        string Email { get; set; }
        string Cpf { get; set; }
    }
}