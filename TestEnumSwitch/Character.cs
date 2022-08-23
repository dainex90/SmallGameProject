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
        public double Power {get;set;} 
        public int Armor {get;set;}
        public int Gold {get;set;}
        public int MaxHealth {get;set;}
        public int CurHealth {get;set;}
        public string ArmorType { get; set; }

        // Constructor
        public Character(string _class, string name, double power, int armor, int gold, int maxHealth, 
            string armorType)
        {
            this.Class = _class;
            this.Name = name;
            this.Power = power;
            this.Armor = armor;
            this.Gold= gold;
            this.MaxHealth = maxHealth;
            this.CurHealth = maxHealth;
           
            this.ArmorType = armorType;     
        }
        public virtual void ShowInventory()
        {   
            // Do i need these functions in base Class??
        }

        public virtual void EquipItem()
        {
            // Do i need these functions in base Class??
        }
    }
}
