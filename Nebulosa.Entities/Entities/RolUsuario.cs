using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Entities.Entities
{
    public class RolUsuario : IdentityUserRole<int>
    {
        public virtual Usuario Usuario { get; set; }
        public virtual Rol Rol { get; set; }

        public int? Estado { get; set; }
    }
}
