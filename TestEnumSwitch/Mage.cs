using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEnumSwitch
{

    public class Mage : Character
    {
        public double MaxAbilityCount { get; set; } = 3;
        public double CurAbilityCount { get; set; }
        public List<Ability> AvailableAbilities { get; set; }
        public MagicWeapon? WeaponSwitching { get; set; }
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
        public Mage(string _class, string name, double powerBase, int armor, int gold, int maxHealth,
            string armorType) : base(_class, name, powerBase, armor, gold, maxHealth, armorType)
        {
            SpecialPotions = new List<string>();
            AllWeapons = new List<MagicWeapon>();
            AvailableAbilities = new List<Ability>();
            MagicWeapon starterWeapon = new MagicWeapon()
            {
                Name = "Bent Scepter",
                Description = "Useless for combat",
                Damage = 6,
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
            
            Console.WriteLine($"INVENTORY              {this.InventorySizeCurrent}/{this.InventorySizeMax}\n");
            int temp = 0;
            if (AllWeapons.Count > 0)
            {
                foreach (var weapon in AllWeapons)
                {
                    temp++;
                    Console.WriteLine($"{temp}. {weapon.Name}");
                }
            }
            temp = 0;
            if (AvailableAbilities.Count > 0)
            {
                Console.WriteLine("'Abilities:'");
                foreach (var item in AvailableAbilities)
                {
                    temp++;
                    Console.WriteLine($"\n\n{temp}. {item.Name} - {AttackType}({item.Damage}  Level({item.Level})");
                }
            }
            else
            {
                Console.WriteLine("EMPTY!");
                Console.Beep();
            }   
            
            Console.WriteLine("\n\n\n Press 'B' <--");
            base.ShowInventory();
        }

        public override void SelectingWeapon()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    EquipWeaponFromIndex(index:0);
                    break;
                case ConsoleKey.D2:
                    EquipWeaponFromIndex(index: 1);
                    break;
                case ConsoleKey.D3:
                    EquipWeaponFromIndex(index: 2);
                    break;
                case ConsoleKey.B:
                    Program.currentGameState = Program.GameStates.main;
                    break;
                default:
                    break;
            }
            base.SelectingWeapon();
        }

        public void EquipWeaponFromIndex(int index)
        {
            try
            {
                WeaponSwitching = CurWeapon;
                CurWeapon = AllWeapons[index];
                AllWeapons.RemoveAt(index);
                AllWeapons.Insert(index, CurWeapon);
                Console.WriteLine($"You Equipped {CurWeapon.Name}!");
                this.PowerMax = this.PowerBase + CurWeapon.Damage;
                Console.ReadKey(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

    }
}
