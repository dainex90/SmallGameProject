using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEnumSwitch
{
    public class MagicWeapon : Weapon
    {
        public double ManaCost { get; set; }

        public MagicWeapon(string name, string description, double attackPower, double attackSpeed, double value, bool isRanged, double manaCost) : base(name, description, attackPower, attackSpeed, value, isRanged)
        {
            ManaCost = manaCost;
        }
    }
}
