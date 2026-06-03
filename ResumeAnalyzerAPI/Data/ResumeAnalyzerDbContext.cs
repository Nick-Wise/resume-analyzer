using ResumeAnalyzerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace ResumeAnalyzerAPI.Data
{
    public class ResumeAnalyzerDbContext : DbContext
    {
        public ResumeAnalyzerDbContext(DbContextOptions<ResumeAnalyzerDbContext> options) : base(options) { }

        public DbSet<AnalysisRecord> AnalysisRecord { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        var listComparer = new ValueComparer<List<string>>(
            (c1, c2) => c1!.SequenceEqual(c2!),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => c.ToList()
        );
            ConfigureListProperty<AnalysisRecord>(modelBuilder, p => p.skills, listComparer);
            ConfigureListProperty<AnalysisRecord>(modelBuilder, p => p.matchedSkills, listComparer);
            ConfigureListProperty<AnalysisRecord>(modelBuilder, p => p.unmatchedSkills, listComparer);
        }

        private static void ConfigureListProperty<TEntity>(
            ModelBuilder modelBuilder,
            System.Linq.Expressions.Expression<Func<TEntity, List<string>>> property,
            ValueComparer<List<string>> comparer) where TEntity : class
        {
            modelBuilder.Entity<TEntity>()
                .Property(property)
                .HasColumnType("Text")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, JsonSerializerOptions.Default),
                    v => JsonSerializer.Deserialize<List<string>>(v, JsonSerializerOptions.Default) ?? new List<string>()
                )
                .Metadata.SetValueComparer(comparer);
        }

    }
}