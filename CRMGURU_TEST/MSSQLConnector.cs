//Класс содержит функции для подключения/отключения к базе данных:
namespace CRMGURU_TEST
{
    using System.Configuration;
    using System.Data.SqlClient;

    class MSSQLConnector
    {
        //fields
        private string connStr;
        public SqlConnection conn { get; private set; }

        //methods
       
        //Установка подключения
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
        //Закрытие подключения
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

        //Считывание конфигурационной информации
        private string GetConnectionStringByName(string name)
        {
            string returnValue = null;
            try
            {
                ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
                if (settings != null)
                    returnValue = settings.ConnectionString;
                
            }
            catch(System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка! " + e.Message);
            }
            return returnValue;
        }
    }

}
