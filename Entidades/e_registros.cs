using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class e_registros
    {
        public int id_registro { get; set; }
        public string usuario { get; set; }
        public string equipo { get; set; }
        public DateTime fecha { get; set; }
        public DateTime hora_inicio { get; set; }
        public DateTime hora_fin { get; set; }

    }
}
