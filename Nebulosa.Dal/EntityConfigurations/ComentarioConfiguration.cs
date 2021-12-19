using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nebulosa.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Dal.EntityConfigurations
{
    public class ComentarioConfiguration : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> builder)
        {
            builder.ToTable("Cometarios");
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Usuario)
                    .WithMany(u => u.Comentario)
                    .HasForeignKey(c => c.IdUsuario)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Post)
                   .WithMany(p => p.Comentario)
                   .HasForeignKey(c => c.IdPost)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
