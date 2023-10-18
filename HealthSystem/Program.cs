using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HealthSystem
{
    internal class Program
    {
        static int health;
        static string healthStatus;
        static int shield;
        static int lives;
        static int level;
        static int xp;
        static void Main(string[] args)
        {

            health = 100;
            shield = 100;
            lives = 3;
            level = 1;
            
            UnitTestHealthSystem();
            UnitTestXPSystem();

            ShowHud();
            TakeDamage(110);
            ShowHud();

            Console.ReadKey(true);
        }

        static void TakeDamage(int damage)
        {
            if (damage > 0)
            {

                if (shield > 0)
                {
                    shield -= damage;

                    if (shield < 0)
                    {
                        health += shield;
                        shield = 0;
                    }

                }
                else
                {
                    health -= damage;
                }
                if (health < 0)
                {
                    health = 0;
                }

            }

        }

        static void Revive()
        {
            if (health < 1)
            {
                shield = 100;
                health = 100;
                lives = lives - 1;
            }
            if (lives < 1)
            {
                lives = 0;
                health = 0;
                shield = 0;
            }
        }

        static void Heal(int hp)
        {
            if (hp > 0)
            {
                if (health < 100)
                {
                    health += hp;
                }
                if (health >= 100)
                {
                    health = 100;
                }
            }
        }

        static void RegenerateShield(int shp)
        {
            if (shp > 0)
            {
                if (shield < 100)
                {
                    shield += shp;
                }
                if (shield >= 100)
                {
                    shield = 100;
                }
            }
        }

        static void IncreaseXP(int lvl)
        {
            xp += lvl;
            if (xp > 100)
            {
                xp -= 100;
                level += 1 + (xp / 100);
            }
        }

        static void ShowHud()
        {
            Console.WriteLine();
            Console.WriteLine("Level: " + level + " XP: " + xp + "% ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine(" Shield: " + shield + "% ");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(" Health: " + health + "% ");
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(" Lives: " + lives + " ");
            Console.ResetColor();

            Console.WriteLine();
        }
        static void UnitTestHealthSystem()
        {
            Debug.WriteLine("Unit testing Health System started...");

            // TakeDamage()

            // TakeDamage() - only shield
            shield = 100;
            health = 100;
            lives = 3;
            TakeDamage(10);
            Debug.Assert(shield == 90);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // TakeDamage() - shield and health
            shield = 10;
            health = 100;
            lives = 3;
            TakeDamage(50);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 60);
            Debug.Assert(lives == 3);

            // TakeDamage() - only health
            shield = 0;
            health = 50;
            lives = 3;
            TakeDamage(10);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 40);
            Debug.Assert(lives == 3);

            // TakeDamage() - health and lives
            shield = 0;
            health = 10;
            lives = 3;
            TakeDamage(25);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 0);
            Debug.Assert(lives == 3);

            // TakeDamage() - shield, health, and lives
            shield = 5;
            health = 100;
            lives = 3;
            TakeDamage(110);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 0);
            Debug.Assert(lives == 3);

            // TakeDamage() - negative input
            shield = 50;
            health = 50;
            lives = 3;
            TakeDamage(-10);
            Debug.Assert(shield == 50);
            Debug.Assert(health == 50);
            Debug.Assert(lives == 3);

            // Heal()

            // Heal() - normal
            shield = 0;
            health = 90;
            lives = 3;
            Heal(5);
            Debug.Assert(shield == 0);
            Debug.Assert(health == 95);
            Debug.Assert(lives == 3);

            // Heal() - already max health
            shield = 90;
            health = 100;
            lives = 3;
            Heal(5);
            Debug.Assert(shield == 90);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // Heal() - negative input
            shield = 50;
            health = 50;
            lives = 3;
            Heal(-10);
            Debug.Assert(shield == 50);
            Debug.Assert(health == 50);
            Debug.Assert(lives == 3);

            // RegenerateShield()

            // RegenerateShield() - normal
            shield = 50;
            health = 100;
            lives = 3;
            RegenerateShield(10);
            Debug.Assert(shield == 60);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // RegenerateShield() - already max shield
            shield = 100;
            health = 100;
            lives = 3;
            RegenerateShield(10);
            Debug.Assert(shield == 100);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 3);

            // RegenerateShield() - negative input
            shield = 50;
            health = 50;
            lives = 3;
            RegenerateShield(-10);
            Debug.Assert(shield == 50);
            Debug.Assert(health == 50);
            Debug.Assert(lives == 3);

            // Revive()

            // Revive()
            shield = 0;
            health = 0;
            lives = 2;
            Revive();
            Debug.Assert(shield == 100);
            Debug.Assert(health == 100);
            Debug.Assert(lives == 1);

            Debug.WriteLine("Unit testing Health System completed.");
            Console.Clear();
        }

        static void UnitTestXPSystem()
        {
            Debug.WriteLine("Unit testing XP / Level Up System started...");

            // IncreaseXP()

            // IncreaseXP() - no level up; remain at level 1
            xp = 0;
            level = 1;
            IncreaseXP(10);
            Debug.Assert(xp == 10);
            Debug.Assert(level == 1);

            // IncreaseXP() - level up to level 2 (costs 100 xp)
            xp = 0;
            level = 1;
            IncreaseXP(105);
            Debug.Assert(xp == 5);
            Debug.Assert(level == 2);

            // IncreaseXP() - level up to level 3 (costs 200 xp)
            xp = 0;
            level = 2;
            IncreaseXP(210);
            Debug.Assert(xp == 10);
            Debug.Assert(level == 3);

            // IncreaseXP() - level up to level 4 (costs 300 xp)
            xp = 0;
            level = 3;
            IncreaseXP(315);
            Debug.Assert(xp == 15);
            Debug.Assert(level == 4);

            // IncreaseXP() - level up to level 5 (costs 400 xp)
            xp = 0;
            level = 4;
            IncreaseXP(499);
            Debug.Assert(xp == 99);
            Debug.Assert(level == 5);

            Debug.WriteLine("Unit testing XP / Level Up System completed.");
            Console.Clear();
        }
    }
}
