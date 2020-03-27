//This class reads all records from 'countries' table, and return them as an ArrayList
namespace CRMGURU_TEST
{
    using System;
    using System.Collections;
    using System.Data.SqlClient;

    class CountryExtraction
    {
        // fields
        private MSSQLConnector connector;

        // methods
        public ArrayList Extract()
        {
            connector = new MSSQLConnector();
            connector.OpenConnect();
            ArrayList list = new ArrayList();


            string sqlExpression1 = @"SELECT A.Название,  A.[Код страны],  A.Площадь,  A.Население, B.Название as CAPITAL, C.Название as REGION 
                                          FROM Страны as A INNER JOIN Города as B ON A.Столица = B.Id
                                          INNER JOIN Регионы as C ON A.Регион = C.Id";

            SqlCommand command2 = new SqlCommand(sqlExpression1, connector.conn);
            SqlDataReader dr = command2.ExecuteReader();

            while (dr.Read())
            {
                Models.Capital cp = new Models.Capital(0, dr.GetValue(4).ToString());
                Models.Region rg = new Models.Region(0, dr.GetValue(5).ToString());

                list.Add(new Models.Country(dr.GetValue(0).ToString(), cp, dr.GetValue(1).ToString(), Convert.ToDouble(dr.GetValue(2)), Convert.ToInt32(dr.GetValue(3)), rg));
            }

            dr.Close();

            connector.CloseConnect();
            return list;
        }
    }
}
