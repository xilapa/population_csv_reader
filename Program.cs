using System;
using System.Collections.Generic;
using System.Linq;

namespace population_csv_reader
{   
    class Program
    {
        public static void Main(string[] args)
        {
            string filePath;
            if (args.Length == 0)
            {
                filePath = "populationData.csv";
            }
            else
            {   
                filePath = args[0];
            }
            var reader = new CsvReader(filePath);


            /// Menu
            int opt = 1;
            do 
            {
                Console.WriteLine("\n\nCountry Population\n\nSelect an option\n\t1 - Show the top N countries\n\t2 - Show a specific country population\n\t3 - Show all countries populations\n\t4 - Show all countries from a region\n\t0 - To exit");
                var input = Console.ReadLine();
                int.TryParse(input, out opt);

                if (opt == 0)
                    break;
                    
                switch (opt)
                {
                    case 1:
                        Console.WriteLine("Type the numbers of countries to show");
                        input = Console.ReadLine();
                        int numberCountries;
                        if (int.TryParse(input, out numberCountries))
                        {
                            Console.WriteLine($"\n\nTop {numberCountries} Countries");
                            Country[] countries = reader.ReadFirstNCountries(numberCountries);
                            PrintCountryArray(countries);
                        } 
                        else
                        {
                            Console.WriteLine("Invalid value, try again");
                        }                    
                        break;

                    case 2:
                        Console.WriteLine("Type the country Code");
                        input = Console.ReadLine().ToUpper();
                        var oneCountry = reader.ReadOneCountry(input);
                        if(oneCountry == null)
                            Console.WriteLine("Country not found");
                        else
                            Console.WriteLine($"\n\nThe country is {oneCountry.Name} and it's population is {oneCountry.Population}");
                        break;

                    case 3:
                        Console.WriteLine("\n\nAll Countries");
                        var allCountries = new List<Country>();
                        allCountries = reader.ReadAllCountries();
                        PrintCountryList(allCountries);
                        break;  

                    case 4:
                        var countriesByRegion = reader.ReadAllCountriesByRegion();

                        Console.WriteLine("\n\tRegions:");
                        foreach (var region in countriesByRegion.Keys.OrderBy(x => x))
                            Console.WriteLine($"\t{region}");
                        Console.WriteLine("\nType the region name");

                        input = Console.ReadLine();

                        if (countriesByRegion.ContainsKey(input))
                            PrintCountryList(countriesByRegion[input]);
                        else    
                            Console.WriteLine("invalid region");                    
                        break;

                    default:
                        Console.WriteLine("Invalid Option, try again");
                        break;              
                }                

            }while (opt != 0);
        }

        private static void PrintCountryArray(Country[] countries)
        {
            var i = 1;
            foreach (var country in countries)
            {
                Console.WriteLine($"{i:D2}. {country.Name,-30}{country.Population,10}");
                i++;
            }
        }

        private static void PrintCountryList(List<Country> _countries)
        {   
            var i = 1;
            foreach (var _country in _countries)
            {
                Console.WriteLine($"{i:D2}. {_country.Name,-30}{_country.Population,10}");
                i++;
            }
        }
    }
}
