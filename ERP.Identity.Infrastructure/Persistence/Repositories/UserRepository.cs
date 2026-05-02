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

        public async Task<(PersonEntity person, UsersEntity user)> create_user(PersonEntity person, UsersEntity user)
        {
            using var transaction = await _db.Database.BeginTransactionAsync();

            try
            {
                await _db.Person.AddAsync(person);
                await _db.SaveChangesAsync();
                
                user.PersonId = person.Id;

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

        public async Task<UsersEntity?> get_user_by_user_name(string user_name)
        {
            return await _db.Users
                .Include(x => x.Person)
                .FirstOrDefaultAsync(x => x.UserName == user_name);
        }

        public async Task<bool> user_name_exists(string user_name)
        {
            return await _db.Users.AnyAsync(x => x.UserName == user_name);
        }
    }
}
