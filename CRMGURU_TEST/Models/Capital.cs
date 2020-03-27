//Класс предназначен для хранения информации о столице, а так же обращений к таблице "Города" через интерфейс ISQL
namespace CRMGURU_TEST.Models
{
    using System;
    using System.Collections;
    using System.Data.SqlClient;

    public class Capital : ISQL
    {
        //Properties
        public int Id { get; set; }
        public String Name { get; set; }
        
        //Constructor
        public Capital(int Id = 0, string Name = "")
        {
            this.Id = Id;
            this.Name = Name;
        }

        //ISQL interface implementations
        public ArrayList TransformResult(SqlDataReader dr)
        {
            ArrayList tempArr = new ArrayList();
            Capital tempElement = new Capital();
            while (dr.Read())
            {
                tempElement.Id = Convert.ToInt32(dr.GetValue(0).ToString());
                tempElement.Name = dr.GetValue(1).ToString();
                tempArr.Add(tempElement);
            }

            return tempArr;
        }

        public ArrayList FindIfExist()
        {

            MSSQLConnector connector = new MSSQLConnector();
            connector.OpenConnect();

            string rqst = "Select * from Города Where Название = '" + this.Name + "'";
            SqlCommand command1 = new SqlCommand(rqst, connector.conn);
            SqlDataReader dr = command1.ExecuteReader();
            ArrayList tempArr = TransformResult(dr);
            connector.CloseConnect();
            if (tempArr.Count == 0) tempArr.Add(new Models.Capital());
            return tempArr;
        }

        public void InsertinDB()
        {
            MSSQLConnector connector = new MSSQLConnector();
            connector.OpenConnect();

            string rqst = "INSERT INTO Города (Название) VALUES ('" + this.Name + "')";
            SqlCommand command2 = new SqlCommand(rqst, connector.conn);
            command2.ExecuteNonQuery();

            connector.CloseConnect();
        }
    }
}
