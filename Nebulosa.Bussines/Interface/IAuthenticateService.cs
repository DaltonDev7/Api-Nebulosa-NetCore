using Nebulosa.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nebulosa.Bussines.Interface
{
    public interface IAuthenticateService
    {
        string RandomString(int length);

        Task<dynamic> RegistrarUsuario(RegisterDTO registerDTO);

        string SaveImage(RegisterDTO registerDTO);
        string SaveImagePost(PostDTO postDTO);
    }
}
