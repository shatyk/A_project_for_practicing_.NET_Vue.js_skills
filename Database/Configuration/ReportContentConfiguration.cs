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
    public class ReportContentConfiguration : IEntityTypeConfiguration<ReportContent>
    {
        public void Configure(EntityTypeBuilder<ReportContent> builder)
        {
            builder.ToTable(nameof(ReportContent), "public")
                .HasKey(p => new { p.ReportId, p.LanguageId });

            builder.Property(r => r.Caption)
                .HasColumnType("text");

            builder.Property(r => r.Text)
                .HasColumnType("text");
        }
    }
}
