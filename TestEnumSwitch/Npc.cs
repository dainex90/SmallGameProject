using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEnumSwitch
{
    internal class Npc
    {
        public string Name { get; set; }
        public bool IsFriendly { get; set; }
        public string Description { get; set; }
        //
        public Npc(string name, bool isFriendly, string description)
        {
            this.Name = name;
            this.IsFriendly = isFriendly;
            this.Description = description;
        }

        public virtual void Interact(Character player)
        {
            // Talk to strangers
            
        }
    }
}
