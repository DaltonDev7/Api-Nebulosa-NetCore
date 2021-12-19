using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nebulosa.Dal.EntityConfigurations;
using Nebulosa.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Dal.Context
{
    public class NebulaDbContext : IdentityDbContext<Usuario, Rol, int, IdentityUserClaim<int>, RolUsuario, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public NebulaDbContext(DbContextOptions<NebulaDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            //Aqui se pondran las configuraciones de los modelos
            builder.ApplyConfiguration(new PostConfiguration());
            builder.ApplyConfiguration(new UsuarioConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new RolUsuarioConfiguration());
            builder.ApplyConfiguration(new ComentarioConfiguration());
            builder.ApplyConfiguration(new LikeConfiguration());
            builder.ApplyConfiguration(new HistoriaUserConfiguration());
            builder.ApplyConfiguration(new SexoConfiguration());
        }

        //Aqui se tiene que poner el DbSet De cada Modelo.
        public DbSet<Post> Post { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Rol> Rol { get; set; }

        public DbSet<RolUsuario> RolUsuario { get; set; }

        public DbSet<Comentario> Comentario { get; set; }
        public DbSet<Like> Like { get; set; }
        public DbSet<HistoriaUser> HistoriaUser { get; set; }
        public DbSet<Sexo> Sexo { get; set; }
    }
}
