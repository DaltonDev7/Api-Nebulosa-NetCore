using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nebulosa.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Dal.EntityConfigurations
{
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.ToTable("Likes");
            builder.HasKey(x => x.Id);

            builder.HasOne(l => l.Usuario)
                   .WithMany(u => u.Likes)
                   .HasForeignKey(l => l.IdUsuario)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(l => l.Post)
                   .WithMany(p => p.Likes)
                   .HasForeignKey(l => l.IdPost)
                   .OnDelete(DeleteBehavior.ClientSetNull);




        }
    }
}
