using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoryProductionRFID
{
    public static class DBConnection
    {
        public static string Connection = File.OpenText("DBConnection.txt").ReadLine();
        
    }
}
