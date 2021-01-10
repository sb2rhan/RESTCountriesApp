using System;
using System.Collections.Generic;
using System.Text;

namespace RestCountriesApp.Models
{
    public class CountryDAO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortCode { get; set; }
        public string Capital { get; set; }
        public string Region { get; set; }
        public long Population { get; set; }
        public string FlagBase64 { get; set; }
        public string FlagPath { get; set; }

        public CountryDAO(int Id, string Name, string ShortCode, string Capital, 
                          string Region, long Population, string FlagBase64, string FlagPath)
        {
            this.Id = Id;
            this.Name = Name;
            this.ShortCode = ShortCode;
            this.Capital = Capital;
            this.Region = Region;
            this.Population = Population;
            this.FlagBase64 = FlagBase64;
            this.FlagPath = FlagPath;
        }
    }
}
