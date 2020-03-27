using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Collections;



namespace CRMGURU_TEST
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private DataTable MakeTable()
        {
            // Create a new DataTable.
            System.Data.DataTable table = new DataTable("Country Information");

            table.Columns.Add("Name", typeof(String));
            table.Columns.Add("Capital", typeof(String));
            table.Columns.Add("Region", typeof(String));
            table.Columns.Add("Code", typeof(String));
            table.Columns.Add("Area", typeof(System.Double));
            table.Columns.Add("Population", typeof(System.Int32));

            return table;
        }

        public void AddRow(DataTable table, Models.Country CI) {
            DataRow row;
            row = table.NewRow();
            row[0] = CI.Name;
            row[1] = CI.Cap.Name;
            row[2] = CI.Reg.Name;
            row[3] = CI.Code;
            row[4] = CI.Area;
            row[5] = CI.Population;

            table.Rows.Add(row);        
        }

//=================================================================================================================================================================
 //=================================================================================================================================================================

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string country = CountryInputBox.Text;

            CountryInfoExctractor _forum = new CountryInfoExctractor();

            if (country == "") { System.Windows.Forms.MessageBox.Show("Input field cannot be empty!"); }
            else
            {
                String RawData = _forum.RequestCountryInfo(country);
                if (RawData != "")
                {
                    Deserializer DSL = new Deserializer();
                    Models.Country CI = DSL.Deserialize(RawData);
                    CI.Show();
                    DataTable DSource = MakeTable();
                    AddRow(DSource, CI);

                    TableView.DataSource = DSource;
                    TableView.Visible = true;

                    SaveInfo(CI);


                }
            }

        }
//=================================================================================================================================================================
//=================================================================================================================================================================
        private void SaveInfo(Models.Country p_CI)
        {
            DialogResult result = MessageBox.Show("Вы хотите сохранить результат поиска в базу данных?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                p_CI.Cap.Id = p_CI.Cap.FindIDIfExist();
                if (p_CI.Cap.Id == 0)
                {
                    p_CI.Cap.InsertinDB();
                    p_CI.Cap.Id = p_CI.Cap.FindIDIfExist();
                }
                //System.Windows.Forms.MessageBox.Show(p_CI.Cap.Id.ToString());
                p_CI.Reg.Id = p_CI.Reg.FindIDIfExist();
                if (p_CI.Cap.Id == 0)
                {
                    p_CI.Reg.InsertinDB();
                    p_CI.Reg.Id = p_CI.Cap.FindIDIfExist();
                }
                //System.Windows.Forms.MessageBox.Show(p_CI.Reg.Id.ToString());

                p_CI.Id = p_CI.FindIDIfExist();
                if (p_CI.Id == 0)
                {
                    p_CI.InsertinDB();
                    p_CI.Id = p_CI.FindIDIfExist();
                }
                else
                {
                    p_CI.UpdateRecord();
                }
                //System.Windows.Forms.MessageBox.Show(p_CI.Id.ToString());
            }
        }
 //=================================================================================================================================================================
 //=================================================================================================================================================================
        private void button2_Click(object sender, EventArgs e)
        {
            
            
           // ArrayList TempArray = 
            //Country_Info[] AllCountries = mscon1.ExtractData();
          //  DataTable DTable = new DataTable();

      //      TableView.DataSource = FillTable(DTable, mscon1.ExtractData());

          //  TableView.Visible = true;
        }
//=================================================================================================================================================================
//=================================================================================================================================================================
        private void CountryInputBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
                if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z') || e.KeyChar == 8 || e.KeyChar == ' ')
                {

                }
                else
                {
                    e.Handled = true;
                }
            
        }

        public DataTable FillTable(DataTable DTable, Models.Country[] Countries)
        {
            DTable = MakeTable();
            
            for (int i = 0; i < Countries.Length; i++)
            {
                AddRow(DTable, Countries[i]);
            }

            return DTable;
        }
    }
}
