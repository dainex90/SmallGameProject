using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEnumSwitch
{
    public class PhysicalWeapon : Weapon
    {
        public double StaminaCost { get; set; }
        public double PhysicalDamage { get; set; }

        public PhysicalWeapon() : base()
        {
        }
    }
}
