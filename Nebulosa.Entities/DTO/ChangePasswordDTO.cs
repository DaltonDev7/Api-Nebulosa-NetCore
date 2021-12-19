using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Entities.DTO
{
    public class ChangePasswordDTO
    {

        public string CurrentPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
