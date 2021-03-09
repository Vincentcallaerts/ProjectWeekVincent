using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingMemory
{
    class Program
    {
        static void Main(string[] args)
        {
            bool runningMemory = true;
            int keuze;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            string[] memory = { "♥", "♥", "♦", "♦", "♣", "♣", "♠", "♠" };
            string[] oplossing = new string[8];
            Random random = new Random();

            while (runningMemory)
            {
                Console.WriteLine("1. Speel Spel");
                Console.WriteLine("2. Stop");
                keuze = InputIntKeuze(2);
                Console.Clear();
                switch (keuze)
                {
                    case 1:

                        oplossing = MaakMemory(memory, random);
                        LeesMemory(oplossing);
                        
                        Console.WriteLine("Geef nu de tekens in de juiste volgorde in:");
                        Console.WriteLine("01234567");
                        string antwoordSpeler = Console.ReadLine();

                        if (AntwoordJuist(oplossing,antwoordSpeler))
                        {
                            Console.WriteLine("Spel Gewonnen");
                            
                        }
                        else
                        {
                            Console.WriteLine("Spel verloren");
                            
                        }

                        break;
                    case 2:
                        runningMemory = false;
                        break;
                    default:
                        break;
                }
            }
        }
        static int InputIntKeuze(int aantal)
        {

            int keuze;
            string getal = Console.ReadLine();

            if (int.TryParse(getal, out keuze))
            {
                if (0 < Convert.ToInt32(keuze) && Convert.ToInt32(keuze) <= aantal)
                {
                    keuze = Convert.ToInt32(getal);
                    return keuze;
                }
                else
                {
                    Console.Write($"Dat was geen Juiste input {getal} mag niet als waarde, probeer het nog eens:");
                    return InputIntKeuze(aantal);
                }

            }
            else
            {
                Console.Write($"Dat was geen Juiste input er bestaat geen {getal}, probeer het nog eens:");
                return InputIntKeuze(aantal);
            }
        }
        static string[] MaakMemory(string[] memory, Random random)
        {

            string[] temp = new string[8];
            for (int i = 0; i < memory.Length; i++)
            {
                temp[i] = memory[random.Next(0, 8)];
            }
            return temp;

        }
        static void LeesMemory(string[] oplossing)
        {

            Console.Clear();
            Console.WriteLine("0|1|2|3|4|5|6|7|");

            for (int i = 0; i < oplossing.Length; i++)
            {
                switch (oplossing[i])
                {
                    case "♥":
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(oplossing[i]+ " ");
                        break;
                    case "♦":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(oplossing[i] + " ");
                        break;
                    case "♣":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(oplossing[i] + " ");
                        break;
                    case "♠":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(oplossing[i]+ " ");
                        break;

                }
            }

            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Je krijgt nu 15 seconden dit te onthouden.");
            System.Threading.Thread.Sleep(10000);
            Console.Clear();

            Console.WriteLine("Gebruik 1 voor ♥");
            Console.WriteLine("Gebruik 2 voor ♦");
            Console.WriteLine("Gebruik 3 voor ♣");
            Console.WriteLine("Gebruik 4 voor ♠");

        }
        static bool AntwoordJuist(string[] oplossing, string antwoord) 
        {
            for (int i = 0; i < oplossing.Length; i++)
            {
                switch (antwoord.ElementAt(i))
                {

                    case '1':
                        if ("♥" != oplossing[i])
                        {
                            return false;
                        }
                        break;

                    case '2':
                        if ("♦" != oplossing[i])
                        {
                            return false;
                        }
                        break;

                    case '3':
                        if ("♣" != oplossing[i])
                        {
                            return false;
                        }
                        break;

                    case '4':
                        if ("♠" != oplossing[i])
                        {
                            return false;
                        }
                        break;

                }
            }
            return true;
        }
    }
}
