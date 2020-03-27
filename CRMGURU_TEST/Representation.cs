//Класс содержит функции для формирования таблицы, отображаемой в TableView
namespace CRMGURU_TEST
{
    using System;
    using System.Collections;
    using System.Data;

    public class Representation
    {
        //properties        
        public DataTable Table { get; set; }

        //constructors
        //Таблица для отображения всех стран из базы
        public Representation(ArrayList list)
        {
            Table = new DataTable("Country Information");
            this.MakeHeaders();

            for (int i = 0; i < list.Count; i++)
            {
                Models.Country CI = ((Models.Country)list[i]);
                AddRow(CI);
            }
        }
        //Таблица для отображения одной страны (найденной в поиске)
        public Representation(Models.Country CI)
        {
            Table = new DataTable("Country Information");
            this.MakeHeaders();
            AddRow(CI);
        }

        //methods
        //Заполнение заголовков
        private void MakeHeaders()
        {
            Table.Columns.Add("Name", typeof(String));
            Table.Columns.Add("Capital", typeof(String));
            Table.Columns.Add("Region", typeof(String));
            Table.Columns.Add("Code", typeof(String));
            Table.Columns.Add("Area", typeof(System.Double));
            Table.Columns.Add("Population", typeof(System.Int32));
        }

        //Заполнение строк
        private void AddRow(Models.Country CI)
        {
            DataRow row;
            row = Table.NewRow();
            row[0] = CI.Name;
            row[1] = CI.Cap.Name;
            row[2] = CI.Reg.Name;
            row[3] = CI.Code;
            row[4] = CI.Area;
            row[5] = CI.Population;

            Table.Rows.Add(row);
        }
    }
}
