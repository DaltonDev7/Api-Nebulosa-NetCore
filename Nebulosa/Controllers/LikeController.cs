using Microsoft.AspNetCore.Mvc;
using Nebulosa.Bussines.Interface;
using Nebulosa.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nebulosa.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : Controller
    {

        private readonly ILikeService _likeService;
        private readonly IUnity _unitOfWork;

        public LikeController(ILikeService likeService, IUnity unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _likeService = likeService;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add(Like like)
        {
            try
            {

                _likeService.addLikePost(like);

               return StatusCode(201, _unitOfWork.Complete());

            }
            catch (Exception err)
            {
                return StatusCode(500, err.Message);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }


        [HttpGet]
        [Route("GetLikesByIdPost/{idPost:int}")]
        public IActionResult GetLikesByIdPots(int idPost)
        {
            try
            {

                var likes = _unitOfWork.Like.GetLikesByIdPost(idPost);

                return StatusCode(200, likes);

            }
            catch (Exception err)
            {
                return StatusCode(500, err.Message);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }


    }
}
