// See https://aka.ms/new-console-template for more information

using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;

using System.Media;
using System.Runtime.CompilerServices;

namespace TestEnumSwitch
{
    class Program
    {
        public enum GameStates
        {
            start,
            charcreation,
            prologue,
            main,
            inventory,
            interact,
            incombat,
            exit
        }

        public enum LevelStates
        {
            forest,
            city,
            tavern,
            mountainpass,
            snowyPeaks
        }
        private static ConsoleColor standardForegroundColor = ConsoleColor.Cyan;
        private static ConsoleColor standardBackgroundColor = ConsoleColor.DarkGray;
        public static Merchant<MagicWeapon> newMerchant;
        public static Mage player;
        static bool chooseClass = true;
        static int width = Console.WindowWidth;
        static int height = Console.WindowHeight;

        // enums
        public static GameStates currentGameState;
        public static LevelStates currentLevelState;

    static void Main(string[] args)
    {
        currentGameState = GameStates.start;
        currentLevelState = LevelStates.forest;
        Console.ForegroundColor = standardForegroundColor;
        Console.BackgroundColor = standardBackgroundColor;

            // MAIN GAME-LOOP!!
         while (currentGameState != GameStates.exit)
        {   
            switch (currentGameState)
            {
                case GameStates.start:
                    SetCursorPos(width / 2, height / 2);
                    Console.WriteLine("A HERO'S REQUEST");
                    SetCursorPos(0, 0);
                    Console.WriteLine("Press 'ENTER'");
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        currentGameState = GameStates.charcreation;
                        Console.Clear();
                    }
                    break;

                case GameStates.charcreation:
                    Console.WriteLine("Please, enter a Name: ");
                    string playerName = Console.ReadLine();
                    Console.Clear();

                    while (chooseClass)
                    {
                        Console.Clear();
                        Console.WriteLine("Choose a Class to play: \n\n\n\n1.) Mage\n\n2.) Hunter");
                        SetCursorPos();
                        if (Console.ReadKey(true).Key == ConsoleKey.D1)
                        {
                            player = new Mage(
                                        _class:"Mage",
                                        name: playerName,
                                        powerBase:7,
                                        armor:3,
                                        gold:20000,
                                        maxHealth:100,
                                        armorType:"Light Armor");
                            
                            chooseClass = false;
                        }
                        //else if (Console.ReadKey().Key == ConsoleKey.D2)
                        //{
                        //    newPlayer = new Character("Ranger", playerName, 10, 8, 50, 100, "Scrap Bow", "Leather Armor", "Energy", "Dexterity Booster");
                        //    chooseClass = false;
                        // }
                            
                        else
                        {
                            Console.WriteLine("\nInvalid Key, choose a Class");
                            Console.ReadKey();
                        }
                            
                    }
                    newMerchant = new Merchant<MagicWeapon>(
                                    name: "Trader",
                                    isFriendly: true,
                                    description: $"Hello, i probably have what you need {player.Name}!"
                                    );
                    currentGameState = GameStates.prologue;
                    break;

                case GameStates.prologue:
                    Console.Clear();
                    GeneralGameFunctionality.DelayTextOutput("You wake up on a beach robbed of all your money and after some time you\n" +
                        "realise your squire is also gone! After some time sunken in you see a path leading into a rainy forest" +
                        "and decides to move further..");
                    SetCursorPos();
                    Console.WriteLine("Press 'ENTER' to Continue.");

                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        currentGameState = GameStates.main;
                        Console.Clear();
                    }
                    break;

                    // MAIN INGAME STATE
                case GameStates.main:
                    switch (currentLevelState)
                    {   
                        // Starting Level atm..
                        case LevelStates.forest:
                            DisplayHUD(currentLevelState, "City");
                            PlayerInput(LevelStates.city);
                            break;
                        case LevelStates.city:
                            // In the City
                            DisplayHUD(currentLevelState, "The Forest");
                            PlayerInput(LevelStates.forest);
                            break;
                        case LevelStates.tavern:
                            // IN THE TAVERN, CAN MEET NPC's
                            DisplayHUD(currentLevelState, "City");
                            PlayerInput(LevelStates.city);
                            break;
                        case LevelStates.mountainpass:
                                // In the mountains, watch out for Enemies here ..
                            DisplayHUD(currentLevelState, "City");
                            PlayerInput(LevelStates.city);
                            
                            break;
                        case LevelStates.snowyPeaks:
                            // UNDER DEVELOPMENT, LATER GAME CONTENT
                            break;
                        default:
                            break;
                    }
                    Console.Clear();
                    break;

                case GameStates.inventory:
                    SetCursorPos(width / 2, height / 2);
                    player.ShowInventory();
                    SetCursorPos();
                    player.SelectingWeapon();
                    break;

                case GameStates.interact:
                    newMerchant.Interact(player);
                    break;
                case GameStates.incombat:
                    Imp enemy = new Imp()
                    {
                        Name = "Fiery Imp",
                        Description = "A basic imp walking the border area around the pass",
                        Level = 2,
                        Power = 3,
                        MaxHealth = 75,
                        CurHealth = 75
                    };
                    player.Interaction(enemy);
                    break;
                case GameStates.exit:
                    Console.Beep();
                    Console.WriteLine("Exiting Program..");
                    break;
                default:
                    break;
                }
            }
    }
    // Methods --
    public static void SetCursorPos(int x=0, int y=(30-1))
    {
        // sets Cursor Position on screen..
        Console.SetCursorPosition(x, y);
    }
        // Main Screen HUD
    static void DisplayHUD(LevelStates curLocation, string prevLocation)
    {
            Console.Clear();
            SetCursorPos(Console.WindowWidth/2, Console.WindowHeight/ 4);
            // Where player are at the moment
            GeneralGameFunctionality.ChangeForegroundFontColor(ConsoleColor.Green);
            Console.WriteLine(curLocation.ToString().ToUpper());
            GeneralGameFunctionality.ChangeForegroundFontColor(standardForegroundColor);
            width = 5;
            height = 5;
            SetCursorPos(width, height);
            // Character Information displayed on-screen
            Console.WriteLine($"\n\n\nName: {player.Name.ToUpper()} \nLEVEL: {player.curLevel}\nClass: {player.Class.ToUpper()}" +
                $"\nHealth: {player.CurHealth}/{player.MaxHealth} \n{player.SpecialResource}: {player.CurSpecialResource}/" +
                $"{player.MaxSpecialResource}\nArmor: {player.Armor} \nGold: {player.Gold}\n{player.AttackType}: {player.PowerMax}");

            if (player.CurWeapon != null)
            {
                if (player.CurWeapon.FireDamage > 0)
                {
                    Console.WriteLine($" + (Fire): {player.CurWeapon.FireDamage}");
                }

                Console.WriteLine($"\nWeapon: {player.CurWeapon.Name}");
            }
            else
            {
                Console.WriteLine("NO WEAPON EQUIPPED!");
            }
            
            // Checking where the player are at the moment..

            if (currentLevelState == LevelStates.city)
            {
                // you can visit the Tavern ..
                Console.WriteLine("\n'T'.) Visit 'Tavern'!");

                // You can go to the MOUNTAIN-PASS
                Console.WriteLine("\n'M'.) Head To 'Mountain Pass'");
            }

            if (currentLevelState == LevelStates.mountainpass && currentGameState != GameStates.incombat)
            {
                GeneralGameFunctionality.DelayTextOutput("A Travelling knight is walking by, maybe you can ask him for some help concerning your injuries..");
                Thread.Sleep(4000);
            }
            if (currentLevelState == LevelStates.tavern)
            {
                // You can talk to merchants and npc's
                Console.WriteLine("\n'N'.) Talk to 'Merchant'");

                Console.WriteLine("\n'S' .) Talk to 'Silent Beggar'");
            }

            GeneralGameFunctionality.ChangeForegroundFontColor(ConsoleColor.DarkCyan);
            Console.WriteLine("\n\n");
            Console.WriteLine($"'I'.) INVENTORY");
            Console.WriteLine($"1.) Travel to {prevLocation}");   
            Console.WriteLine($"2.) Travel to Snowy Peaks");
            GeneralGameFunctionality.ChangeForegroundFontColor(standardForegroundColor);
    }
    
    static void PlayerInput(LevelStates prevLocation)
    {
            // MAIN PLAYER INPUT -->
        switch (Console.ReadKey(true).Key)
        {
            case ConsoleKey.I:
                currentGameState = GameStates.inventory;
                break;
            case ConsoleKey.T:
                currentLevelState = LevelStates.tavern;    
                break;
            case ConsoleKey.N:
                currentGameState = GameStates.interact;
                break;
            case ConsoleKey.S:
                Merchant<MagicWeapon> spellVendor = new Merchant<MagicWeapon>("Silent Beggar", true, "We have some spells");
                    spellVendor.CreateItems(count: 10);
                    spellVendor.ListItems();
                break;
            case ConsoleKey.M:
                currentLevelState = LevelStates.mountainpass;
                currentGameState = GameStates.incombat;
                break;
            case ConsoleKey.D1:
                currentLevelState = prevLocation;
                break;
            case ConsoleKey.D2:
                Console.Clear();
                SetCursorPos(width / 2, height / 2);
                GeneralGameFunctionality.DelayTextOutput("It's too dangerous going this path right now!");
                Console.ReadLine();
                break;
            default:
                break;
        }
    }
}
}


