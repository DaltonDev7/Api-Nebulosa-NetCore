using Nebulosa.Bussines.Interface;
using Nebulosa.Dal.Context;
using Nebulosa.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebulosa.Services.LikeService
{
    public class LikeService : ILikeService
    {
        public NebulaDbContext _context;
        private readonly IUnity _unitOfWork;

        public LikeService(NebulaDbContext context, IUnity unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public void addLikePost(Like like)
        {

           var data = _context.Like.Where(l => l.IdPost == like.IdPost && l.IdUsuario == like.IdUsuario).FirstOrDefault();

            if (data != null)
            {
                _unitOfWork.Like.removeLike(data);
            }
            else
            {
                _unitOfWork.Like.addLike(like);
            }
        }



    }
}
