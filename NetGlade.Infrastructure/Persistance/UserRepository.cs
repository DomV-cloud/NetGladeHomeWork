using NetGlade.Application.Common.Interfaces.Persistance;
using NetGlade.Domain.Entities;
using NetGlade.Application.DatabaseContext;
namespace NetGlade.Infrastructure.Persistance
{
    public class UserRepository : IUserRepository
    {
        private readonly NetGladeContext _context;

        public UserRepository(NetGladeContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
           using (var context = _context)
           {
                context.Users.Add(user);

                context.SaveChanges();
            }
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }
    }
}
