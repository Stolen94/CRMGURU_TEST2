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
            string country = CountryInputBox.Text;

            CountryInfoExctractor exctractor = new CountryInfoExctractor();

            //проверка заполненности поля ввода
            if (country == "") { System.Windows.Forms.MessageBox.Show("Input field cannot be empty!"); }
            else
            {
                //Получение данных о стране в виде строки RawData в формате JSON с помощью внешнего API
                String RawData = exctractor.RequestCountryInfo(country);
                
                if (RawData != "")
                {
                    //Десериализация строки RawData в объект CI типа Country
                    Deserializer DSL = new Deserializer();
                    Models.Country CI = DSL.Deserialize(RawData);

                    //Отображение объекта CI в TableView
                    Representation repr = new Representation(CI);
                    TableView.DataSource = repr.Table;
                    TableView.Visible = true;

                    //Приглашение на сохранение CI
                    if (CI.Name != "") { SaveInfo(CI); }
                }
            }

        }
      
        private void SaveInfo(Models.Country CI)
        {
           //Диалог с предложением сохранения
            DialogResult result = MessageBox.Show("Вы хотите сохранить результат поиска в базу данных?", "Подтвердите действие", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                //Проверка наличия столицы страны CI в таблице 'Города'.
                //Если Id не найден, значение CI.сap.Name записывается в таблицу 'Города'
                //и Id новой записи записывается в CI.Cap.Id
                CI.Cap.Id = ((Models.Capital)CI.Cap.FindIfExist()[0]).Id;
                if (CI.Cap.Id == 0)
                {
                    CI.Cap.InsertinDB();
                    CI.Cap.Id = ((Models.Capital)CI.Cap.FindIfExist()[0]).Id;
                }
                //System.Windows.Forms.MessageBox.Show(p_CI.Cap.Id.ToString());

                //Проверка наличия Региона страны CI в таблице 'Регионы'.
                //Если Id не найден, значение CI.reg.Name записывается в таблицу 'Регионы'
                //и Id новой записи записывается в CI.reg.Id
                CI.Reg.Id = ((Models.Region)CI.Reg.FindIfExist()[0]).Id;
                if (CI.Reg.Id == 0)
                {
                    CI.Reg.InsertinDB();
                    CI.Reg.Id = ((Models.Region)CI.Reg.FindIfExist()[0]).Id;
                }
                //System.Windows.Forms.MessageBox.Show(p_CI.Reg.Id.ToString());

                //Проверка наличия страны CI в таблице 'Страны'.
                //Если Id не найден, значение CI.Name записывается в таблицу 'Страны'
                //и Id новой записи записывается в CI.Id
                //иначе, значения найденной записи обновляются 
                CI.Id = ((Models.Country)CI.FindIfExist()[0]).Id;
                if (CI.Id == 0)
                {
                    CI.InsertinDB();
                    CI.Id = ((Models.Country)CI.FindIfExist()[0]).Id;
                }
                else
                {
                    CI.UpdateRecord();
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
