using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nebulosa.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Dal.EntityConfigurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasOne(u => u.Sexo)
                   .WithMany(s => s.Usuarios)
                   .HasForeignKey(u => u.IdSexo)
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
