using Nebulosa.Bussines.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Services.ValidateImgAvatar
{
    public class ValidateImgUserService : IValidateImgUserService
    {


        //METODO QUE SE ENCARGA DE VALIDAR SI EL USUARIO TIENE UNA IMAGEN POR DEFECTO.
        public dynamic ValidateImgAvatar(string img)
        {
            switch (img)
            {
                case "profile-0.jpg":
                case "profile-1.jpg":
                case "profile-2.jpg":
                case "profile-3.jpg":
                case "profile-4.jpg":
                case "profile-5.jpg":
                case "profile-6.jpg":
                case "profile-7.jpg":
                    return true;
                break;

                default:
                    return false;
               break;

            }
        }
    }
}
