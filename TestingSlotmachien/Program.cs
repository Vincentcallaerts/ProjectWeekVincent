using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSlotmachien
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Random random = new Random();
            string[,] slotMachien = { { "♥", "♣", "♦", "♠", "A", "B", "7" }, { "♥", "♣", "♦", "♠", "A", "B", "7" }, { "♥", "♣", "♦", "♠", "A", "B", "7" } };
            bool runningSlots = true;
            int keuze;
            PrintSlotMachien(slotMachien);
            while (runningSlots)
            {
                Console.WriteLine("1. Spin");
                Console.WriteLine("1. Stop");
                keuze = InputIntKeuze(2);
                switch (keuze)
                {
                    case 1:
                        for (int i = 0; i < 20; i++)
                        {
                            PrintSlotMachien(slotMachien);
                            
                        }
                        Console.WriteLine($"Je winst is in het totaal: {WinstHorizontaal(slotMachien) + " " + WinstDiagonaal(slotMachien)} = {WinstDiagonaal(slotMachien)+WinstHorizontaal(slotMachien)}");
                        break;
                    case 2:

                        break;
                    
                }                             
            }          
        }

        

        static void PrintSlotMachien(string[,] slotMachien) 
        {
            Console.Clear();
            Console.WriteLine("===============");
            
            for (int i = 0; i < 3; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("$$$");
                Console.ResetColor();
                for (int j = 0; j < 3; j++)
                {
                    if (j == 0)
                    {
                        Console.ResetColor();
                    }
                    Console.Write("|");
                    switch (slotMachien[i, j])
                    {

                        case "♥":
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"{slotMachien[i, j]}");
                            break;

                        case "♣":
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"{slotMachien[i, j]}");
                            break;

                        case "♦":
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write($"{slotMachien[i, j]}");
                            break;

                        case "♠":
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write($"{slotMachien[i, j]}");
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write($"{slotMachien[i, j]}");
                            break;
                    }
                    if (j == 2)
                    {
                        Console.ResetColor();
                    }
                    Console.Write("|");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("$$$");
                Console.ResetColor();
                Console.WriteLine();
            }
            Console.ResetColor();
            Console.WriteLine("===============");
            System.Threading.Thread.Sleep(200);
            
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
        static string[,] Draai(string[,] slotMachien, Random random)
        {
            string[,] temp = slotMachien;
            int temprandom;
            string tempstorage = string.Empty;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    temprandom = random.Next(0, 7);
                    tempstorage = temp[0, i];
                    temp[0, i] = temp[i, temprandom];
                    temp[i, temprandom] = tempstorage;
                }              

            }
            return temp;
        }
        static int WinstHorizontaal(string[,] slotMachien) 
        {
            //"♥" = 1, "♣" = 2, "♦" = 3, "♠" = 4,"A" = 5,"B" = 6,"7" = 7
            int winst = 0;
            for (int i = 0; i < slotMachien.Length/7; i++)
            {
                if (slotMachien[i,0] == slotMachien[i,1] && slotMachien[i, 2] == slotMachien[i, 1])
                {
                    switch (slotMachien[i,0])
                    {
                        case "♥":
                            winst++;
                            break;

                        case "♣":
                            winst += 2;
                            break;

                        case "♦":
                            winst += 3;
                            break;

                        case "♠":
                            winst += 4;
                            break;
                        case "A":
                            winst += 5;
                            break;

                        case "B":
                            winst += 6;
                            break;

                        case "7":
                            winst += 7;
                            break;

                    }
                }   
            }
            return winst;
        }
        private static int WinstDiagonaal(string[,] slotMachien)
        {
            //"♥" = 1, "♣" = 2, "♦" = 3, "♠" = 4,"A" = 5,"B" = 6,"7" = 7
            int winst = 0;

            if (slotMachien[0, 0] == slotMachien[1, 1] && slotMachien[1, 1] == slotMachien[2, 2])
            {
                switch (slotMachien[0, 0])
                {
                    case "♥":
                        winst++;
                        break;

                    case "♣":
                        winst += 2;
                        break;

                    case "♦":
                        winst += 3;
                        break;

                    case "♠":
                        winst += 4;
                        break;
                    case "A":
                        winst += 5;
                        break;

                    case "B":
                        winst += 6;
                        break;

                    case "7":
                        winst += 7;
                        break;

                }
            }
            if (slotMachien[2, 0] == slotMachien[1, 1] && slotMachien[1, 1] == slotMachien[0, 2])
            {
                switch (slotMachien[2, 0])
                {
                    case "♥":
                        winst++;
                        break;

                    case "♣":
                        winst += 2;
                        break;

                    case "♦":
                        winst += 3;
                        break;

                    case "♠":
                        winst += 4;
                        break;
                    case "A":
                        winst += 5;
                        break;

                    case "B":
                        winst += 6;
                        break;

                    case "7":
                        winst += 7;
                        break;

                }
            }
            return winst;
        }

    }
}
