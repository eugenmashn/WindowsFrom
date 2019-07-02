using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            char c = 'e';
            string name = "Eugen";
            string person = "Mashniskuy{0,-20} Eugen{1,15:C} twenty years";
            Console.WriteLine(System.Char.ToUpperInvariant('i'));
            Console.WriteLine(name.IndexOf("Eu"));
          // System.IO.File.WriteAllText("data.txt", "Testing", Encoding.Unicode);
            Console.WriteLine(DateTimeOffset.Now.ToString("s"));
            Random r1 = new Random();
            Console.WriteLine(r1.Next(100));
            var Eugen = Tuple.Create( "Eugen",  25);
            Console.WriteLine(Eugen.Item1);
            Guid g = Guid.NewGuid();
            Console.WriteLine(g);
            object x = null;
            object y = 5;
            Console.WriteLine(object.Equals(y,x));
            Console.ReadKey();
        }
    }
}
