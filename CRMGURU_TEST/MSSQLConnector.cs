using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace CRMGURU_TEST
{
    class MSSQLConnector
    {
        private string connStr = @"Data Source=localhost; Initial Catalog=Country_DB;Integrated Security=True";
        private SqlConnection conn;

//========================================================================================================================================================================
//========================================================================================================================================================================
        public SqlConnection GetConnect()
        {
            return conn;
        }
//========================================================================================================================================================================
//========================================================================================================================================================================
        public void OpenConnect()
        {
            conn = new SqlConnection(connStr);
            try
            {
                conn.Open();
                //System.Windows.Forms.MessageBox.Show("Соединение успешно произведено");
            }
            catch (SqlException e)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка подключения: " + e.Message);
            }
            
        }
//========================================================================================================================================================================
//========================================================================================================================================================================
        public void CloseConnect()
        {
            try
            {
                conn.Close();
                conn.Dispose();
                //System.Windows.Forms.MessageBox.Show("Соединение успешно расторгнуто");
            }
            catch (SqlException e)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка подключения: " + e.Message);
            }

            
        }


//========================================================================================================================================================================
//========================================================================================================================================================================
       /*  public Country_Info[] ExtractData()
        {
            this.OpenConnect();
            int count = this.GetNumofRecords("Страны");
            Country_Info[] AllCountries = new Country_Info[count];

            string sqlExpression1 = @"SELECT A.Название,  A.[Код страны],  A.Площадь,  A.Население, B.Название as CAPITAL, C.Название as REGION 
                                          FROM Страны as A INNER JOIN Города as B ON A.Столица = B.Id
                                          INNER JOIN Регионы as C ON A.Регион = C.Id";
             SqlCommand command2 = new SqlCommand(sqlExpression1, this.GetConnect());
             SqlDataReader dr = command2.ExecuteReader();

              int i = 0;
              while (dr.Read())
              {
                  AllCountries[i] = new Country_Info(dr.GetValue(0).ToString(), dr.GetValue(4).ToString(), dr.GetValue(1).ToString(), Convert.ToDouble(dr.GetValue(2)), Convert.ToInt32(dr.GetValue(3)), dr.GetValue(5).ToString());
                  i++;
              }

              dr.Close();
            this.CloseConnect();
            return AllCountries;
        }*/
//========================================================================================================================================================================
//========================================================================================================================================================================


    }

}
