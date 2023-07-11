using Database.Configuration;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Fundraising> Fundraisings { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<ReportTag> ReportTags { get; set; }    
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<ReportContent> ReportContents { get; set; }
        public virtual DbSet<FundraisingContent> FundraisingContents { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new FundraisingConfiguration());
            modelBuilder.ApplyConfiguration(new ReportConfiguration());
            modelBuilder.ApplyConfiguration(new ReportTagConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
            modelBuilder.ApplyConfiguration(new ReportContentConfiguration());
            modelBuilder.ApplyConfiguration(new FundraisingContentConfiguration());
        }
    }
}