using Nebulosa.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Bussines.Interface
{
    public interface ILikeRepository : IBaseRepository<Like>
    {

        void addLike(Like like);

        void removeLike(Like like);

        dynamic GetLikesByIdPost(int idPost);




    }
}
