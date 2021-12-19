using Microsoft.AspNetCore.Http;
using Nebulosa.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Entities.DTO
{
    public class PostDTO : Post
       
    {
        public IFormFile? Image { get; set; }

    }
}
