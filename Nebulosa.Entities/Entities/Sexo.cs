using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Entities.Entities
{
    public class Sexo : EntityBase
    {
        public string Nombre { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
    }
}
