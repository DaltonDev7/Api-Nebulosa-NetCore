using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Entities.Entities
{
    public class Post : EntityBase
    {
        public string? ImagePost { get; set; }
        public string Descripcion { get; set; }
        public int IdUsuario { get; set; }

        public Usuario Usuario { get; set; }
        public ICollection<Comentario> Comentario { get; set; }

        public ICollection<Like> Likes { get; set; }
    }
}
