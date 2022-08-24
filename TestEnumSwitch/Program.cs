// See https://aka.ms/new-console-template for more information

using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;

using System.Media;

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
            interact,
            exit
        }

        public enum LevelStates
        {
            forest,
            city,
            tavern,
            snowyPeaks
        }

        public static Merchant newMerchant;
        public static Mage newPlayer;
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
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.BackgroundColor = ConsoleColor.DarkGray;

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
                            newPlayer = new Mage(
                                        _class:"Mage",
                                        name:playerName,
                                        power:7,
                                        armor:3,
                                        gold:250,
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
                    newMerchant = new Merchant(
                                    name: "Trader",
                                    isFriendly: true,
                                    description: $"Hello, i probably have what you need {newPlayer.Name}!"
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
                        currentGameState = GameStates.ingame;
                        Console.Clear();
                    }
                    break;

                    // INGAME
                case GameStates.ingame:
                    switch (currentLevelState)
                    {   
                        case LevelStates.forest:
                            Console.Clear();
                            DisplayHUD("THE FOREST", "City");
                            PlayerInput(LevelStates.city);
                            break;
                        case LevelStates.city:
                            // In the City
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
                    newPlayer.ShowInventory();
                    SetCursorPos();
                    newPlayer.EquipItem();
                    break;

                case GameStates.interact:
                    newMerchant.Interact(newPlayer);
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
    static void DisplayHUD(string curLocation, string prevLocation)
    {
            SetCursorPos(width / 2, height);
            width = 5;
            Console.WriteLine(curLocation);
            SetCursorPos(width, height);
            
            // Character Information displayed on-screen
            Console.WriteLine($"Name: {newPlayer.Name.ToUpper()} \nLEVEL: {newPlayer.curLevel}\nClass: {newPlayer.Class.ToUpper()}" +
                $"\nHealth: {newPlayer.CurHealth}/{newPlayer.MaxHealth} \n{newPlayer.SpecialResource}: {newPlayer.CurSpecialResource}/" +
                $"{newPlayer.MaxSpecialResource}\nArmor: {newPlayer.Armor} \n{newPlayer.AttackType}: {newPlayer.Power}\nGold: {newPlayer.Gold}");

            if (newPlayer.CurWeapon != null)
            {
                Console.WriteLine($"Weapon: {newPlayer.CurWeapon.Name}");
            }
            else
            {
                Console.WriteLine("NO WEAPON EQUIPPED!");
            }
            
            // Checking where the player are at the moment..

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


