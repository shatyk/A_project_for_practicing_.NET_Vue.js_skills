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
    public class ReportTagConfiguration : IEntityTypeConfiguration<ReportTag>
    {
        public void Configure(EntityTypeBuilder<ReportTag> builder)
        {
            builder.ToTable(nameof(ReportTag), "public")
                .HasKey(rt => rt.Id);

            builder.HasIndex(rt => new { rt.ReportId, rt.TagId })
                .IsUnique();
        }
    }
}
