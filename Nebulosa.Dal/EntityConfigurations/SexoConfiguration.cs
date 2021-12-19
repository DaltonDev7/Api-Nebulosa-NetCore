using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nebulosa.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nebulosa.Dal.EntityConfigurations
{
    public class SexoConfiguration : IEntityTypeConfiguration<Sexo>
    {
        public void Configure(EntityTypeBuilder<Sexo> builder)
        {
            builder.ToTable("Sexo");
            builder.HasKey(x => x.Id);


        }
    }
}
