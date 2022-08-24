using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEnumSwitch
{
    internal class Merchant : Npc
    {
        
        public List<MagicWeapon> MagicWeaponsStore { get; set; }
        public List<PhysicalWeapon> PhysicalWeapons { get; set; }
        public List<Weapon> GeneralWeapons { get; set; }


        public Merchant(string name, bool isFriendly, string description) : base(name, isFriendly, description)
        {
            MagicWeaponsStore = new List<MagicWeapon>();
            PhysicalWeapons = new List<PhysicalWeapon>();
            GeneralWeapons = new List<Weapon>();

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
                
                // 2. Show Items for Player..
                ListItems();
                // Get Items if you can afford
                GetItem(player);
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
                AttackPower = 10,
                AttackSpeed = 6,
                Value = 60,
                IsRanged = true,
                ManaCost = 19
            };
            MagicWeapon weapon2 = new()
            {
                Name = "Fire Starter",
                Description = "A Fire has Started",
                AttackPower = 13,
                AttackSpeed = 12,
                Value = 100,
                IsRanged = true,
                ManaCost = 32
            };
            MagicWeapon weapon3 = new()
            {
                Name = "Quick Hand",
                Description = "None",
                AttackPower = 15,
                AttackSpeed = 15,
                Value = 107,
                IsRanged = true,
                ManaCost = 28
            };

            MagicWeapon weapon4 = new()
            {
                Name = "walking Stick",
                Description = "Lasts a While",
                AttackPower = 16,
                AttackSpeed = 16,
                Value = 69,
                IsRanged = false,
                ManaCost = 0
            };
            MagicWeaponsStore.Add(weapon1);
            MagicWeaponsStore.Add(weapon2);
            MagicWeaponsStore.Add(weapon3);
            MagicWeaponsStore.Add(weapon4);
        }

        public void ListItems()
        {
            int temp = 0;
            if (MagicWeaponsStore.Count > 0)
            {
                foreach (var weapon in MagicWeaponsStore)
                {
                    temp++;
                    Console.WriteLine($"\n{temp}. '{weapon.Name}'  Cost ({weapon.Value})Gold  Attack Power ({weapon.AttackPower})  Speed ({weapon.AttackSpeed})");
                }
            }
            else
            {
                Console.WriteLine("STORE IS EMPTY!");
            }

            Console.WriteLine($"{Program.newPlayer.Name}'s Gold: {Program.newPlayer.Gold}");

            Console.WriteLine("\n\n\n\nBack <-- 'B'");
        }

        private void GetItem(Mage player)
        {
            
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    // Get the item indexed at [0] if you can afford it!
                    if (CanAffordWeapon(player, 0) && HasInventorySpace())
                    {
                        player.AllWeapons.Add(MagicWeaponsStore[0]);
                        player.Gold -= MagicWeaponsStore[0].Value;
                        Console.WriteLine($"You Bought a {MagicWeaponsStore[0].Name}");
                        MagicWeaponsStore.RemoveAt(0);
                    }
                    else
                    {
                        Console.WriteLine("CAN'T BUY!");
                    }
                    break;
                case ConsoleKey.D2:
                    // get item Indexed at [1] and so forth..
                    if (CanAffordWeapon(player, 1) && HasInventorySpace())
                    {
                        player.AllWeapons.Add(MagicWeaponsStore[1]);
                        player.Gold -= MagicWeaponsStore[1].Value;
                        Console.WriteLine($"You Bought a {MagicWeaponsStore[1].Name}");
                        MagicWeaponsStore.RemoveAt(1);
                    }
                    else
                    {
                        Console.WriteLine("CAN'T BUY");
                    }
                    break;
                case ConsoleKey.D3:
                    if (CanAffordWeapon(player, 2) && HasInventorySpace())
                    {
                        player.AllWeapons.Add(MagicWeaponsStore[2]);
                        player.Gold -= MagicWeaponsStore[2].Value;
                        Console.WriteLine($"You Bought a {MagicWeaponsStore[2].Name}");
                        MagicWeaponsStore.RemoveAt(2);
                    }
                    else
                    {
                        Console.WriteLine("CAN'T BUY");
                    }
                    break;
                case ConsoleKey.D4:
                    if (CanAffordWeapon(player, 3) && HasInventorySpace())
                    {
                        player.AllWeapons.Add(MagicWeaponsStore[3]);
                        player.Gold -= MagicWeaponsStore[3].Value;
                        Console.WriteLine($"You Bought a {MagicWeaponsStore[3].Name}");
                        MagicWeaponsStore.RemoveAt(3);
                    }
                    else
                    {
                        Console.WriteLine("CAN'T BUY");
                    }
                    break;
                case ConsoleKey.B:
                    // Go back to the tavern again..
                    Program.currentGameState = Program.GameStates.ingame;
                    Program.currentLevelState = Program.LevelStates.tavern;
                    break;
                default:
                    break;
            }
        }

        private bool CanAffordWeapon(Mage player, int index)
        {
            try
            {
                if (MagicWeaponsStore[index].Value <= player.Gold)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            return false;
            
        }

        private bool HasInventorySpace()
        {
            if (Program.newPlayer.AllWeapons.Count >= (int)Program.newPlayer.InventorySize)
            {
                // Can't buy any more items from Store!
                return false;
            }

            else
            {
                // Can buy stuff..
                return true;
            }
        }
    }
}
