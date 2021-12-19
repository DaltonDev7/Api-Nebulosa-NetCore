using Nebulosa.Bussines.Interface;
using Nebulosa.Dal.Context;
using Nebulosa.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Repository.Genericos
{
    public class UnityOfWork : IUnity, IDisposable
    {
        private readonly NebulaDbContext _dbContext;
        public UnityOfWork(NebulaDbContext dbContext)
        {
            _dbContext = dbContext;

            //Inicializamo los repositorios y le pasamos el context al contructor de los repositorios

            Posts = new PostRepository(_dbContext);
            Like = new LikeRepository(_dbContext);
            Comentario = new ComentarioRepository(_dbContext);
            HistoriaUser = new HistoriaUserRepository(_dbContext);
            Usuario = new UsuarioRepository(_dbContext);
            Sexo = new SexoRepository(_dbContext);
        }

        public IPostRepository Posts { get; private set; }


        public ILikeRepository Like { get; private set; }

        public IComentarioRepository Comentario { get; private set; }

        public IHistoriaUserRepository HistoriaUser { get; private set; }
        public IUsuarioRepository Usuario { get; private set; }

        public ISexoRepository Sexo { get; private set; }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
