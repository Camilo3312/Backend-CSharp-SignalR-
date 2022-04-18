using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIMySQL.Model
{
    public class Room
    {
        public int idsala { get; set; }
        public string fechainicio { get; set; }
        public string fechafin { get; set; }
        public string horainicio { get; set; }
        public string horafin { get; set; }
        public int servicio { get; set; }
    }
}
