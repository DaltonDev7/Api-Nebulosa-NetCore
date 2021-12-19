using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Entities.DTO
{
    public class UsuarioUpdateDTO
    {

        public int Id { get; set; }
        public string Email { get; set; }

        public string UserName { get; set; }
        public int IdSexo { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }

    }
}
