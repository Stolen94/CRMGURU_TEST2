//Класс предназначен для поиска информации о заданной стране через API.

namespace CRMGURU_TEST
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    public class CountryInfoExctractor
    {
        // methods
        public string RequestCountryInfo(string country)
        {
            try
            {
                //Performing the request through the API======================================================================================
                string Request = "https://restcountries.eu/rest/v2/name/" + country + "?fields=name;capital;region;alpha3Code;population;area";

                //System.Windows.Forms.MessageBox.Show(Request);
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(Request);
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                StreamReader myStreamReader = new StreamReader(myHttpWebResponse.GetResponseStream(), Encoding.GetEncoding(1251));
                string responseData = (myStreamReader.ReadToEnd());
                return responseData;
            }

            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка! " + e.Message);
                return "";
            }
        }
    }
}
