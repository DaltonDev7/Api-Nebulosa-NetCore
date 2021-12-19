using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Entities.Entities
{
    public class Comentario : EntityBase
    {
        public string Descripcion { get; set; }
        public int IdUsuario { get; set; }
        public int IdPost { get; set; }

        public Usuario Usuario { get; set; }
        public Post Post { get; set; }
    }
}
