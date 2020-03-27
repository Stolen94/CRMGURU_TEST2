using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Collections;


namespace CRMGURU_TEST.Models
{
    public class Country : ISQL,  ISQLupdate
    {
        private int id;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }


        private string name;
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        private string code;
        public string Code
        {
            get
            {
                return code;
            }

            set
            {
                code = value;
            }
        }

        private int population;
        
        public int Population
        {
            get
            {
                return population;
            }

            set
            {
                population = value;
            }
        }

        private double area;

        public double Area
        {
            get
            {
                return area;
            }

            set
            {
                area = value;
            }
        }

        private Region reg;
        public Region Reg
        {
            get
            {
                return reg;
            }

            set
            {
                reg.Id = value.Id;
                reg.Name = value.Name;
            }
        }

        private Capital cap;
        public Capital Cap
        {
            get
            {
                return cap;
            }

            set
            {
                cap.Id = value.Id;
                cap.Name = value.Name;
            }
        }

        public Country(string p_Name, Capital p_Capital, string p_Alpha3Code, double p_Area, int p_Population, Region p_Region)
        {
            Name = p_Name;            
            Code = p_Alpha3Code;
            Area = p_Area;
            Population = p_Population;
            cap = p_Capital;
            reg = p_Region;
        }

        public Country()
        {
            Capital p_Capital = new Capital();
            Region p_Region = new Region();
            Name = "";
            Code = "";
            Area = 0;
            Population = 0;
            cap = p_Capital;
            reg = p_Region;
        }


        public void Show()
        {
            System.Windows.Forms.MessageBox.Show(Name + " " + Code + " " + Cap.Name + " " + Cap.Id + " " + Area + " " + Population + " " + Reg.Name + " " + Reg.Id + " ");
        }

        public ArrayList TransformResult(SqlDataReader dr)
        {
            
            ArrayList tempArr = new ArrayList();
            
            while (dr.Read())
            {
                Country TempElement = new Country();

                TempElement.Id = Convert.ToInt32(dr.GetValue(0).ToString());
                TempElement.Name = dr.GetValue(1).ToString();
                TempElement.Code = dr.GetValue(2).ToString();
                TempElement.Area = Convert.ToInt32(dr.GetValue(3).ToString());
                TempElement.Population = Convert.ToInt32(dr.GetValue(4).ToString());
                TempElement.cap.Id = Convert.ToInt32(dr.GetValue(5).ToString());            
                TempElement.reg.Id= Convert.ToInt32(dr.GetValue(6).ToString());

                tempArr.Add(TempElement);


            }

            return tempArr;
        }

        public ArrayList FindIfExist()
        {

            MSSQLConnector connector = new MSSQLConnector();
            connector.OpenConnect();


            string rqst = "Select * from Страны Where [Код Страны] = '" + this.Code + "'";

            SqlCommand command1 = new SqlCommand(rqst, connector.GetConnect());
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
            SqlCommand command2 = new SqlCommand(rqst, connector.GetConnect());
            command2.ExecuteNonQuery();

            connector.CloseConnect();
        }

        public void UpdateRecord()
        {
            MSSQLConnector connector = new MSSQLConnector();
            connector.OpenConnect();
            
            string rqst = @"UPDATE Страны SET Название = '" + this.Name + "', [Код страны]= '" + this.Code + "', Площадь=" + this.Area + 
                          ",Население=" + this.Population + ", Регион =" + this.Reg.Id + ", Столица = " + this.Cap.Id + " WHERE id=" + this.Id; ;

            SqlCommand command = new SqlCommand(rqst, connector.GetConnect());
            command.ExecuteNonQuery();
            System.Windows.Forms.MessageBox.Show(this.Name + " data was updated");

            connector.CloseConnect();
        }
    }
}
