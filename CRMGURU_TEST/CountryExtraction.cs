using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using System.Data.SqlClient;

namespace CRMGURU_TEST
{
    class CountryExtraction
    {
        private MSSQLConnector connector;
        public ArrayList Extract ()
        {
            connector = new MSSQLConnector();
            connector.OpenConnect();
            ArrayList list = new ArrayList();


            string sqlExpression1 = @"SELECT A.Название,  A.[Код страны],  A.Площадь,  A.Население, B.Название as CAPITAL, C.Название as REGION 
                                          FROM Страны as A INNER JOIN Города as B ON A.Столица = B.Id
                                          INNER JOIN Регионы as C ON A.Регион = C.Id";

            SqlCommand command2 = new SqlCommand(sqlExpression1, connector.GetConnect());
            SqlDataReader dr = command2.ExecuteReader();
            

            while (dr.Read())
            {
                Models.Capital cp = new Models.Capital(0, dr.GetValue(4).ToString());
                Region rg = new Region(0, dr.GetValue(5).ToString());

                list.Add(new Models.Country(dr.GetValue(0).ToString(), cp, dr.GetValue(1).ToString(), Convert.ToDouble(dr.GetValue(2)), Convert.ToInt32(dr.GetValue(3)), rg));
            }

            dr.Close();

            connector.CloseConnect();
            return list;
        }
    }
}
