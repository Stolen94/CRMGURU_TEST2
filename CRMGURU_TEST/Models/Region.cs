//Класс предназначен для хранения информации о регионе, а так же обращений к таблице "Регионы" через интерфейс ISQL
namespace CRMGURU_TEST.Models
{
    using System;
    using System.Collections;
    using System.Data.SqlClient;

    public class Region : ISQL
    {
        //Properties
        public int Id { get; set; }
        public String Name { get; set; }

        //Constructor
        public Region(int Id = 0, string Name = "")
        {
            this.Id = Id;
            this.Name = Name;
        }

        //ISQL interface implementations
        public ArrayList TransformResult(SqlDataReader dr)
        {

            ArrayList tempArr = new ArrayList();
            Region tempElement = new Region();
            while (dr.Read())
            {
                tempElement.Id = Convert.ToInt32(dr.GetValue(0).ToString());
                tempElement.Name = dr.GetValue(1).ToString();
                tempArr.Add(tempElement);
            }
            dr.Close();
            return tempArr;
        }

        public ArrayList FindIfExist()
        {

            MSSQLConnector connector = new MSSQLConnector();
            connector.OpenConnect();


            string rqst = "Select * from Регионы Where Название = '" + this.Name + "'";
            SqlCommand command1 = new SqlCommand(rqst, connector.conn);
            SqlDataReader dr = command1.ExecuteReader();
            ArrayList tempArr = TransformResult(dr);
            connector.CloseConnect();
            if (tempArr.Count == 0) tempArr.Add(new Region());
            return tempArr;

        }

        public void InsertinDB()
        {
            MSSQLConnector connector = new MSSQLConnector();
            connector.OpenConnect();

            string rqst = "INSERT INTO Регионы (Название) VALUES ('" + this.Name + "')";
            SqlCommand command2 = new SqlCommand(rqst, connector.conn);
            command2.ExecuteNonQuery();

            connector.CloseConnect();
        }
    }
}
