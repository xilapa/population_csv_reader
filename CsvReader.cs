using System;
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

        private Country readCountryFromCsvLine(string csvLine)
        {
            var parts = csvLine.Split(new char[] {','});
            
            var name = parts[0];
            var code = parts[1];
            var region = parts[2];
            var population = int.Parse(parts[3]);

            return new Country(name, code,region, population);
        }
    }
}