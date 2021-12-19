using Nebulosa.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Bussines.Interface
{
    public interface IHistoriaUserRepository : IBaseRepository<HistoriaUser>
    {
        void RemoveHistoryById(int idHistoria);
    }
}
