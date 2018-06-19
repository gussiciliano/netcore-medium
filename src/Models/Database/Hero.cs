using System.Collections.Generic;

namespace test_net_core_mvc.Models.DataBase
{
    public class Hero : BaseEntity
    {
        public string Name { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public List<string> Powers { get; set; }
    }
}
