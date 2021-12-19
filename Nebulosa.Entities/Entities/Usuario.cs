using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Entities.Entities
{
    public class Usuario : IdentityUser<int>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int? IdSexo { get; set; }
        public string? ImgAvatar { get; set; }
        public string UserUrl { get; set; }



        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModiificacion { get; set; }






        public Sexo Sexo { get; set; }
        public ICollection<RolUsuario> RolesUsuarios { get; set; }

        public ICollection<HistoriaUser> HistoriaUser  { get; set; }

        public ICollection<Post> Post { get; set; }
        public ICollection<Comentario> Comentario { get; set; }

        public ICollection<Like> Likes { get; set; }

    }
}
