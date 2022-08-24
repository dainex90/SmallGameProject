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

        public PhysicalWeapon(string name, string description, double attackPower, double attackSpeed, double cost, bool isRanged, double staminaCost) : base()
        {
            StaminaCost = staminaCost;
        }
    }
}
