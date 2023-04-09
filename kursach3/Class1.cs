using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace kursach3
{
    
    class AppData
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);

        

    }
}
