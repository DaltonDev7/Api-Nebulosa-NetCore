using Nebulosa.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nebulosa.Bussines.Interface
{
    public interface IComentarioRepository : IBaseRepository<Comentario>
    {
        dynamic RemoveComentarioById(int idComentario);
        dynamic GetComentariosCount(int IdUser);

        Task<dynamic> GetComentarioByIdPost(int IdPost);

        void Update(Comentario comentario);
    }
}
