using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestEnumSwitch
{
    public static class EnemyPlayerInteraction
    {
        static bool inCombat = true;

        // Extension methods working with player / Enemy interaction..
        public static void Interaction(this Mage player, Enemy enemy)
        {
            do
            {
                if (player.CurHealth == player.MaxHealth && enemy.CurHealth == enemy.MaxHealth)
                {
                    GeneralGameFunctionality.DelayTextOutput($"You meet a {enemy.Name} (lv){enemy.Level}\n\n'{enemy.Description}'\n\n\n\nAttack ('Y' / 'N')");
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine($"{enemy.Name} (lv){enemy.Level} Health {enemy.CurHealth}");
                    Console.WriteLine($"{player.Name} (lv){player.curLevel} Health {player.CurHealth}");
                    Console.WriteLine("Attack ('Y' / 'N'");

                }
                if (player.CurHealth <= 0 || enemy.CurHealth <= 0)
                {
                    Console.Beep();
                    if (player.CurHealth <= 0)
                    {
                        Console.WriteLine("You DIed! Game-Over ..");
                        Thread.Sleep(2500);
                        Program.currentGameState = Program.GameStates.exit;
                        break;
                    }
                    else if (enemy.CurHealth <= 0)
                    {
                        Console.WriteLine("VICTORY..");
                        Thread.Sleep(2500);
                        Program.currentGameState = Program.GameStates.main;
                        break;
                    }
                }
                if (Console.ReadKey(true).Key == ConsoleKey.Y)
                {
                    // Go Attack Enemy ..
                    PlayerHitEnemy(player, enemy);
                    EnemyHitPlayer(player, enemy);
                }
                else if (Console.ReadKey(true).Key == ConsoleKey.N)
                {
                    inCombat = false;
                    // surrender
                    Surrender();
                    // Change GameState back to Main again
                }
            } while (inCombat);
            
        }
        public static void PlayerHitEnemy(this Mage mage, Enemy enemy)
        {
            Console.Beep();
            enemy.CurHealth -= mage.PowerMax * mage.curLevel;
            Console.WriteLine($"{enemy.Name} got attacked and lost {mage.PowerMax * mage.curLevel} health!");
            Thread.Sleep(2500);
        }

        public static void EnemyHitPlayer(this Mage mage, Enemy enemy)
        {
            Console.Beep();
            mage.CurHealth -= enemy.Power * enemy.Level;
            Console.WriteLine($"{mage.Name} got attacked and lost {enemy.Power * enemy.Level}");
            Thread.Sleep(2500);
        }

        private static void Surrender()
        {
            GeneralGameFunctionality.DelayTextOutput("You Surrender ..");
            Thread.Sleep(2500);
            Program.currentGameState = Program.GameStates.main;
        }
    }
}
