using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nebulosa.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Dal.EntityConfigurations
{
    public class HistoriaUserConfiguration : IEntityTypeConfiguration<HistoriaUser>
    {
        public void Configure(EntityTypeBuilder<HistoriaUser> builder)
        {
            builder.ToTable("HistoriaUsers");
            builder.HasKey(x => x.Id);

            builder.HasOne(h => h.Usuario)
                   .WithMany(u => u.HistoriaUser)
                   .HasForeignKey(h => h.IdUsuario);
        }
    }
}
