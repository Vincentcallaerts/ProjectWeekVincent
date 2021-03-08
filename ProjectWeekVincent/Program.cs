﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ProjectWeekVincent
{
    class Program
    {
        static void Main(string[] args)
        {
            bool loggedIn = false;
            bool running = true;

            string bestandsnaam = "gegevens.txt";
            string gebruikersNaam = String.Empty;
            string[,] gebruikers = new string[6, 3];
            DateTime timeLoggedIn = DateTime.Now;


            int keuze;
            int gebruiker = 0;
            int counter = 0;
            int laatsteKeuze = 0;


            //gebruiker maken volgende stap.

            while (running)
            {
                
                if (loggedIn == false)
                {
                    Console.Clear();
                  
                    ReadUsersInUsers(bestandsnaam, gebruikers);
                    LaatGebruikersZien(gebruikers);
                    Console.WriteLine("1. Gebruiker toevoegen");
                    Console.WriteLine("2. Gebruiker bewerken");
                    Console.WriteLine("3. Gebruiker verwijderen");
                    Console.WriteLine("4. Inloggen");
                    Console.WriteLine("5. Stop");
                    
                    Console.WriteLine();
                    if (laatsteKeuze == 4)
                    {
                        Console.WriteLine("Wij hebben niets met die gegevens in onze databank je kan dus niet inloggen :( ");
                    }
                    Console.Write("Input Je Keuze:");
                    keuze = InputIntKeuze(5);

                    switch (keuze)
                    {

                        case 1:

                            laatsteKeuze = 1;
                            Console.Clear();
                            maakGebruiker(gebruikers,bestandsnaam);
                            break;

                        case 2:

                            laatsteKeuze = 2;
                            Console.Clear();
                            GebruikerBewerken(gebruikers,bestandsnaam);
                            break;

                        case 3:

                            laatsteKeuze = 3;
                            Console.Clear();
                            GebruikerVerwijderen(gebruikers, bestandsnaam);
                            break;

                        case 4:
                           
                            Console.Clear();
                            laatsteKeuze = 4;
                            Console.Write("Geef uw Gebruikersnaam in: ");
                            string temp = Console.ReadLine().ToLower();

                            for (int i = 0; i < gebruikers.Length / 6; i++)
                            {
                                if (temp == gebruikers[i, 0])
                                {
                                   
                                    Console.Write("Geef uw Wachtwoord in: ");
                                    temp = Console.ReadLine();
                                    if (EncrypteWachtwoord(temp) == gebruikers[i, 1])
                                    {
                                        gebruiker = i;
                                        loggedIn = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Gebruikersnaam en Wachtwoord komen niet overeen");
                                    }
                                }
                            }
                            break;
                        case 5:

                            SlaagGebruikersOp(gebruikers, bestandsnaam);
                            running = false;
                            break;

                        default: Console.Write("Dat was geen juiste input, Probeer opnieuw: "); break;
                    }

                }
                else
                {
                    Console.Clear();
                    if (counter == 0)
                    {
                        Console.WriteLine($"Hallo {gebruikers[gebruiker, 0]}\nHet is vandaag {timeLoggedIn.ToString("dd/MM/yyyy")}\nJe Bent om {timeLoggedIn.ToString("HH:mm")} gestart met het programma");
                        counter = 1;
                    }
                    else
                    {
                        
                        Console.WriteLine($"Hallo {gebruikers[gebruiker, 0]}\nHet is vandaag {timeLoggedIn.ToString("dd/MM/yyyy")}\nJe Bent al {DateTime.Now.Minute - timeLoggedIn.Minute} min ingelogt");
                    }
                    
                   
                    //hieraan beginnen
                    Console.WriteLine("1. Blackjack: ");
                    Console.WriteLine("2. Slotmachien: ");
                    Console.WriteLine("3. Memorie: ");
                    Console.WriteLine("4. Uitloggen: ");
                    keuze = InputIntKeuze(4);

                    switch (keuze)
                    {
                        case 1:
                            Console.WriteLine("1. Blackjack: ");
                            break;
                        case 2:
                            Console.WriteLine("2. Slotmachien: ");
                            break;
                        case 3:
                            Console.WriteLine("3. Memorie: ");
                            break;
                        case 4:
                            Console.WriteLine("4. Uitloggen: ");
                            loggedIn = false;
                            break;
                        default:
                            break;
                    }
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
        static void ReadUsersInUsers(string bestandsnaam, string[,] gebruikers)
        {

            if (File.Exists(bestandsnaam))
            {
                using (StreamReader reader = new StreamReader(bestandsnaam))
                {
                    while (!reader.EndOfStream)
                    {
                        for (int i = 0; i <= gebruikers.Length / 3; i++)
                        {

                            string gebruikerInfo = reader.ReadLine();
                            if (gebruikerInfo != null)
                            {
                                string[] gebruikersInfoSplit = gebruikerInfo.Split(',');
                                gebruikers[i, 0] = gebruikersInfoSplit[0];
                                gebruikers[i, 1] = gebruikersInfoSplit[1];
                                gebruikers[i, 2] = gebruikersInfoSplit[2];

                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Er werd geen locaal bestand gevonden");
                Console.WriteLine($"We maken een bestand aan dat {bestandsnaam} heet.");

                using (StreamWriter writer = new StreamWriter(bestandsnaam))
                {

                    maakGebruiker(gebruikers,bestandsnaam);

                }
                Console.WriteLine("Gegevens zijn weggeschreven.");
            }


        }
        static void LaatGebruikersZien(string[,] gebruikers)
        {
            for (int i = 0; i < gebruikers.Length / 3; i++)
            {
               
                if (gebruikers[i, 0] != null)
                {
                   

                    for (int j = 0; j < gebruikers.Length / 6; j++)
                    {
                        if (j == 0)
                        {
                            Console.Write($"Username: {gebruikers[i, j]} ");
                        }
                        if (j == 1)
                        {
                            Console.Write($"Wachtwoord: {gebruikers[i, j]} ");
                        }
                        if (j == 2)
                        {
                            Console.WriteLine($"Buget: {gebruikers[i, j]} ");
                        }


                    }
                    
                }
            }

        }
        static void LaatGebruikersNaamZien(string[,] gebruikers)
        {
            for (int i = 0; i < gebruikers.Length / 3; i++)
            {

                if (gebruikers[i, 0] != null)
                {                  
                  
                    Console.WriteLine($"Gebruiker {i+1}: {gebruikers[i, 0]} ");
                    
                }                    
            }
        }

        static int CheckEmptySpot(string[,] gebruikers)
        {
            int emptySpot = -1;


            for (int i = 0; i < gebruikers.Length / 3; i++)
            {
                if (gebruikers[i, 0] == null)
                {
                    emptySpot = i;
                    return emptySpot;
                }
            }
            return emptySpot;

        }

        static void maakGebruiker(string[,] gebruikers, string bestandsnaam)
        {
            int legePlaats = CheckEmptySpot(gebruikers);
            if (legePlaats != -1)
            {

            Start:

                string info = string.Empty;
                Console.WriteLine("Voer een gebruikersnaam in, deze mag alleen letters en getallen bevatten,");
                Console.Write("Gebruikersnaam:");
                info = Console.ReadLine().ToLower();
                if (Regex.IsMatch(info, @"^[a-zA-Z0-9]+$"))
                {
                    Console.Clear();
                    
                    Console.WriteLine($"Je gebruikersnaam is {info}");
                    gebruikers[legePlaats, 0] = info;
                Middle:
                    Console.WriteLine("Voer een wachtwoord in,dit moet 1 kleine, grote letter hebben\nMinstens 1 vreemd teken #?!@$%^&*-\ntussen de 8 en 20 letters hebben\nMinstens 1 getal");
                    Console.Write("Wachtwoord:");
                    info = Console.ReadLine();

                    if (Regex.IsMatch(info, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$"))
                    {
                        Console.Clear();
                        gebruikers[legePlaats, 1] = EncrypteWachtwoord(info);
                        gebruikers[legePlaats, 2] = "200";
                        SlaagGebruikersOp(gebruikers,bestandsnaam);
                    }
                    else
                    {
                        Console.Clear();
                        goto Middle;
                    }

                }
                else
                {
                    goto Start;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Je Lijst zit vol, verwijder er eerst eentje. Dit kan je doen in menu 3");
                
            }


        }
        static void SlaagGebruikersOp(string[,] gebruikers, string bestandsnaam)
        {
            if (File.Exists(bestandsnaam))
            {
                using (StreamWriter writer = new StreamWriter(bestandsnaam))
                {
                    for (int i = 0; i < gebruikers.Length / 3; i++)
                    {
                        if (gebruikers[i,0] != null)
                        {
                            writer.WriteLine($"{gebruikers[i, 0]},{gebruikers[i, 1]},{gebruikers[i, 2]}");
                        }
                        
                        
                    }
                }
            }
            else
            {
                Console.WriteLine("Er werd geen locaal bestand gevonden");

            }

        }
        static void GebruikerBewerken(string[,] gebruikers, string bestandsnaam) 
        {
            
            int gebruiker;
            int keuze;
            bool running = true;
            string nieuweInfo = "";
         

            while (running)
            {
                
                LaatGebruikersNaamZien(gebruikers);
                
                Console.WriteLine();
                Console.Write("De hoeveelste gebruiker wilt u selecteren:");
                gebruiker = InputIntKeuze(CheckEmptySpot(gebruikers));
                gebruiker--;

                Console.WriteLine("1. Gebruikernaam bewerken");
                Console.WriteLine("2. Gebruiker Wachtwoord bewerken");
                Console.WriteLine("3. Gebruiker buget bewerken");
                Console.WriteLine("4. Stoppen met aanpassen");             
                keuze = InputIntKeuze(4);
                
                switch (keuze)
                {
                    case 1:

                        Console.WriteLine("Voer een gebruikersnaam in, deze mag alleen letters en getallen bevatten,");
                        Console.Write("Gebruikersnaam:");
                        nieuweInfo = Console.ReadLine().ToLower();

                        if (Regex.IsMatch(nieuweInfo, @"^[a-zA-Z0-9]+$"))
                        {
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine($"Je gebruikersnaam is {nieuweInfo}");
                            gebruikers[gebruiker, 0] = nieuweInfo;

                        }
                        else
                        {
                            Console.Clear();
                            goto case 1;
                        }
                            break;
                    case 2:

                        Console.WriteLine("Voer een wachtwoord in,dit moet 1 kleine, grote letter hebben\nMinstens 1 vreemd teken #?!@$%^&*-\ntussen de 8 en 20 letters hebben\nMinstens 1 getal");
                        Console.Write("Wachtwoord:");
                        nieuweInfo = Console.ReadLine();

                        if (Regex.IsMatch(nieuweInfo, @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$"))
                        {
                            Console.Clear();
                            gebruikers[gebruiker, 1] = EncrypteWachtwoord(nieuweInfo);

                        }
                        else
                        {
                            Console.Clear();
                            goto case 2;
                        }
                        break;
                    case 3:

                        Console.WriteLine("Voer een nieuw buget in Max van 500 alles erboven moet je zelf verdienen");
                        Console.Write("Nieuw buget: ");
                        int buget = InputIntKeuze(500);                      
                        gebruikers[gebruiker, 2] = Convert.ToString(buget);
                        Console.Clear();

                        break;
                    case 4:
                        SlaagGebruikersOp(gebruikers,bestandsnaam);
                        running = false;
                        Console.Clear();
                        break;


                    default:
                        break;
                }
            }

        }
        static void GebruikerVerwijderen(string[,] gebruikers, string bestandsnaam)
        {
            int gebruiker;

            LaatGebruikersNaamZien(gebruikers);

            Console.WriteLine();
            Console.Write("De hoeveelste gebruiker wilt u verwijderen:");
            gebruiker = InputIntKeuze(CheckEmptySpot(gebruikers));
            gebruiker--;
            gebruikers[gebruiker, 0] = null;
            gebruikers[gebruiker, 1] = null;
            gebruikers[gebruiker, 2] = null;
            SlaagGebruikersOp(gebruikers, bestandsnaam);

        }
        static string EncrypteWachtwoord(string wachtwoord) 
        {
            string tempstring = wachtwoord;
            string finalstring = "";
            for (int i = 0; i < tempstring.Length; i++)
            {
                char tempchar = tempstring[i];
                finalstring += (char)(tempchar + 1);

            }
            return finalstring;
        }
       
    }
}