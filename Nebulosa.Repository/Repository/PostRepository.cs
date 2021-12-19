using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Nebulosa.Bussines.Interface;
using Nebulosa.Dal.Context;
using Nebulosa.Entities.DTO;
using Nebulosa.Entities.Entities;
using Nebulosa.Repository.Genericos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebulosa.Repository.Repository
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public static IHostEnvironment _hostEnvironment;

        public NebulaDbContext _context { get { return context; } }
        public PostRepository(NebulaDbContext context) : base(context)
        {
           
        }


        public dynamic GetPostUserCount(int IdUser)
        {

            var validate = _context.Post.Select(p => p.IdUsuario == IdUser);

            if (validate != null)
            {
                return _context.Post.Count(p => p.IdUsuario == IdUser);
            }

            return 0;
        }

   
        public dynamic RemovePostById(Post post)
        {
      
            Remove(post);
            return new { msj = "Post Eliminado" };
        }

        public void Update(Post post)
        {
            var postUpdate = Get(post.Id);
            postUpdate.Descripcion = post.Descripcion;
            postUpdate.FechaModificacion = DateTime.UtcNow.AddMinutes(-240);
        }


        public string SaveImagePost(PostDTO postDTO)
        {

            string uniqueFileName = null;

            if (postDTO.Image != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.ContentRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + postDTO.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    postDTO.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;

        }


        public static string GetImagePostByName(string ImagePost)
        {

            if (ImagePost != null)
            {
                string path =  PostRepository._hostEnvironment.ContentRootPath + "/images/post/" + ImagePost;
                byte[] b = File.ReadAllBytes(path);

                var imagen = Convert.ToBase64String(b);
                return imagen;
            }

            return null;
        }

        public Task<dynamic> GetPostByIdPost(int idPost)
        {
            throw new NotImplementedException();
        }
    }
}
