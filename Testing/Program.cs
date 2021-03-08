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
            
            Console.WriteLine(EncrypteWachtwoord("hallo"));
            Console.ReadLine();
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
