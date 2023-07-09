using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User), "public")
                .HasKey(x => x.Id);

            builder.Property(x => x.Password)
            .HasColumnType("varchar(120)");

            builder.Property(x => x.Username)
                .HasColumnType("varchar(120)");

            builder.HasMany(x => x.RefreshTokens)
                .WithOne()
                .HasForeignKey(x => x.UserId);
        }
    }
}
