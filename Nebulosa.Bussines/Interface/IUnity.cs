using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Bussines.Interface
{
    public interface IUnity
    {
        int Complete();
        void Dispose();

        IPostRepository Posts { get; }

        ISexoRepository Sexo { get; }

        ILikeRepository Like { get; }

        IComentarioRepository Comentario { get; }

        IHistoriaUserRepository HistoriaUser { get; }

        IUsuarioRepository Usuario { get;  }
    }
}
