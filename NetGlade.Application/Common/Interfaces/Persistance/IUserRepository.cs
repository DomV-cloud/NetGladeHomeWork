using NetGlade.Domain.Entities;

namespace NetGlade.Application.Common.Interfaces.Persistance
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);

        void Add(User user);

        List<User> GetAll();
    }
}
