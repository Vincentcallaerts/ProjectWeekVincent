using System;
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
            Random random = new Random();

            //gebruiker maken volgende stap.

            while (running)
            {
                
                if (loggedIn == false)
                {
                    Console.Clear();
                    gebruikers = EmptyGebruikers();

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
                        
                        Console.WriteLine($"Hallo {gebruikers[gebruiker, 0]} | {gebruikers[gebruiker,2]} $\nHet is vandaag {timeLoggedIn.ToString("dd/MM/yyyy")}\nJe Bent al {DateTime.Now.Minute - timeLoggedIn.Minute} min ingelogt");
                    }                   
                    
                    //hieraan beginnen
                    Console.WriteLine("1. Blackjack ");
                    Console.WriteLine("2. Slotmachien ");
                    Console.WriteLine("3. Memorie ");
                    Console.WriteLine("4. Uitloggen ");
                    keuze = InputIntKeuze(4);
                    

                    switch (keuze)
                    {
                        case 1:
                            Console.Clear();
                            Console.OutputEncoding = System.Text.Encoding.Unicode;

                            string[,] kaartenboek = { { "A♥", "2♥", "3♥", "4♥", "5♥", "6♥", "7♥", "8♥", "9♥", "10♥", "J♥", "Q♥", "K♥" }, { "A♦", "2♦", "3♦", "4♦", "5♦", "6♦", "7♦", "8♦", "9♦", "10♦", "J♦", "Q♦", "K♦" }, { "A♣", "2♣", "3♣", "4♣", "5♣", "6♣", "7♣", "8♣", "9♣", "10♣", "J♣", "Q♣", "K♣" }, { "A♠", "2♠", "3♠", "4♠", "5♠", "6♠", "7♠", "8♠", "9♠", "10♠", "J♠", "Q♠", "K♠" } };
                            
                            bool runningBlackjack = true;
                            bool runningGame = false;
                            

                            while (runningBlackjack)
                            {
                                
                                string[] handSpeler = new string[10];
                                string[] handDealer = new string[10];
                                Console.WriteLine("1. Speel Spel.");
                                Console.WriteLine("2. Stop.");
                                keuze = InputIntKeuze(2);
                                runningGame = true;
                                switch (keuze)
                                {
                                    case 1:
                                        Console.Clear();
                                        TrekKaarten(handSpeler, kaartenboek, random);
                                        TrekKaarten(handSpeler, kaartenboek, random);
                                        Console.WriteLine(WaardeHand(handSpeler));

                                        
                                        if (WaardeHand(handSpeler) == 21)
                                        {
                                            Console.WriteLine("Je hebt gewonnen er is 25 euro aan je buget toegevoed.");
                                            gebruikers[gebruiker, 2] = Convert.ToString(Convert.ToInt32(gebruikers[gebruiker, 2]) + 25);
                                            SlaagGebruikersOp(gebruikers, bestandsnaam);
                                            runningGame = false;
                                        }
                                        
                                        while (runningGame)
                                        {
                                            LaatBoekKaartenAfdrukken(handSpeler);
                                            Console.WriteLine();
                                            Console.WriteLine($"Je hand heeft nu {WaardeHand(handSpeler)} als waarde.");
                                            Console.WriteLine("1. Trek kaart.");
                                            Console.WriteLine("2. Stop.");
                                            keuze = InputIntKeuze(2);
                                            switch (keuze)
                                            {
                                                case 1:

                                                    TrekKaarten(handSpeler, kaartenboek, random);
                                                    LaatBoekKaartenAfdrukken(handSpeler);
                                                    if (WaardeHand(handSpeler) == 21)
                                                    {
                                                        Console.WriteLine("Je hebt gewonnen er is 25 euro aan je buget toegevoed.");
                                                        gebruikers[gebruiker, 2] = Convert.ToString(Convert.ToInt32(gebruikers[gebruiker, 2]) + 25);
                                                        SlaagGebruikersOp(gebruikers, bestandsnaam);
                                                        runningGame = false; ;
                                                    }
                                                 
                                                    else if (WaardeHand(handSpeler) >= 21)
                                                    {
                                                        Console.WriteLine("Je hebt verloren er is 10 euro van je buget afgehaald.");
                                                        gebruikers[gebruiker, 2] = Convert.ToString(Convert.ToInt32(gebruikers[gebruiker, 2]) - 10);
                                                        SlaagGebruikersOp(gebruikers, bestandsnaam);
                                                        runningGame = false;

                                                    }
                                                    break;
                                                case 2:

                                                    if (WaardeHand(handSpeler) == 21)
                                                    {
                                                        
                                                        Console.WriteLine("Je hebt gewonnen er is 25 euro aan je buget toegevoed.");
                                                        gebruikers[gebruiker, 2] = Convert.ToString(Convert.ToInt32(gebruikers[gebruiker, 2]) + 25);
                                                        SlaagGebruikersOp(gebruikers, bestandsnaam);
                                                        runningGame = false;
                                                    }
                                                    else
                                                    {
                                                        TrekKaarten(handDealer, kaartenboek, random);
                                                        TrekKaarten(handDealer, kaartenboek, random);
                                                        while (WaardeHand(handDealer) <= 17)
                                                        {
                                                            TrekKaarten(handDealer, kaartenboek, random);
                                                        }

                                                        if (WaardeHand(handDealer) > 21)
                                                        {
                                                            Console.Write($"De Dealer had: ");
                                                            LaatBoekKaartenAfdrukken(handDealer);
                                                            Console.WriteLine("Je hebt gewonnen er is 20 euro aan je buget toegevoed.");
                                                            gebruikers[gebruiker, 2] = Convert.ToString(Convert.ToInt32(gebruikers[gebruiker, 2]) + 20);
                                                            SlaagGebruikersOp(gebruikers, bestandsnaam);
                                                            runningGame = false;

                                                        }
                                                        else if (WaardeHand(handDealer) > WaardeHand(handSpeler))
                                                        {
                                                            Console.Write($"De Dealer had: ");
                                                            LaatBoekKaartenAfdrukken(handDealer);
                                                            Console.WriteLine("Je hebt verloren er is 10 euro van je buget afgehaald.");
                                                            gebruikers[gebruiker, 2] = Convert.ToString(Convert.ToInt32(gebruikers[gebruiker, 2]) -10);
                                                            SlaagGebruikersOp(gebruikers, bestandsnaam);
                                                            runningGame = false;
                                                        }
                                                        else if (WaardeHand(handDealer) == 21)
                                                        {
                                                            Console.Write($"De Dealer had: ");
                                                            LaatBoekKaartenAfdrukken(handDealer);
                                                            Console.WriteLine("Je hebt verloren er is 10 euro van je buget afgehaald.");
                                                            gebruikers[gebruiker, 2] = Convert.ToString(Convert.ToInt32(gebruikers[gebruiker, 2]) - 10);
                                                            SlaagGebruikersOp(gebruikers, bestandsnaam);
                                                            runningGame = false;
                                                        }
                                                        else
                                                        {
                                                            Console.Write($"De Dealer had: ");
                                                            LaatBoekKaartenAfdrukken(handDealer);
                                                            Console.WriteLine("Je hebt niets verloren.");
                                                            gebruikers[gebruiker, 2] = Convert.ToString(Convert.ToInt32(gebruikers[gebruiker, 2]) - 10);
                                                            SlaagGebruikersOp(gebruikers, bestandsnaam);
                                                            runningGame = false;
                                                        }
                                                    }
                                                    break;
                                            }
                                        }
                                        break;
                                    case 2:
                                        runningBlackjack = false;
                                        runningGame = false;
                                        break;                                 
                                }
                            }
                            break;
                        case 2:

                            Console.OutputEncoding = System.Text.Encoding.Unicode;
                            
                            string[,] slotMachien = { { "♥", "♣", "♦", "♠", "A", "B", "7" }, { "♥", "♣", "♦", "♠", "A", "B", "7" }, { "♥", "♣", "♦", "♠", "A", "B", "7" } };
                            bool runningSlots = true;
                           
                            PrintSlotMachien(slotMachien);

                            while (runningSlots)
                            {
                                Console.WriteLine("1. Spin");
                                Console.WriteLine("2. Stop");
                                keuze = InputIntKeuze(2);
                                switch (keuze)
                                {
                                    case 1:
                                        for (int i = 0; i < 50; i++)
                                        {
                                            PrintSlotMachien(slotMachien);
                                            Draai(slotMachien,random);
                                        }
                                        if (WinstDiagonaal(slotMachien) + WinstHorizontaal(slotMachien) != 0)
                                        {
                                            Console.WriteLine($"Je winst is in het totaal: {WinstHorizontaal(slotMachien) + " " + WinstDiagonaal(slotMachien)} = {WinstDiagonaal(slotMachien) + WinstHorizontaal(slotMachien)}");
                                            Console.WriteLine("Deze wordt bij je buget opgetelt");
                                            gebruikers[gebruiker, 2] = Convert.ToString(Convert.ToInt32(gebruikers[gebruiker, 2]) + WinstDiagonaal(slotMachien) + WinstHorizontaal(slotMachien));
                                            SlaagGebruikersOp(gebruikers, bestandsnaam);

                                        }
                                        else
                                        {
                                            
                                            Console.WriteLine("Er is geen 3 op een rij, Er staat nu 5 euro minder in uw buget");
                                            gebruikers[gebruiker, 2] = Convert.ToString(Convert.ToInt32(gebruikers[gebruiker, 2]) - 5);
                                            SlaagGebruikersOp(gebruikers, bestandsnaam);
                                        }
                                        
                                        break;
                                    case 2:
                                        runningSlots = false;
                                        break;

                                }
                            }
                            break;
                        case 3:

                            bool runningMemory = true;
                            
                            Console.OutputEncoding = System.Text.Encoding.Unicode;
                            string[] memory = { "♥", "♥", "♦", "♦", "♣", "♣", "♠", "♠" };
                            string[] oplossing = new string[8];                          

                            while (runningMemory)
                            {
                                Console.Clear();
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

                                        if (AntwoordJuist(oplossing, antwoordSpeler))
                                        {

                                            Console.WriteLine("Je hebt gewonnen er is 10 euro aan je buget toegevoed.");
                                            gebruikers[gebruiker, 2] = Convert.ToString(Convert.ToInt32(gebruikers[gebruiker, 2]) + 10);
                                            SlaagGebruikersOp(gebruikers, bestandsnaam);

                                        }
                                        else
                                        {
                                            Console.WriteLine("Je hebt verloren er is 20 euro van je buget afgehaald.");
                                            gebruikers[gebruiker, 2] = Convert.ToString(Convert.ToInt32(gebruikers[gebruiker, 2]) - 20);
                                            SlaagGebruikersOp(gebruikers, bestandsnaam);

                                        }

                                        break;
                                    case 2:
                                        runningMemory = false;
                                        break;
                                    default:
                                        break;
                                }
                            }

                            break;
                        case 4:
                            laatsteKeuze = 0;
                            Console.WriteLine("4. Uitloggen: ");
                            SlaagGebruikersOp(gebruikers, bestandsnaam);
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

        private static string[,] EmptyGebruikers()
        {
            string[,] gebruikersnew = new string[6, 3];
            return gebruikersnew;
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
                File.Delete(bestandsnaam);

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
        static void TrekKaarten(string[] hand, string[,] kaartenboek, Random random)
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
            Console.WriteLine();
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
                temp = hand[i].Substring(0, hand[i].Length - 1);
                if (int.TryParse(temp, out temper))
                {
                    waarde += Convert.ToInt32(temp);
                }
                else if (temp == "A" || temp == "J" || temp == "Q" || temp == "K")
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
                        Console.Write(oplossing[i] + " ");
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
                        Console.Write(oplossing[i] + " ");
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
            for (int i = 0; i < slotMachien.Length / 7; i++)
            {
                if (slotMachien[i, 0] == slotMachien[i, 1] && slotMachien[i, 2] == slotMachien[i, 1])
                {
                    switch (slotMachien[i, 0])
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
