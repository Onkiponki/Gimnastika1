using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Gimnastika1
{
    internal static class Konekcija
    {
        static public SqlConnection zakonektuj()
        {
            string cs = @"Data Source = DESKTOP-HLB8M6E\SQLEXPRESS; Initial Catalog = Gimnastika; Integrated Security = True";
            return new SqlConnection(cs);
            
        }
    }
}
