using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nebulosa.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Dal.EntityConfigurations
{
    public class RolUsuarioConfiguration : IEntityTypeConfiguration<RolUsuario>
    {
        public void Configure(EntityTypeBuilder<RolUsuario> builder)
        {
            builder.ToTable("RolesUsuario");
            builder.HasKey(c => new { c.UserId, c.RoleId });

            builder.HasOne(ur => ur.Rol)
               .WithMany(r => r.RolesUsuarios)
               .HasForeignKey(ur => ur.RoleId)
               .IsRequired();

            builder.HasOne(ur => ur.Usuario)
           .WithMany(r => r.RolesUsuarios)
           .HasForeignKey(ur => ur.UserId)
           .IsRequired();
        }
    }
}
