using ERP.Identity.Application.Interfaces;
using ERP.Identity.Domain.Entities;
using ERP.Identity.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ERP.Identity.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDbContext _db;

        public UserRepository(IdentityDbContext db)
        {
            _db = db;
        }

        public async Task<(Person person, Users user)> create_user(Person person, Users user)
        {
            using var transaction = await _db.Database.BeginTransactionAsync();

            try
            {
                await _db.Person.AddAsync(person);
                await _db.SaveChangesAsync();
                
                user.person_id = person.id;

                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();

                await transaction.CommitAsync();

                return (person, user);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Users?> get_user_by_user_name(string user_name)
        {
            return await _db.Users
                .Include(x => x.person)
                .FirstOrDefaultAsync(x => x.user_name == user_name);
        }

        public async Task<bool> user_name_exists(string user_name)
        {
            return await _db.Users.AnyAsync(x => x.user_name == user_name);
        }
    }
}
