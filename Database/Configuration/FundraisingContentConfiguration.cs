using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Configuration
{
    public class FundraisingContentConfiguration : IEntityTypeConfiguration<FundraisingContent>
    {
        public void Configure(EntityTypeBuilder<FundraisingContent> builder)
        {
            builder.ToTable(nameof(FundraisingContent), "public")
                .HasKey(p => new { p.FundraisingId, p.LanguageId });

            builder.Property(r => r.Caption)
                .HasColumnType("text");

            builder.Property(r => r.Text)
                .HasColumnType("text");
        }
    }
}
