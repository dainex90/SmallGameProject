using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEnumSwitch
{
    // Base Class describing a weapon . Other Sub classes can inherit from this
    public class Weapon
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Damage { get; set; }
        public double AttackSpeed { get; set; }
        public double Value { get; set; }
        public bool IsRanged { get; set; }

        public Weapon()
        {
        }
    }
}
