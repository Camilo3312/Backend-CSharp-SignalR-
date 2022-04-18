using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIMySQL.Data
{
    public class MySQLConfiguration
    {
        public MySQLConfiguration(string connectionString) => _connectionString = connectionString;

        public string _connectionString { get; set; }
    }
}
