using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;

namespace CRMGURU_TEST
{
    public class CountryInfoExctractor
    {

        public string RequestCountryInfo(string country)
        {
            try
            {
                //Performing the request through the API====================================================================================================================
                string Request = "https://restcountries.eu/rest/v2/name/" + country + "?fields=name;capital;region;alpha3Code;population;area";

                //System.Windows.Forms.MessageBox.Show(Request);
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create(Request);
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                StreamReader myStreamReader = new StreamReader(myHttpWebResponse.GetResponseStream(), Encoding.GetEncoding(1251));
                string ResponseData = (myStreamReader.ReadToEnd());
                return ResponseData;
            }

            catch(Exception e) {
                System.Windows.Forms.MessageBox.Show("Ошибка! " + e.Message );
                return "";
            }
        }
    }
}
