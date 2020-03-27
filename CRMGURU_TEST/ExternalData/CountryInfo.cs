namespace CRMGURU_TEST
{
    public class CountryInfo
    {
        // properties
        public string Alpha3Code { get; set; }
        public double Area { get; set; }
        public string Capital { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
        public string Region { get; set; }

        // construstors
        public CountryInfo()
        {       
            Alpha3Code = "";
            Area = 0;
            Capital = "";
            Name = "";
            Population = 0;
            Region = "";
        }

    }
}
