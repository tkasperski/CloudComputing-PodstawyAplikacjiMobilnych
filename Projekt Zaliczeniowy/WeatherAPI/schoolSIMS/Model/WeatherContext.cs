using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace schoolSIMS.Model
{
    public partial class WeatherContext : DbContext
    {
        public WeatherContext()
        {
        }

        public WeatherContext(DbContextOptions<WeatherContext> options)
            : base(options)
        {
        }

        public virtual DbSet<WeatherInfo> WeatherInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:weatherserverkmtk.database.windows.net,1433;Initial Catalog=WeatherDB;Persist Security Info=False;User ID=azureuser;Password=Azure123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<WeatherInfo>(entity =>
            {
                entity.Property(e => e.Temperature).IsUnicode(false);

                entity.Property(e => e.DescTemp).IsUnicode(false);

                entity.Property(e => e.Humidity).IsUnicode(false);

                entity.Property(e => e.Wind).IsUnicode(false);

                entity.Property(e => e.Gauge).IsUnicode(false);

                entity.Property(e => e.Cloudiness).IsUnicode(false);

                entity.Property(e => e.City).IsUnicode(false);
            });
        }
    }
}
