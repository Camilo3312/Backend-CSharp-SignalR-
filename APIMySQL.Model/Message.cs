using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIMySQL.Model
{
    public class Message
    {
        public int idmensaje { get; set; }
        public int idusuario { get; set; }
        public string mensaje { get; set; }
        public string fecha { get; set; }
        public int idsala { get; set; }
        public string token { get; set; }
    }
}
