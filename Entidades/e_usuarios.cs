using System;

namespace Entidades
{
    public class e_usuarios
    {
        public string usuario { get; set; }
        public string pass { get; set; }
        public int tipo_usuario { get; set; }
        public int estado { get; set; }
        public int castigos { get; set; }
        public int causa_castigo { get; set; }
        public DateTime fecha { get; set; }
    }
}
