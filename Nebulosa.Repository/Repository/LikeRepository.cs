using Nebulosa.Bussines.Interface;
using Nebulosa.Dal.Context;
using Nebulosa.Entities.Entities;
using Nebulosa.Repository.Genericos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebulosa.Repository.Repository
{
    public class LikeRepository : BaseRepository<Like>, ILikeRepository
    {
        public NebulaDbContext _context { get { return context; } }
        public LikeRepository(NebulaDbContext context) : base(context)
        {
        }

        public void addLike(Like like)
        {

            Add(like);
        }

        public void removeLike(Like like)
        {
            Remove(like);
        }

            
        public dynamic GetLikesByIdPost(int idPost)
        {
           return _context.Post.Where(p => p.Id == idPost).Select(p => new
            {
                LikeCount = p.Likes.Count(l => l.IdPost == p.Id),
            }).ToList();
        }

    }
}
