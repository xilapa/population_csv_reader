using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    class CsvReader
    {
        private string _csvFilePath;

        public CsvReader(string csvFilePath)
        {
            _csvFilePath = csvFilePath;
        }

        public Country[] ReadFirstNCountries(int nCountries)
        {
            var _countries = new Country[nCountries];

            using(var csv = File.OpenText(_csvFilePath))
            {  
                var csvLine = csv.ReadLine(); // apenas para jogar fora a primeira linha dos cabeçalhos
                for(var i=0; i<nCountries; i++ )
                {
                    csvLine = csv.ReadLine();
                    _countries[i] = readCountryFromCsvLine(csvLine);
                }
            }

            return _countries;
        }

        public List<Country> ReadAllCountries()
        {
            var _countries = new List<Country>();

            using(var csv = File.OpenText(_csvFilePath))
            {
                var csvLine = csv.ReadLine(); // descarta o cabeçalhos
                while((csvLine = csv.ReadLine()) != null)
                {
                    _countries.Add(readCountryFromCsvLine(csvLine));
                }

            }

            return _countries;
        }

        public Country ReadOneCountry(string countryCode)
        {
            var countries = new Dictionary<string, Country>();
            Country country;
            using (var csv = File.OpenText(_csvFilePath))
            {
                var csvLine = csv.ReadLine(); // descarta cabeçalhos
                while((csvLine = csv.ReadLine()) != null)
                {
                    country = readCountryFromCsvLine(csvLine);
                    countries.Add(country.Code,country);
                }
            }

            var countryFound = countries.TryGetValue(countryCode, out country);
            if (!countryFound)
                country = null;

            return country;
        }        

        private Country readCountryFromCsvLine(string csvLine)
        {
            var parts = csvLine.Split(new char[] {','});
            string name = "";
            string code = "";
            string region = "";
            var population = 0;

            switch(parts.Length)
            {
                case 4:
                    name = parts[0];
                    code = parts[1];
                    region = parts[2];
                    int.TryParse(parts[3], out population);
                    break;
                case 5: // quando o nome tem vírgula ocorre uma divisão extra
                    name = (parts[0] + ", " + parts[1]).Trim(); // o nome fica divido nos dois primeiros itens do array
                    code = parts[2];
                    region = parts[3];
                    int.TryParse(parts[4], out population);
                    break;
                default:
                    throw new ArgumentException($"{nameof(csvLine)} can't be parsed");
            }

            return new Country(name, code,region, population);
        }
    }
}