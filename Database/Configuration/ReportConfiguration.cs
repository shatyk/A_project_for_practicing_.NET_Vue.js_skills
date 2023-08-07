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
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable(nameof(Report), "public")
                .HasKey(r => r.Id);

            builder.HasMany(r => r.ReportTags)
                .WithOne(rt => rt.Report)
                .HasForeignKey(rt => rt.ReportId);

            builder.HasMany(r => r.Contents)
                .WithOne()
                .HasForeignKey(rc => rc.ReportId);
        }
    }
}
