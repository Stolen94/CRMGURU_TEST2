using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMGURU_TEST
{
    public class CountryInfo
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public string Region { get; set; }
        public string Alpha3Code { get; set; }
        public double Area { get; set; }
        public int Population { get; set; }
       
        public CountryInfo()
        {
            Name = "";
            Capital = "";
            Alpha3Code = "";
            Area = 0;
            Population = 0;
            Region = "";
        }

    }
}
