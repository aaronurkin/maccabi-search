using MaccabiSearch.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MaccabiSearch.Domain.Services.Implementations
{
    public class MaccabiSearchDbContext : DbContext
    {
        public MaccabiSearchDbContext(DbContextOptions<MaccabiSearchDbContext> options)
            : base(options) { }

        public DbSet<SearchResultPgEntity> SearchResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<SearchResultPgEntity>(searchResult =>
                {
                    searchResult
                        .ToTable("search_results");

                    searchResult
                        .HasKey(sr => sr.Id);

                    searchResult
                        .Property(sr => sr.Id)
                        .HasColumnName("id")
                        .IsRequired()
                        .HasDefaultValueSql("uuid_generate_v4()");

                    searchResult
                        .Property(sr => sr.Title)
                        .HasColumnName("title")
                        // TODO: Configure max lenth according to the Search Engine maximum length
                        .HasMaxLength(255)
                        .IsRequired();

                    searchResult
                        .HasIndex(sr => sr.Title);

                    searchResult
                        .Property(sr => sr.SearchEngine)
                        .HasColumnName("search_engine")
                        .IsRequired();

                    searchResult
                        .Property(sr => sr.EnteredDate)
                        .HasColumnName("entered_date")
                        .IsRequired()
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");
                });
        }
    }
}
