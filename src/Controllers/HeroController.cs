using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_net_core_mvc.Models.DataBase;
using test_net_core_mvc.Repositories;

namespace test_net_core_mvc.Controllers
{
    [Route("Heroes")]
    public class HeroController : Controller
    {
        private HeroRepository _heroRepository { get; set; }
        public HeroController(DbContextOptions<DataBaseContext> options)
        {
            _heroRepository = new HeroRepository(options);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Json(_heroRepository.GetById(id));
        }

        [HttpGet("Shortest")]
        public IActionResult GetShortest()
        {
            return Json(_heroRepository.GetShortestHeroes());
        }

        [HttpPost]
        public IActionResult Create(Hero hero)
        {
            var response = string.Empty;
            try
            {
                _heroRepository.Insert(hero);
                return Json("Hero inserted succesfully");
            }
            catch (Exception)
            {
                return Json("Error inserting hero");
            }
        }
    }
}
