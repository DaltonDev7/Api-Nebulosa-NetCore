
using Nebulosa.Entities.DTO;
using Nebulosa.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nebulosa.Bussines.Interface
{
    public interface IPostRepository : IBaseRepository<Post>
    {

        dynamic GetPostUserCount(int IdUser);

        Task<dynamic> GetPostByIdPost(int idPost);

        dynamic RemovePostById(Post post);

        string SaveImagePost(PostDTO postDTO);
        void Update(Post post);
    }
}
