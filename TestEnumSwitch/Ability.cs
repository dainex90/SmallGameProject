namespace TestEnumSwitch
{
    public class Ability
    {
        //public string Type { get; set; }
        public string Name { get; set; }
        public double Level { get; set; }
        public string School { get; set; }
        public double Damage { get; set; }
        public double Cost { get; set; }

        public Ability(string name, double level, string school, double damage, double cost)
        {
            //Type = type;
            Name = name;
            Level = level;
            School = school;
            Damage = damage;
            Cost = cost;
        }
        


    }
}