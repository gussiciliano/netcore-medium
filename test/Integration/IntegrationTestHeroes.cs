using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using test_net_core_mvc;
using test_net_core_mvc.Models.DataBase;
using Xunit;

namespace test.Integration
{
    public class IntegrationTestHeroes
    {
        private readonly HttpClient _client;
        private readonly DataBaseContext _context;
        public IntegrationTestHeroes()
        {
            // Set up server configuration
            var configuration = new ConfigurationBuilder()
                                // Indicate the path for our source code, where the appsettings file is located
                                .SetBasePath(Path.GetFullPath(@"../../../../src/"))
                                .AddJsonFile("appsettings.Test.json", optional: false)
                                .Build();
            // Create builder
            var builder = new WebHostBuilder()
                            // Set test environment
                            .UseEnvironment("Testing")
                            .UseStartup<Startup>()
                            .UseConfiguration(configuration);
            // Create test server
            var server = new TestServer(builder);
            // Create database context
            this._context = server.Host.Services.GetService(typeof(DataBaseContext)) as DataBaseContext;
            // Create client to query server endpoints
            this._client = server.CreateClient();
        }

        [Theory]
        // Set the hero list we will be using for testing
        [MemberData(nameof(Heroes))]
        public async Task InsertAndGetHeroes(string name, float height, float weight, string powers)
        {
            // Create hero object
            var hero = new Hero { Name = name, Height = height, Weight = weight, Powers = powers };
            // Add heroes to database
            _context.Heroes.Add(hero);
            _context.SaveChanges();
            // Request for the hero we just created
            var response = await _client.GetAsync($"/heroes/{hero.Id}");
            // Check if status code is OK
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            // Get JSON  of the hero from response
            var jsonResponse = await response.Content.ReadAsStringAsync();
            // Deserialize response JSON to Hero class
            var heroResponse = JsonConvert.DeserializeObject<Hero>(jsonResponse);
            // Check if the hero is the same
            Assert.Equal(hero.Id, heroResponse.Id);
            Assert.Equal(hero.Name, heroResponse.Name);
        }

        [Fact]
        public async Task InsertAndGetShortestHero()
        {
            // Convert object list to Hero List
            var heroList = Heroes.Select(h => new Hero{
                Name = h[0].ToString(),
                Height = Convert.ToSingle(h[1].ToString()),
                Weight = Convert.ToSingle(h[2].ToString()),
                Powers = h[3].ToString(),
            }).ToList();
            // Add heroes to database
            _context.Heroes.AddRange(heroList);
            _context.SaveChanges();
            // Set the value for the expected response (hero with min height)
            var expectedHero = heroList.Select(h => (h.Height, h)).Min().Item2;
            // Request for the shortest hero
            var response = await _client.GetAsync($"/heroes/shortest");
            // Check if status code is OK
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            // Get JSON from response
            var jsonResponse = await response.Content.ReadAsStringAsync();
            // Deserialize response JSON to Hero class
            var heroResponse = JsonConvert.DeserializeObject<Hero>(jsonResponse);
            // Check if the hero is the same
            Assert.Equal(expectedHero.Id, heroResponse.Id);
            Assert.Equal(expectedHero.Name, heroResponse.Name);
        }

        // Create data for tests
        public static IEnumerable<object[]> Heroes =>
            new List<object[]>
            {
                new object[]{ "Strong Bob", 160, 80, "Super Strength" },
                new object[]{ "Fast Rob", 150, 70,  "Super Speed" },
                new object[]{ "Magic Cob", 180, 90,  "Magic" },
            };
    }
}
