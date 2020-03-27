using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Collections;

namespace CRMGURU_TEST
{
    public interface ISQL
    {
        // константа
        
        ArrayList TransformResult(SqlDataReader dr);

        int FindIDIfExist();

        void InsertinDB();
    }
}
