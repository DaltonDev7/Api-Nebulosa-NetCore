using Nebulosa.Bussines.Interface;
using Nebulosa.Dal.Context;
using Nebulosa.Entities.Entities;
using Nebulosa.Repository.Genericos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Repository.Repository
{
    public class SexoRepository : BaseRepository<Sexo>, ISexoRepository
    {

        public NebulaDbContext _context { get { return context; } }
        public SexoRepository(NebulaDbContext context) : base(context)
        {
        }


    }
}
