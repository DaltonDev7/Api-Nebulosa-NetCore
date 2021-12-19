using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nebulosa.Bussines.Interface;
using Nebulosa.Entities.DTO;
using Nebulosa.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nebulosa.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IAuthenticateService _authService;
        private readonly IPostService _postService;
        private readonly IUnity _unitOfWork;

        public PostController(
            UserManager<Usuario> userManager,
            IAuthenticateService authService,
            IPostService postService,
            IUnity unitOfWork
            )
        {
            _userManager = userManager;
            _authService = authService;
            _unitOfWork = unitOfWork;
            _postService = postService;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromForm] PostDTO post)
        {
            try
            {

                var username = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(username);

                var uniqueImg = _authService.SaveImagePost(post);

                post.IdUsuario = user.Id;
                post.ImagePost = uniqueImg;
                post.FechaCreacion = DateTime.UtcNow.AddMinutes(-240);

                

                _unitOfWork.Posts.Add(post);

                return StatusCode(200, _unitOfWork.Complete());

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
        [Route("Get")]
        public IActionResult Get()
        {
            try
            {
                
                return StatusCode(200, _unitOfWork.Posts.GetAll());

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
        [Route("GetPostByIdUser/{idUser:int}")]
        public async Task<IActionResult> GetPostUsers(int idUser)
        {
            try
            {
                // var posts = await _unitOfWork.Posts.GetPostByUser(idUser);

                var posts = await _postService.GetPostByUser(idUser);

                return StatusCode(200, posts);
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
        [Route("Remove/{idPost:int}")]
        public IActionResult Remove(int idPost)
        {
            try
            {
                var result = _postService.RemovePostById(idPost);
                return StatusCode(200, _unitOfWork.Complete());

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

        [HttpPost]
        [Route("Update")]
        public IActionResult Update(Post post)
        {
            try
            {
                _unitOfWork.Posts.Update(post);

                return StatusCode(204, _unitOfWork.Complete());

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
