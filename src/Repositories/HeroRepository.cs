using System.Linq;
using Microsoft.EntityFrameworkCore;
using test_net_core_mvc.Models;
using test_net_core_mvc.Models.DataBase;

namespace test_net_core_mvc.Repositories
{
    public class HeroRepository : MasterRepository<Hero>
    {
        public HeroRepository(DbContextOptions<DataBaseContext> options) : base(options) { }

        public Hero GetShortestHeroes()
        {
            using(var context = Context)
            {
                var minHeight = context.Heroes.Select(hero => hero.Height).Min();
                return context.Heroes.Where(hero => hero.Height == minHeight).FirstOrDefault();
            }
        }
    }
}
