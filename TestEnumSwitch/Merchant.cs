using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEnumSwitch
{
    internal class Merchant : Npc
    {
        public Dictionary<string, int> itemsForSale;
        public Merchant(string name, bool isFriendly, string description) : base(name, isFriendly, description)
        {
        }

        public override void Interact(Character player)
        {
            Console.Clear();
            if (IsFriendly && player.curLevel >= 1)
            {
                // 1. Can Talk to merchant
                Console.WriteLine(Description);
                Console.ReadKey();
                // Create Items in Store
                CreateItems();
                // 2. Show Items for Player..
                ListItems();
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
            itemsForSale = new Dictionary<string, int>();

            itemsForSale.Add("Magi's Curved Stinger", 10);
            itemsForSale.Add("Hunter's Medium Bow", 11);
            itemsForSale.Add("Brittle Stave", 8);
        }

        public void ListItems()
        {
            int temp = 0;
            foreach (var item in itemsForSale)
            {
                temp++;
                Console.WriteLine($"\n{temp}. {item.Key} : 'Power' - {item.Value}");
            }

            Console.ReadLine();
        }
        private void GetItem()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:

                default:
                    break;
            }
        }
    }
}
