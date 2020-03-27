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




    }

}
