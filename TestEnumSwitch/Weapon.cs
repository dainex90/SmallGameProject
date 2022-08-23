﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEnumSwitch
{
    // Base Class describing a weapon . Other Sub classes can inherit from this
    public class Weapon
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double AttackPower { get; set; }
        public double AttackSpeed { get; set; }
        public double Value { get; set; }
        public bool IsRanged { get; set; }

        public Weapon(string name, string description, double attackPower, double attackSpeed, double value, bool isRanged)
        {
            Name = name;
            Description = description;
            AttackPower = attackPower;
            AttackSpeed = attackSpeed;
            Value = value;
            IsRanged = isRanged;
        }
    }
}