using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Entities.Entities
{
    public class Rol : IdentityRole<int>
    {
        public ICollection<RolUsuario> RolesUsuarios { get; set; }
    }
}
