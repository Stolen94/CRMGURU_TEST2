//Класс предназначен для хранения информации о cтране, а так же обращений к таблице "Страны" через интерфейсы ISQL и ISQLUPDATE
namespace CRMGURU_TEST.Models
{
    using System;
    using System.Collections;
    using System.Data.SqlClient;

    public class Country : ISQL, ISQLupdate
    {
        //properties
        public int Id { get; set; }
        public String Name { get; set; }
        public String Code { get; set; }
        public int Population { get; set; }
        public double Area { get; set; }

        private Region reg;
        public Region Reg
        {
            get { return reg; }

            set
            {
                reg.Id = value.Id;
                reg.Name = value.Name;
            }
        }

        private Capital cap;
        public Capital Cap
        {
            get { return cap; }

            set
            {
                cap.Id = value.Id;
                cap.Name = value.Name;
            }
        }

        //constructors
        public Country(string name, Capital capital, string alpha3Code, double area, int population, Region region)
        {
            this.Name = name;
            this.Code = alpha3Code;
            this.Area = area;
            this.Population = population;
            this.cap = capital;
            this.reg = region;
        }

        public Country()
        {
            Capital capital = new Capital();
            Region region = new Region();
            this.Name = "";
            this.Code = "";
            this.Area = 0;
            this.Population = 0;
            this.cap = capital;
            this.reg = region;
        }

        //mehtods
        public void Show()
        {
            System.Windows.Forms.MessageBox.Show(Name + " " + Code + " " + Cap.Name + " " + Cap.Id + " " + Area + " " + Population + " " + Reg.Name + " " + Reg.Id + " ");
        }
        //ISQL Interface Implementation
        public ArrayList TransformResult(SqlDataReader dr)
        {

            ArrayList tempArr = new ArrayList();

            while (dr.Read())
            {
                Country tempElement = new Country();

                tempElement.Id = Convert.ToInt32(dr.GetValue(0).ToString());
                tempElement.Name = dr.GetValue(1).ToString();
                tempElement.Code = dr.GetValue(2).ToString();
                tempElement.Area = Convert.ToInt32(dr.GetValue(3).ToString());
                tempElement.Population = Convert.ToInt32(dr.GetValue(4).ToString());
                tempElement.Cap.Id = Convert.ToInt32(dr.GetValue(5).ToString());
                tempElement.Reg.Id = Convert.ToInt32(dr.GetValue(6).ToString());

                tempArr.Add(tempElement);
            }
            return tempArr;
        }

        public ArrayList FindIfExist()
        {

            MSSQLConnector connector = new MSSQLConnector();
            connector.OpenConnect();

            string rqst = "Select * from Страны Where [Код Страны] = '" + this.Code + "'";

            SqlCommand command1 = new SqlCommand(rqst, connector.conn);
            SqlDataReader dr = command1.ExecuteReader();
            ArrayList tempArr = TransformResult(dr);
            connector.CloseConnect();
            if (tempArr.Count == 0) tempArr.Add(new Models.Country());
            return tempArr;

        }

        public void InsertinDB()
        {
            MSSQLConnector connector = new MSSQLConnector();
            connector.OpenConnect();

            string rqst = @"INSERT INTO Страны (Название, [Код страны], Площадь, Население, Регион, Столица) VALUES "
                          + "('" + this.Name + "','" + this.Code + "'," + this.Area + "," + this.Population + "," + this.Reg.Id + "," + this.Cap.Id + ")";
            this.Show();
            SqlCommand command2 = new SqlCommand(rqst, connector.conn);
            command2.ExecuteNonQuery();

            connector.CloseConnect();
        }

        //ISQLUPDATE Interface Implementation
        public void UpdateRecord()
        {
            MSSQLConnector connector = new MSSQLConnector();

            connector.OpenConnect();

            string rqst = @"UPDATE Страны SET Название = '" + this.Name + "', [Код страны]= '" + this.Code + "', Площадь=" + this.Area +
                          ",Население=" + this.Population + ", Регион =" + this.Reg.Id + ", Столица = " + this.Cap.Id + " WHERE id=" + this.Id; ;

            SqlCommand command = new SqlCommand(rqst, connector.conn);
            command.ExecuteNonQuery();
            System.Windows.Forms.MessageBox.Show(this.Name + " data was updated");

            connector.CloseConnect();
        }
    }
}
