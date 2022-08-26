using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEnumSwitch
{
    public class Character : CharacterBase
   {
        // Fields and propertys
        public string Class { get; set; }
        public string Name {get;set;}
        public double PowerBase {get;set;}
        public double PowerMax { get; set;}
        public int Armor {get;set;}
        public double Gold {get;set;}
        public double MaxHealth {get;set;}
        public double CurHealth {get;set;}
        public string ArmorType { get; set; }
        
        public Character()
        {

        }
        // Constructor
        public Character(string _class, string name, double powerBase, int armor, double gold, double maxHealth, 
            string armorType)
        {
            this.Class = _class;
            this.Name = name;
            this.PowerBase = powerBase;
            this.Armor = armor;
            this.Gold= gold;
            this.MaxHealth = maxHealth;
            this.CurHealth = maxHealth;
           
            this.ArmorType = armorType;
            this.PowerMax = PowerBase;
        }
        public virtual void ShowInventory()
        {   
            // Do i need these functions in base Class??
        }

        public virtual void SelectingWeapon()
        {
            // Do i need these functions in base Class??
        }
    }
}
