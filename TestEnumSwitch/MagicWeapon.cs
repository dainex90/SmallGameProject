using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEnumSwitch
{
    public class MagicWeapon : Weapon
    {
        public double FireDamage { get; set; }
        public double MagicDamage { get; set; }
        public double ManaCost { get; set; }

        public MagicWeapon() : base()
        {
        }
    }
}
