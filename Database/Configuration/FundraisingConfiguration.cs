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
    public class FundraisingConfiguration : IEntityTypeConfiguration<Fundraising>
    {
        public void Configure(EntityTypeBuilder<Fundraising> builder)
        {
            builder.ToTable(nameof(Fundraising), "public")
                .HasKey(t => t.Id);

            builder.Property(r => r.Capture)
                .HasColumnType("text");

            builder.Property(r => r.Text)
                .HasColumnType("text");

            builder.HasMany(f => f.Reports)
                .WithOne(r => r.Fundraising)
                .HasForeignKey(r => r.FundraisingId);

        }
    }
}
