// See https://aka.ms/new-console-template for more information

using System.Reflection.Emit;
using System.Reflection.PortableExecutable;

namespace TestEnumSwitch
{
    class Program
    {
        public enum GameStates
        {
            start,
            charcreation,
            prologue,
            ingame,
            inventory,
            exit
        }

        public enum LevelStates
        {
            forest,
            city,
            tavern,
            snowyPeaks
        }

        static bool chooseClass = true;
        static Character newPlayer;
        static int width = Console.WindowWidth;
        static int height = Console.WindowHeight;
        static string location = "";

        // enums
        public static GameStates currentGameState;
        public static LevelStates currentLevelState;

    static void Main(string[] args)
    {
        currentGameState = GameStates.start;
        currentLevelState = LevelStates.forest;
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.BackgroundColor = ConsoleColor.DarkGray;
        
            while (currentGameState != GameStates.exit)
        {
                // MAIN GAME-LOOP!!
                switch (currentGameState)
                {
                    case GameStates.start:
                        SetCursorPos(width / 2, height / 2);
                        Console.WriteLine("A HERO'S REQUEST");
                        SetCursorPos(0, 0);
                        Console.WriteLine("Press 'ENTER'");
                        if (Console.ReadKey().Key == ConsoleKey.Enter)
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
                            if (Console.ReadKey().Key == ConsoleKey.D1)
                            {
                                newPlayer = new Character("Mage", playerName, 6, 4, 50, 100, "Bent Scepter", "Cloth Armor", "Mana", "Arcane Elixir");
                                chooseClass = false;
                            }
                            else if (Console.ReadKey().Key == ConsoleKey.D2)
                            {
                                newPlayer = new Character("Ranger", playerName, 10, 8, 50, 100, "Scrap Bow", "Leather Armor", "Energy", "Dexterity Booster");
                                chooseClass = false;
                            }
                            else
                            {
                                Console.WriteLine("\nInvalid Key, choose a Class");
                                Console.ReadKey();
                            }
                        }
                        currentGameState = GameStates.prologue;
                        break;

                    case GameStates.prologue:
                        Console.Clear();
                        Console.WriteLine("\nYou wake up on a beach robbed of all your money and after some time you\n" +
                            "realise your squire is also gone! After some time sunken in you see a path leading into a rainy forest" +
                            "and decides to move further..");
                        SetCursorPos();
                        Console.WriteLine("Press 'ENTER' to Continue.");

                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                        {
                            currentGameState = GameStates.ingame;
                            Console.Clear();
                        }
                        break;
                    case GameStates.ingame:
                        switch (currentLevelState)
                        {   
                            case LevelStates.forest:
                                Console.Clear();
                                DisplayHUD("THE FOREST", "City");
                                PlayerInput(LevelStates.city);
                                break;
                            case LevelStates.city:
                                Console.Clear();
                                DisplayHUD("CITY", "The Forest");
                                PlayerInput(LevelStates.forest);
                                break;
                            case LevelStates.tavern:
                                Console.Clear();
                                // IN THE TAVERN, CAN MEET NPC's
                                DisplayHUD("THE TAVERN", "City");
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
                        Console.WriteLine($"INVENTORY\n\n\n");
                        newPlayer.ShowInventory();
                        // resetting cursor pos ..
                        SetCursorPos();
                        newPlayer.EquipItem();
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
    // Method Declarations
    public static void SetCursorPos(int x=0, int y= (30-1))
    {
        // sets Cursor Position on screen..
        Console.SetCursorPosition(x, y);
    }
    static void DisplayHUD(string curLocation, string prevLocation)
    {
            // CODE HERE .-
            height = 3;
            SetCursorPos(width / 2, height);
            Console.WriteLine(curLocation);
            width = 0;
            height = 5;
            SetCursorPos(width, height);
            Console.WriteLine($"Name: {newPlayer.name.ToUpper()} \nLEVEL: {newPlayer.curLevel}\nClass: {newPlayer._class.ToUpper()}" +
                $"\nHealth: {newPlayer.curHealth}/{newPlayer.maxHealth} \n{newPlayer.specialResource}: {newPlayer.curSpecialResource}/" +
                $"{newPlayer.maxSpecialResource} \nWeapon: {newPlayer.curWeapon}" +
                $"\nArmor: {newPlayer.armor} \nGold: {newPlayer.gold}");
            newPlayer.GetPowerPairs();

            if (currentLevelState == LevelStates.city)
            {
                // you can visit the Tavern ..
                Console.WriteLine("\n'T'.) Visit Tavern!");
            }

            if (currentLevelState == LevelStates.tavern)
            {
                // You can talk to merchants and npc's
                Console.WriteLine("\'N'.) Talk to Merchant");
            }
            Console.WriteLine("\n\n");
            Console.WriteLine($"'I'.) INVENTORY");
            Console.WriteLine($"1.) Travel to {prevLocation}");   
            Console.WriteLine($"2.) Travel to Snowy Peaks");
    }
    
    static void PlayerInput(LevelStates prevLocation)
    {
            // CODE HERE -->
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.I:
                currentGameState = GameStates.inventory;
                break;
            case ConsoleKey.T:
                currentLevelState = LevelStates.tavern;    
                break;
            case ConsoleKey.N:
                Merchant merchant = new Merchant("Trader", true, $"Hello, i probably have what you need {newPlayer.name}!");
                merchant.Interact(newPlayer);
                break;
            case ConsoleKey.D1:
                currentLevelState = prevLocation;
                break;
            case ConsoleKey.D2:
                Console.Clear();
                SetCursorPos(width / 2, height / 2);
                Console.WriteLine("It's too dangerous going this path right now..");
                Console.ReadLine();
                break;
            default:
                break;
        }

    }
}
}


