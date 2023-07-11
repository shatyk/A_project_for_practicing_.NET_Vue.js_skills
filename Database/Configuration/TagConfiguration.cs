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
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable(nameof(Tag), "public")
                .HasKey(t => t.Id);

            builder.Property(t => t.Text)
                .HasColumnType("text");

            builder.HasIndex(t => t.Text).IsUnique();

            builder.HasMany(r => r.ReportTags)
                .WithOne(rt => rt.Tag)
                .HasForeignKey(rt => rt.TagId);
        }
    }
}
