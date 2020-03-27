using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;


namespace CRMGURU_TEST
{
    class Deserializer

    {
        public Models.Country Deserialize(string p_RawData)
        {
            p_RawData = p_RawData.TrimStart('[');
            p_RawData = p_RawData.TrimEnd(']');
            CountryInfo p_CI = new CountryInfo();
            try { 
            p_CI = JsonConvert.DeserializeObject<CountryInfo>(p_RawData);
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            Models.Country TempElement = new Models.Country();

            TempElement.Name = p_CI.Name;
            TempElement.Code = p_CI.Alpha3Code;
            TempElement.Area = p_CI.Area;
            TempElement.Population = p_CI.Population;
            TempElement.Cap.Name = p_CI.Capital;
            TempElement.Reg.Name = p_CI.Region;

            //TempElement.Show();
            return TempElement;
        }
    }
}
