using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;

namespace CRMGURU_TEST
{
    class Representation
    {
        private DataTable table;

        public DataTable Table
        {
            get
            {
                return table;
            }
        set
            {
                table = value;
            }
        }

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

        public Representation(Models.Country CI)
        {
            Table = new DataTable("Country Information");
            this.MakeHeaders();
            AddRow(CI);
        }


        private void MakeHeaders()
        {
            Table.Columns.Add("Name", typeof(String));
            Table.Columns.Add("Capital", typeof(String));
            Table.Columns.Add("Region", typeof(String));
            Table.Columns.Add("Code", typeof(String));
            Table.Columns.Add("Area", typeof(System.Double));
            Table.Columns.Add("Population", typeof(System.Int32));
        }

        public void AddRow(Models.Country CI)
        {
            DataRow row;
            row = table.NewRow();
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
