using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Entities.DTO
{
    public class RegisterDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int IdSexo { get; set; }

        public IFormFile? ImgAvatar { get; set; }

        public string? ImgSpace { get; set; }

        public DateTime FechaCreacion { get; set; }

        public List<string> Role { get; set; }

    }
}
