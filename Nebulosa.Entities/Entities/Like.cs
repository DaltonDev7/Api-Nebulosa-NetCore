using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Entities.Entities
{
    public class Like : EntityBase
    {
        public int IdPost { get; set; }
        public int IdUsuario { get; set; }

        public Usuario Usuario { get; set; }
        public Post Post { get; set; }
    }
}
