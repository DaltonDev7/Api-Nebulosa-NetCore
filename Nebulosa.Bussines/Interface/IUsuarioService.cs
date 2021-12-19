using Nebulosa.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nebulosa.Bussines.Interface
{
    public interface IUsuarioService
    {

        Task<dynamic> Update(UsuarioUpdateDTO usuario);

        Task<dynamic> getAllUser();

        Task<dynamic> actualizarImgProfile(UserImgDTO imgUser);
        Task<dynamic> GetUserByUrl(string url);
        Task<dynamic> GetUser(int idUsuario);

        Task<dynamic> BuscadorUsuarios(string busqueda);

        Task<dynamic> Me();
    }
}
