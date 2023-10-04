using System;
using System.Collections.Generic;
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

        static void Main(string[] args)
        {
            health = 100;
            shield = 100;
            lives = 3;

            ShowHud();
            TakeDamage(10);
            ShowHud();
            TakeDamage(30);
            Heal(10);
            ShowHud();
            TakeDamage(55);
            ShowHud();
            TakeDamage(90);
            ShowHud();
            TakeDamage(300);
            ShowHud(); 
            TakeDamage(300);
            ShowHud();

            Console.ReadKey(true);
        }

        static void TakeDamage(int damage)
        {

            if (shield >= 0)
            {
                shield = shield - damage;
            }

            if (shield <= 0)
            {
                shield = 0;
                health = health - damage;

            } 


            if (health < 1)
            {
                Console.WriteLine("You died");

                lives = lives - 1;

                health = 100;
                shield = 100;

                if (lives <= 0)
                {
                    Console.WriteLine("GAME OVER");
                    health = 0;
                    shield = 0;
                }


            }

        }

        static void Heal(int hp)
        {

        }

        static void RegenerateShield(int hp)
        {

        }

        static void ShowHud()
        {
            Console.WriteLine();
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
    }
}
