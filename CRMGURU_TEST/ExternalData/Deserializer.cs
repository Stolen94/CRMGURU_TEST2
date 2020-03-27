//Класс предназначен для преобразования полученной JSON-строки rawData в объект типа Country
//CountryInfo является вспомогательным классом, предназначенным для десериализации строки rawData

namespace CRMGURU_TEST
{
    using Newtonsoft.Json;
    using System;

    class Deserializer

    {   //methods
        public Models.Country Deserialize(string rawData)
        {
            CountryInfo countryInfo = new CountryInfo();

            rawData = rawData.TrimStart('[');
            rawData = rawData.TrimEnd(']');
            Models.Country tempElement = new Models.Country();
            try
            {
                countryInfo = JsonConvert.DeserializeObject<CountryInfo>(rawData);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                return tempElement;
            }

            
            tempElement.Name = countryInfo.Name;
            tempElement.Code = countryInfo.Alpha3Code;
            tempElement.Area = countryInfo.Area;
            tempElement.Population = countryInfo.Population;
            tempElement.Cap.Name = countryInfo.Capital;
            tempElement.Reg.Name = countryInfo.Region;

            //TempElement.Show();
            return tempElement;
        }
    }
}
