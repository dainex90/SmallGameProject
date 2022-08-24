using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEnumSwitch
{

    public class Mage : Character
    {   
        public MagicWeapon? PreviousWeapon { get; set; }
        public MagicWeapon? CurWeapon { get; set; }
        public List<string> SpecialPotions { get; set; }
        public List<MagicWeapon> AllWeapons { get; set; }
        public string AttackType { get; set; } = "Spell-Power";

        public string SpecialResource { get; set; }
        public int MaxSpecialResource { get; set; }
        public int CurSpecialResource { get; set; }

        public Mage() : base()
        {
        }
        public Mage(string _class, string name, double power, int armor, int gold, int maxHealth,
            string armorType) : base(_class, name, power, armor, gold, maxHealth, armorType)
        {
            SpecialPotions = new List<string>();
            AllWeapons = new List<MagicWeapon>();
            MagicWeapon starterWeapon = new MagicWeapon()
            {
                Name = "Bent Scepter",
                Description = "Useless for combat",
                AttackPower = 6,
                AttackSpeed = 3,
                Value = 20,
                IsRanged = true,
                ManaCost = 15
            };

            AllWeapons.Add(starterWeapon);
            SpecialResource = "Mana";
            MaxSpecialResource = 100;
            CurSpecialResource = MaxSpecialResource;
        }

        public override void ShowInventory()
        {
            Console.Clear();
            Console.WriteLine($"INVENTORY\n\n\n");
            int temp = 0;
            if (AllWeapons.Count > 0)
            {
                foreach (var weapon in AllWeapons)
                {
                    temp++;
                    Console.WriteLine($"{temp}. {weapon.Name}");
                }
            }
            else
            {
                Console.WriteLine("EMPTY!");
            }   
            
            Console.WriteLine("\n\n\n Press 'B' <--");
            base.ShowInventory();
        }

        public override void EquipItem()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    try
                    {
                        CurWeapon = AllWeapons[0];
                        AllWeapons.RemoveAt(0);
                        Console.WriteLine($"You Equipped {CurWeapon.Name}!");
                        
                        this.Power += CurWeapon.AttackPower;
                        Console.ReadKey(true);
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Can't Equip!");
                    
                    }
                    Console.Beep();
                    break;

                case ConsoleKey.B:
                    Program.currentGameState = Program.GameStates.ingame;
                    break;

                default:
                    break;
            }
            base.EquipItem();
        }



    }
}
