using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.SqlClient;

namespace CRMGURU_TEST
{
    public class Region : ISQL
    {
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

        public Region(int p_id = 0, string p_nm = "")
        {
            Id = p_id;
            Name = p_nm;
        }

        public ArrayList TransformResult(SqlDataReader dr)
        {

            ArrayList tempArr = new ArrayList();
            Region TempElement = new Region();
            while (dr.Read())
            {
                TempElement.Id = Convert.ToInt32(dr.GetValue(0).ToString());
                TempElement.Name = dr.GetValue(1).ToString();
                tempArr.Add(TempElement);
            }
            dr.Close();
            return tempArr;
        }

        public int FindIDIfExist()
        {

            MSSQLConnector connector = new MSSQLConnector();
            connector.OpenConnect();
            int id = 0;

            string rqst = "Select * from Регионы Where Название = '" + this.Name + "'";
            SqlCommand command1 = new SqlCommand(rqst, connector.GetConnect());
            SqlDataReader dr = command1.ExecuteReader();
            ArrayList tempArr = TransformResult(dr);
            connector.CloseConnect();
            if (tempArr.Count == 0) { return 0; }
            id = ((Region)tempArr[0]).Id;
            return id;

        }

        public void InsertinDB()
        {
            MSSQLConnector connector = new MSSQLConnector();
            connector.OpenConnect();
        
            string rqst = "INSERT INTO Регионы (Название) VALUES ('" + this.Name + "')";
            SqlCommand command2 = new SqlCommand(rqst, connector.GetConnect());
            command2.ExecuteNonQuery();

            connector.CloseConnect();
        }
    }
}
