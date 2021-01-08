using System;
using System.Collections.Generic;

namespace ConsoleApp1
{   
    class Program
    {
        public static void Main(string[] args)
        {
            //var filePath = "populationData.csv";
            var filePath = args[0];
            var reader = new CsvReader(filePath);


            Console.WriteLine("\n\nTop Ten Countries");
            Country[] countries = reader.ReadFirstNCountries(10);           

            var i = 1;
            foreach (var country in countries)
            {
                Console.WriteLine($"{i:D2}. {country.Name,-20}{country.Population,10}");
                i++;
            }

            Console.WriteLine("\n\nAll Countries");

            var allCountries = new List<Country>();
            allCountries = reader.ReadAllCountries();

            var j = 1;
            foreach (var country in allCountries)
            {
                Console.WriteLine($"{j:D3}. {country.Name,-30}{country.Population,10}");
                j++;
            }
            
                   
        }
    }
}
