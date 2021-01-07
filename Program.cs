using System;

namespace ConsoleApp1
{   
    class Program
    {
        public static void Main(string[] args)
        {
            //var filePath = "populationData.csv";
            var filePath = args[0];
            var reader = new CsvReader(filePath);

            Country[] countries = reader.ReadFirstNCountries(10);

            var i = 1;
            foreach (var country in countries)
            {
                Console.WriteLine($"{i:D2}. {country.Name,-20}{country.Population,10}");
                i++;
            }
                   
        }
    }
}
