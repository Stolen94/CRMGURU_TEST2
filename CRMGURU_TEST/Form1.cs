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

//=================================================================================================================================================================
 //=================================================================================================================================================================

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string country = CountryInputBox.Text;

            CountryInfoExctractor exctractor = new CountryInfoExctractor();

            if (country == "") { System.Windows.Forms.MessageBox.Show("Input field cannot be empty!"); }
            else
            {
                String RawData = exctractor.RequestCountryInfo(country);
                if (RawData != "")
                {
                    Deserializer DSL = new Deserializer();
                    Models.Country CI = DSL.Deserialize(RawData);
                    
                    Representation repr = new Representation(CI);
                    TableView.DataSource = repr.Table;
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
                p_CI.Cap.Id = ((Models.Capital)p_CI.Cap.FindIfExist()[0]).Id;
                if (p_CI.Cap.Id == 0)
                {
                    p_CI.Cap.InsertinDB();
                    p_CI.Cap.Id = ((Models.Capital)p_CI.Cap.FindIfExist()[0]).Id;
                }
                //System.Windows.Forms.MessageBox.Show(p_CI.Cap.Id.ToString());
                p_CI.Reg.Id = ((Region)p_CI.Reg.FindIfExist()[0]).Id;
                if (p_CI.Reg.Id == 0)
                {
                    p_CI.Reg.InsertinDB();
                    p_CI.Reg.Id = ((Region)p_CI.Reg.FindIfExist()[0]).Id;
                }
                //System.Windows.Forms.MessageBox.Show(p_CI.Reg.Id.ToString());

                p_CI.Id = ((Models.Country)p_CI.FindIfExist()[0]).Id;
                if (p_CI.Id == 0)
                {
                    p_CI.InsertinDB();
                    p_CI.Id = ((Models.Country)p_CI.FindIfExist()[0]).Id;
                }
                else
                {
                    p_CI.UpdateRecord();
                }
                System.Windows.Forms.MessageBox.Show("Success.");
            }
        }
 //=================================================================================================================================================================
 //=================================================================================================================================================================
        private void ExtractButton_Click(object sender, EventArgs e)
        {
            CountryExtraction countryExtraction = new CountryExtraction();
            Representation repr = new Representation(countryExtraction.Extract());
            TableView.DataSource = repr.Table;
            TableView.Visible = true;
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

    }
}
