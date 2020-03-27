using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace CRMGURU_TEST
{
    class MSSQLConnector
    {
        private string connStr;
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
            connStr = GetConnectionStringByName("DefaultConnectString");
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

            private string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;
            System.Windows.Forms.MessageBox.Show(returnValue);
            return returnValue;
        }


    }

}
