using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_net_core_mvc.Models.DataBase;
using test_net_core_mvc.Repositories;

namespace test_net_core_mvc.Controllers
{
    public class HeroController : Controller
    {
        private HeroRepository _heroRepository { get; set; }
        public HeroController(DbContextOptions<DataBaseContext> options)
        {
            _heroRepository = new HeroRepository(options);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            return Json(_heroRepository.GetById(id));
        }

        [HttpGet]
        public IActionResult GetShortest()
        {
            return Json(_heroRepository.GetShortestHeroes());
        }
    }
}
