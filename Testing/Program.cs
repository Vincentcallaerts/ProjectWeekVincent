using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = System.Text.Encoding.Unicode;
         
            string[,] kaartenboek = { { "A♥", "2♥", "3♥", "4♥", "5♥", "6♥", "7♥", "8♥", "9♥", "10♥", "J♥", "Q♥", "K♥" }, { "A♦", "2♦", "3♦", "4♦", "5♦", "6♦", "7♦", "8♦", "9♦", "10♦", "J♦", "Q♦", "K♦" }, { "A♣", "2♣", "3♣", "4♣", "5♣", "6♣", "7♣", "8♣", "9♣", "10♣", "J♣", "Q♣", "K♣" }, { "A♠", "2♠", "3♠", "4♠", "5♠", "6♠", "7♠", "8♠", "9♠", "10♠", "J♠", "Q♠", "K♠" } };
            
            
            int keuze;
            bool running = true;
            bool runninggame = false;
            Random random = new Random();

            while (running)
            {
                string[] handSpeler = new string[10];
                string[] handDealer = new string[10];
                Console.WriteLine("1. Speel Spel");
                Console.WriteLine("2. Stop");
                keuze = InputIntKeuze(2);
                switch (keuze)
                {
                    case 1:
                        Console.Clear();
                        TrekKaarten(handSpeler, kaartenboek, random);
                        TrekKaarten(handSpeler, kaartenboek, random);
                        LaatBoekKaartenAfdrukken(handSpeler);
                        Console.WriteLine(WaardeHand(handSpeler));

                        runninggame = true;
                        while (runninggame)
                        {
                            Console.WriteLine();
                            Console.WriteLine("1. Trek kaart");
                            Console.WriteLine("2. Stop");
                            keuze = InputIntKeuze(2);
                            switch (keuze)
                            {
                                case 1:
                                    
                                    TrekKaarten(handSpeler, kaartenboek, random);
                                    LaatBoekKaartenAfdrukken(handSpeler);
                                    if (WaardeHand(handSpeler) == 21)
                                    {
                                        Console.WriteLine("Je hebt gewonnen");
                                    }
                                    if (WaardeHand(handSpeler) == 21)
                                    {
                                        Console.WriteLine("Je hebt gewonnen woohoow");
                                        runninggame = false;
                                    }
                                    else if (WaardeHand(handSpeler) >= 21)
                                    {
                                        Console.WriteLine("Je hebt verloren woohoow");
                                        runninggame = false;

                                    }
                                    break;
                                case 2:
                                    if (WaardeHand(handSpeler) == 21)
                                    {
                                        Console.WriteLine("Je hebt gewonnen");
                                    }
                                    else
                                    {
                                        TrekKaarten(handDealer, kaartenboek, random);
                                        TrekKaarten(handDealer, kaartenboek, random);
                                        while (WaardeHand(handDealer) <= 17)
                                        {
                                            TrekKaarten(handDealer, kaartenboek, random);
                                        }

                                        if (WaardeHand(handDealer) >= 21)
                                        {
                                            Console.WriteLine("Je hebt gewonnen woohoow");
                                            runninggame = false;

                                        }
                                        else if (WaardeHand(handDealer) > WaardeHand(handSpeler))
                                        {
                                            Console.WriteLine("Je hebt verloren woohoow");
                                            runninggame = false;
                                        }
                                        else if (WaardeHand(handDealer) == 21)
                                        {
                                            Console.WriteLine("Je hebt verloren woohoow");
                                            runninggame = false;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Je hebt gewonnen woohoow");
                                            runninggame = false;
                                        }
                                    }
                                    
                                    

                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case 2: running = false;
                        break;
                    default:
                        break;
                }
            };           
        }

        static void TrekKaarten(string[] hand, string[,] kaartenboek,Random random)
        {

            int srandom = random.Next(0, 13);
            int frandom = random.Next(0, 4);
            

            if (CheckEmptySpot(hand) == -1)
            {
                Console.WriteLine("Spijtig je Hand zit vol");
            }
            else
            {
               
                hand[CheckEmptySpot(hand)] = kaartenboek[frandom, srandom];
                
            }
            
        }
        static void LaatBoekKaartenAfdrukken(string[,] kaartenboek)
        {
            for (int i = 0; i < kaartenboek.Length / 13; i++)
            {
                for (int j = 0; j < kaartenboek.Length / 4; j++)
                {
                    Console.Write(kaartenboek[i, j] + " ");
                }
            }
            
        }
        static void LaatBoekKaartenAfdrukken(string[] hand)
        {
            for (int i = 0; i < hand.Length; i++)
            {
               
                Console.Write(hand[i] + " ");
                
            }
        }
        
        static int CheckEmptySpot(string[] hand)
        {
            int emptySpot = -1;


            for (int i = 0; i < hand.Length; i++)
            {
                if (hand[i] == null)
                {
                    emptySpot = i;
                    return emptySpot;
                }
            }
            return emptySpot;

        }
        static int WaardeHand(string[] hand)
        {
            int temper;
            int waarde = 0;

            string temp = string.Empty;

            for (int i = 0; i < CheckEmptySpot(hand); i++)
            {
                temp = hand[i].Substring(0, hand[i].Length-1);
                if (int.TryParse(temp, out temper))
                {
                    waarde += Convert.ToInt32(temp);
                }
                else if(temp == "A" || temp == "J" || temp == "Q" || temp == "K")
                {
                    switch (temp)
                    {
                        case "A":
                            if (waarde + 11 > 21)
                            {
                                waarde++;
                            }
                            else
                            {
                                waarde += 11;
                            }
                            break;
                        case "J":                          
                        case "Q":                           
                        case "K":
                            waarde += 10;
                            break;
                        default:
                            break;
                    }
                }                
            }
            return waarde;
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
                if (aantal >= 500)
                {
                    Console.WriteLine("Voer een nieuw buget in Max van 500 alles erboven moet je zelf verdienen");
                    Console.Write("Nieuw buget: ");
                    return InputIntKeuze(aantal);
                }
                else
                {
                    Console.Write($"Dat was geen Juiste input er bestaat geen {Convert.ToInt32(getal)}, probeer het nog eens:");
                    return InputIntKeuze(aantal);
                }

            }
            else
            {
                Console.Write($"Dat was geen Juiste input er bestaat geen {getal}, probeer het nog eens:");
                return InputIntKeuze(aantal);
            }
        }

    }
}
