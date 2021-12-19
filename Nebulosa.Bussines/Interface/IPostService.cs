using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nebulosa.Bussines.Interface
{
    public interface IPostService
    {
        Task<dynamic> GetPostByUser(int IdUsuario);
        dynamic RemovePostById(int idPost);
        string saludo();

      //  static string GetImagePostByName(string ImagePost, bool permiso);
    }
}
