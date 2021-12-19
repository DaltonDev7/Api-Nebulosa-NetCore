using Nebulosa.Bussines.Interface;
using Nebulosa.Dal.Context;
using Nebulosa.Entities.Entities;
using Nebulosa.Repository.Genericos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Repository.Repository
{
    public class HistoriaUserRepository : BaseRepository<HistoriaUser>, IHistoriaUserRepository
    {

        public NebulaDbContext _context { get { return context; } }
        public HistoriaUserRepository(NebulaDbContext context) : base(context)
        {
        }

        public void RemoveHistoryById(int idHistoria)
        {
            var historia = Get(h => h.Id == idHistoria);

            if (historia != null)
            {
                Remove(historia);
            }
        }
    }
}
