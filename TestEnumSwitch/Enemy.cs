using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEnumSwitch
{
    // base Enemy class
    public class Enemy
    {
        public string Name { get; set; }
        public string Description { get; set; } 
        public int Level { get; set; }
        public int Power { get; set; }
        public double CurHealth { get; internal set; }
        public double MaxHealth { get; set; }
        
        public Enemy()
        {
        }
    }
}
