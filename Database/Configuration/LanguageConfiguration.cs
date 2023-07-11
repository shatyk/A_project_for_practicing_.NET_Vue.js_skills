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
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable(nameof(Language), "public")
                .HasKey(l => l.Id);

            builder.Property(l => l.Name)
                .HasColumnType("text");

            builder.HasMany<FundraisingContent>()
                .WithOne()
                .HasForeignKey(f => f.LanguageId);

            builder.HasMany<ReportContent>()
                .WithOne()
                .HasForeignKey(f => f.LanguageId);
        }
    }
}
