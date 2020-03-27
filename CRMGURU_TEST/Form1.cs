namespace CRMGURU_TEST
{
    using System;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        //Constructor
        public Form1()
        {
            InitializeComponent();
        }
        //Methods
        private void SearchButton_Click(object sender, EventArgs e)
        {
            string countryinput = CountryInputBox.Text;

            CountryInfoExctractor exctractor = new CountryInfoExctractor();

            //проверка заполненности поля ввода
            if (countryinput == "") { System.Windows.Forms.MessageBox.Show("Input field cannot be empty!"); }
            else
            {
                //Получение данных о стране в виде строки RawData в формате JSON с помощью внешнего API
                String RawData = exctractor.RequestCountryInfo(countryinput);
                
                if (RawData != "")
                {
                    //Десериализация строки RawData в объект country типа Country
                    Deserializer DSL = new Deserializer();
                    Models.Country country = DSL.Deserialize(RawData);

                    //Отображение объекта country в TableView
                    Representation repr = new Representation(country);
                    TableView.DataSource = repr.Table;
                    TableView.Visible = true;

                    //Приглашение на сохранение CI
                    if (country.Name != "") { SaveInfo(country); }
                }
            }

        }
      
        private void SaveInfo(Models.Country country)
        {
           //Диалог с предложением сохранения
            DialogResult result = MessageBox.Show("Вы хотите сохранить результат поиска в базу данных?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //Проверка наличия столицы страны country в таблице 'Города'.
                //Если Id не найден, значение country.сap.Name записывается в таблицу 'Города'
                //и Id новой записи записывается в country.Cap.Id
                country.Cap.Id = ((Models.Capital)country.Cap.FindIfExist()[0]).Id;
                if (country.Cap.Id == 0)
                {
                    country.Cap.InsertinDB();
                    country.Cap.Id = ((Models.Capital)country.Cap.FindIfExist()[0]).Id;
                }
                //System.Windows.Forms.MessageBox.Show(p_CI.Cap.Id.ToString());

                //Проверка наличия Региона страны country в таблице 'Регионы'.
                //Если Id не найден, значение country.reg.Name записывается в таблицу 'Регионы'
                //и Id новой записи записывается в country.reg.Id
                country.Reg.Id = ((Models.Region)country.Reg.FindIfExist()[0]).Id;
                if (country.Reg.Id == 0)
                {
                    country.Reg.InsertinDB();
                    country.Reg.Id = ((Models.Region)country.Reg.FindIfExist()[0]).Id;
                }
                //System.Windows.Forms.MessageBox.Show(p_CI.Reg.Id.ToString());

                //Проверка наличия страны country в таблице 'Страны'.
                //Если Id не найден, значение country.Name записывается в таблицу 'Страны'
                //и Id новой записи записывается в country.Id
                //иначе, значения найденной записи обновляются 
                country.Id = ((Models.Country)country.FindIfExist()[0]).Id;
                if (country.Id == 0)
                {
                    country.InsertinDB();
                    country.Id = ((Models.Country)country.FindIfExist()[0]).Id;
                }
                else
                {
                    country.UpdateRecord();
                }
                System.Windows.Forms.MessageBox.Show("Success.");
            }
        }

        //Вывод всех стран из базы и отображение их в TableView
        private void ExtractButton_Click(object sender, EventArgs e)
        {
            CountryExtraction countryExtraction = new CountryExtraction();
            Representation repr = new Representation(countryExtraction.Extract());
            TableView.DataSource = repr.Table;
            TableView.Visible = true;
        }

        //Защита от ввода русских символов и цифр
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
