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
        public string _class = "";
        public string name = "";
        public int power;
        public int armor;
        public int gold;
        public int maxHealth;
        public int curHealth;
        
        public List<string> inventory = new List<string>();
        public List<string> healthPotions = new List<string>();
        public List<string> specialPotions = new List<string>();
        public Dictionary<string, int> classSpecificPower = new Dictionary<string, int>();

        public string curWeapon = "";
        public string specialResource;
        public int maxSpecialResource = 100;
        public int curSpecialResource;
        public string armorType = "";
        

        // default constructor
        public Character()
        {}
        // Constructor
        public Character(string _class, string name, int power, int armor, int gold, int maxHealth, string weapon, 
            string armorType, string specialResource, string specialPotion)
        {
            this._class = _class;
            this.name = name;
            this.power = power;
            this.armor = armor;
            this.gold = gold;
            this.maxHealth = maxHealth;
            curHealth = maxHealth;
            curSpecialResource = maxSpecialResource;
            
           
            this.armorType = armorType;
            this.specialResource = specialResource;
            inventory.Add(weapon);
            specialPotions.Add(specialPotion);
            healthPotions.Add("Small Health Potion");

            this.SetPowerPairs();
        }
        // !experimental..
        public void SetPowerPairs()
        {
            if (this._class == "Ranger")
            {
                classSpecificPower.Add("Attack-Power", this.power);
            }
            else
            {
                classSpecificPower.Add("Spell-Power", this.power);
            }
            
        }

        public void GetPowerPairs()
        {
            // COde here
            foreach (var pair in classSpecificPower)
            {
                Console.WriteLine($"{pair.Key} : {pair.Value}");
            }
        }
        public void ShowInventory()
        {
            int inventoryKey = 0;
            char itemSelPointer = '-';
            
            if (inventory.Count > 0)
            {
                foreach (string item in inventory)
                {
                    Console.WriteLine($"{itemSelPointer} {item}");
                    inventoryKey++; 
                    Console.WriteLine($"Press {inventoryKey} to equip {item}");
                }
            }
            else
            {
                Console.Clear();
                Console.Beep();
                Console.WriteLine("Your Inventory is Empty");
                Program.SetCursorPos();
            }

            Console.WriteLine("\n\n'B'.) Go back <--");
        }

        public void EquipItem()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    try
                    {
                        curWeapon = inventory[0];
                        inventory.RemoveAt(0);
                        Console.WriteLine($"You Equipped {curWeapon}!");
                        power += 8;

                        this.ShowInventory();
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Can't equip!"); 
                    }
                    break;

                case ConsoleKey.B:
                    Program.currentGameState = Program.GameStates.ingame;
                    break;
                
                default:
                    break;
            }
        }
    }
}
