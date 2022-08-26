using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TestEnumSwitch
{
    internal class Merchant<T> : Npc where T : MagicWeapon
    {
        
        public List<T> MagicWeaponsStore { get; set; }
        public List<PhysicalWeapon> PhysicalWeapons { get; set; }
        public List<Weapon> GeneralWeapons { get; set; }
        public List<Ability> Abilities { get; set; }
        public List<Ability> AbilitiesOrdered { get; set; }
        private bool atMerchant = true;

        public Merchant(string name, bool isFriendly, string description) : base(name, isFriendly, description)
        {
            MagicWeaponsStore = new List<T>();
            PhysicalWeapons = new List<PhysicalWeapon>();
            GeneralWeapons = new List<Weapon>();
            Abilities = new List<Ability>();

            // Create Items ONLY 1 Time in the beginning!
            this.CreateItems();
        }

        public override void Interact(Mage player)
        {
            Console.Clear();
            if (IsFriendly && player.curLevel >= 1)
            {
                // 1. Can Talk to merchant

                GeneralGameFunctionality.DelayTextOutput(Description);
                Thread.Sleep(1500);
                Console.WriteLine("\n");
            
                atMerchant = true;
                while (atMerchant)
                {
                    Console.Clear();
                    // 2. Show Items for Player..
                    ListItems(MagicWeaponsStore);
                    // Select what item you want ..
                    ItemSelectionInput(player);
                } 
            }
            else
            {
                // can't talk to this merchant or npc.
                Console.WriteLine("Get away from here stranger!");
                Console.ReadLine();
            }
        }

        public void CreateItems()
        {
            MagicWeapon weapon1 = new MagicWeapon()
            {
                Name = "Balanced Stick",
                Description = "A finer Stick",
                Damage = 10,
                AttackSpeed = 6,
                Value = 60,
                IsRanged = true,
                ManaCost = 19
            };
            MagicWeapon weapon2 = new()
            {
                Name = "Fire Starter",
                Description = "A Fire has Started",
                Damage = 3,
                AttackSpeed = 12,
                FireDamage = 12,
                Value = 100,
                IsRanged = true,
                ManaCost = 32
            };
            MagicWeapon weapon3 = new()
            {
                Name = "Quick Hand",
                Description = "None",
                Damage = 14,
                AttackSpeed = 20,
                Value = 107,
                IsRanged = true,
                ManaCost = 28
            };

            MagicWeapon weapon4 = new()
            {
                Name = "walking Stick",
                Description = "Lasts a While",
                Damage = 16,
                AttackSpeed = 16,
                Value = 170,
                IsRanged = false,
                ManaCost = 0
            };
            MagicWeaponsStore.Add((T)weapon1);
            MagicWeaponsStore.Add((T)weapon2);
            MagicWeaponsStore.Add((T)weapon3);
            MagicWeaponsStore.Add((T)weapon4);
        }
        public void CreateItems(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Ability ability = new Ability("'Ice-Spark'", i, "Frost", (10+i), (25+i));
                Abilities.Add(ability);
            }  
        }
        public void ListItems(List<T> items)
        {
            int temp = 0;
            if (items.Count > 0)
            {
                foreach (var weapon in items)
                {
                    temp++;
                    Console.WriteLine($"\n{temp}. '{weapon.Name.ToUpper()}'  Cost: ({weapon.Value})G  Damage: ({weapon.Damage})  Speed: ({weapon.AttackSpeed})");
                    if (weapon.FireDamage > 0)
                    {
                        Console.WriteLine($"Fire-Damage: {weapon.FireDamage}");
                    }
                }
            }
            else
            {
                Console.WriteLine("STORE IS EMPTY!");
            }

            Console.WriteLine($"{Program.player.Name}'s Gold: {Program.player.Gold}");

            Console.WriteLine("\n\n\n\nBack <-- 'B'");
        }

        public void ListItems()
        {
            int temp = 1;
            AbilitiesOrdered = Abilities.OrderBy(ability => ability.Cost).ToList();
            Console.WriteLine($"{this.Description} {Program.player.Name}                            GOLD: {Program.player.Gold}" );
            foreach (var item in AbilitiesOrdered)
            {
                Console.WriteLine($"{temp}- {item.Name} Cost({item.Cost})G  Damage: {item.Damage}  Level: {item.Level + 1}");
            }
            Console.ReadKey(true);

        }
        private void ItemSelectionInput(Mage player)
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    // Get the item indexed at [0] if you can afford it!
                    if (CanAffordWeapon(player, index: 0) && HasInventorySpace())
                    {
                        GetItemOperation(player, index:0);
                    }
                    break;
                case ConsoleKey.D2:
                    // get item Indexed at [1] and so forth..
                    if (CanAffordWeapon(player, index: 1) && HasInventorySpace())
                    {
                        GetItemOperation(player, index:1);
                    }
                    break;
                case ConsoleKey.D3:
                    if (CanAffordWeapon(player, index: 2) && HasInventorySpace())
                    {
                        GetItemOperation(player, index:2);
                    }
                    break;
                case ConsoleKey.D4:
                    if (CanAffordWeapon(player, index: 3) && HasInventorySpace())
                    {
                        GetItemOperation(player, index:3);
                    }
                    break;
                case ConsoleKey.B:
                    // Go back to the tavern again..
                    Program.currentGameState = Program.GameStates.main;
                    Program.currentLevelState = Program.LevelStates.tavern;
                    atMerchant = false;
                    break;

                default:
                    break;
            }

            Console.ReadKey(true);
        }
        private void GetItemOperation(Mage player, int index)
        {
            player.AllWeapons.Add(MagicWeaponsStore[index]);
            player.Gold -= MagicWeaponsStore[index].Value;
            Console.WriteLine($"You Bought a {MagicWeaponsStore[index].Name}");
            MagicWeaponsStore.RemoveAt(index);
            player.InventorySizeCurrent += 1;
        }

    private bool CanAffordWeapon(Mage player, int index)
        {
            try
            {
                if (MagicWeaponsStore[index].Value <= player.Gold)
                {
                    // Can afford it!
                    return true;
                }
                else
                {
                    Console.WriteLine("\nNot enough Gold!");
                    return false;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("You already bought that item");
            }
            return false;
            
        }

        private static bool HasInventorySpace()
        {
            if (Program.player.AllWeapons.Count >= (int)Program.player.InventorySizeMax)
            {
                // Inventory is full!!
                Console.WriteLine("\nInventory is full!");
                return false;
            }

            else
            {
                // Can buy stuff..
                return true;
            }
        }

        public void ItemSelectionInput()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    if (Program.player.CurAbilityCount < Program.player.MaxAbilityCount && Program.player.Gold >= AbilitiesOrdered[0].Cost)
                    {
                        // Buy Ability
                        Program.player.AvailableAbilities.Add(AbilitiesOrdered[0]);
                        Program.player.Gold -= AbilitiesOrdered[0].Cost;
                        Console.WriteLine($"You Bought a {AbilitiesOrdered[0].Name}");
                        AbilitiesOrdered.RemoveAt(0);
                        Program.player.CurAbilityCount += 1;
                    }
                    else
                    {
                        Console.WriteLine("Can't Buy!");
                    }
                    break;
                default:
                    break;
            }
            Console.ReadKey(true);
        }
    }
}
