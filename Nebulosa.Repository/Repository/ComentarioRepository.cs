using Microsoft.EntityFrameworkCore;
using Nebulosa.Bussines.Interface;
using Nebulosa.Dal.Context;
using Nebulosa.Entities.Entities;
using Nebulosa.Repository.Genericos;
using Nebulosa.Services.PostService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nebulosa.Repository.Repository
{
    public class ComentarioRepository : BaseRepository<Comentario>, IComentarioRepository
    {
        public NebulaDbContext _context { get { return context; } }
        public PostService postService = new PostService();
        public ComentarioRepository(NebulaDbContext context) : base(context)
        {
        }

        public dynamic RemoveComentarioById(int idComentario)
        {
            var comentario = Get(c => c.Id == idComentario);

            if(comentario == null)
            {
                return new { msj = "El Comentario no existe" };
            }

            Remove(comentario);
            return new { msj = "Comentario Eliminado" };

        }

        public dynamic GetComentariosCount(int IdUser)
        {
            return 0;
            //return _context.Comentario.Where(c => c.IdUsuario == IdUser).Count();
        }


        public async Task<dynamic> GetComentarioByIdPost(int IdPost)
        {
            var comentarios = await _context.Post.Where(p => p.Id == IdPost).Select(p => new
            {
                ComentariosCount = p.Comentario.Count(),
                Comentarios = _context.Comentario.Where(c => c.IdPost == IdPost).Select(c => new
                {
                    c.Id,
                    c.IdUsuario,
                    Descripcion = c.Descripcion,
                    Usuario = c.Usuario.Nombre + " " + c.Usuario.Apellido,
                    ImgUsuario = PostService.GetImagePostByName(c.Usuario.ImgAvatar, false)
                }).ToList()
            }).FirstOrDefaultAsync();


            //var comentarios =   await _context.Comentario.Where(c => c.IdPost == IdPost).Select(c => new
            //{
            //    Descripcion = c.Descripcion,
            //    Usuario = c.Usuario.Nombre + " " + c.Usuario.Apellido
            //}).ToListAsync();

            return comentarios;
        }

        public void Update(Comentario comentario)
        {
            var comentarioUpdate = Get(comentario.Id);
            comentarioUpdate.Descripcion = comentario.Descripcion;
        }
    }
}
