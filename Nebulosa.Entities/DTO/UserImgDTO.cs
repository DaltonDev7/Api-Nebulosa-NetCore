using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Entities.DTO
{
    public class UserImgDTO
    {
        public IFormFile? Image { get; set; }
        public int IdUsuario { get; set; }
    }
}
