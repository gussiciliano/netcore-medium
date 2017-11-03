using System.Linq;
using Microsoft.EntityFrameworkCore;
using test_net_core_mvc.Models;
using test_net_core_mvc.Models.DataBase;

namespace test_net_core_mvc.Repositories
{
    public class UserRepository : MasterRepository<User>
    {
        public UserRepository(DbContextOptions<DataBaseContext> options) : base(options) { }

        public User GetByEmail(string email)
        {
            using(var context = Context)
            {
                return context.Users.SingleOrDefault(u => u.Email == email);
            }
        }

        public bool UserExists(int id)
        {
            using(var context = Context)
            {
                return context.Users.Any(u => u.Id == id);
            }
        }
    }
}