using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMGURU_TEST
{
    public class Country_Info
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public string Region { get; set; }
        public string Alpha3Code { get; set; }
        public double Area { get; set; }
        public int Population { get; set; }
       
        public Country_Info(string p_Name = "", string p_Capital = "", string p_Alpha3Code = "", double p_Area = 0, int p_Population = 0, string p_Region = "")
        {
            Name = p_Name;
            Capital = p_Capital;
            Alpha3Code = p_Alpha3Code;
            Area = p_Area;
            Population = p_Population;
            Region = p_Region;
        }



        public void Show()
        {
            System.Windows.Forms.MessageBox.Show(Name + " " + Alpha3Code + " " + Capital + " " + Area + " " + Population + " " + Region);
        }
    }
}
