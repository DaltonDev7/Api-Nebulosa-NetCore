using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Nebulosa.Bussines.Interface;
using Nebulosa.Dal.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebulosa.Services.PostService
{
    public class PostService : IPostService
    {

        public NebulaDbContext _context;

        public static IHostEnvironment _hostEnvironment;
        public static IValidateImgUserService _validateImgUser;
        private readonly IUnity _unitOfWork;


        public PostService()
        {

        }


        public string saludo()
        {
            return "Hola modofoca";
        }

        public PostService(NebulaDbContext context, IHostEnvironment hostEnvironment, IValidateImgUserService validateImgUser, IUnity unitOfWork)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _unitOfWork = unitOfWork;
            _validateImgUser = validateImgUser;
        }

        public async Task<dynamic> GetPostByUser(int IdUsuario)
        {


            var listPost = await _context.Post.Where(p => p.IdUsuario == IdUsuario).Select(p => new
            {
                p.Id,
                p.Descripcion,
                p.IdUsuario,
                Usuario = p.Usuario.Nombre + " "+ p.Usuario.Apellido,
                UsuarioImg = PostService.GetImagePostByName(p.Usuario.ImgAvatar, false),
                //   p.ImagePost,
                p.FechaCreacion,
                ImagePost = PostService.GetImagePostByName(p.ImagePost, true),
                ComentariosCount =  p.Comentario.Count(),
                LikeCount  = p.Likes.Count(),
                Comentarios = _context.Comentario.Where(c => c.IdPost == p.Id).Select(c => new {
                    c.Id,
                    c.IdPost,
                    c.IdUsuario,
                    ImgUsuario = PostService.GetImagePostByName(c.Usuario.ImgAvatar, false),
                    Usuario = c.Usuario.Nombre + " " + c.Usuario.Apellido,
                    c.Descripcion,
                    c.FechaCreacion
                }).ToList()
            }).OrderByDescending(x => x.FechaCreacion).ToListAsync();

            return listPost;
        }


        public static string GetImagePostByName(string ImagePost, bool permiso)
        {

            //VALIDAMOS SI LA IMAGEN QUE QUEREMOS RETORNAR ES LA DE UN POST
            if (permiso == true)
            {

                if (ImagePost != null)
                {
                    string path = PostService._hostEnvironment.ContentRootPath + "/images/post/" + ImagePost;
                    byte[] b = File.ReadAllBytes(path);

                    var imagen = "data:image/png;base64," + Convert.ToBase64String(b);
                    return imagen;
                }

            }
            else
            {
                //VALIDAMOS PRIMERO SI EL  USUARIO TIENE UNA IMAGEN POR DEFECTO, O IMAGEN PERSONALIZADA.
                var validate = PostService._validateImgUser.ValidateImgAvatar(ImagePost);

                if (validate == true)
                {
                    return "assets/avatar/"+ImagePost;
                }
                else
                {
                    string path = PostService._hostEnvironment.ContentRootPath + "/images/" + ImagePost;

                    byte[] b = File.ReadAllBytes(path);

                    var imagen = "data:image/png;base64," + Convert.ToBase64String(b);
                    return imagen;
                }

            }

            return null;
        }


        public dynamic RemovePostById(int idPost)
        {
            // var post = Get(p => p.Id == idPost);
            var post = _context.Post.Where(p => p.Id == idPost).FirstOrDefault();

            if (post.ImagePost != null)
            {

                string path = PostService._hostEnvironment.ContentRootPath + "/images/post/" + post.ImagePost;
                if (path != null)
                {
                    File.Delete(path);
                }

            }

            if (post == null)
            {
                return new { msj = "El Post no existe" };
            }

            return _unitOfWork.Posts.RemovePostById(post);
            
        }

        //public static dynamic getPathFormated(string nameCarpeta, string img)
        //{
        //    string path = PostService._hostEnvironment.ContentRootPath + $"/images/post/" + ImagePost;
        //    byte[] b = File.ReadAllBytes(path);
        //}

    }
}
