using Microsoft.EntityFrameworkCore;

namespace SnippetApp.Models
{
  public class MyDbContext : DbContext
  {
    public DbSet<Snippet> Snippets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      optionsBuilder.UseSqlite("DataSource=Snippets.db");
    }
  }
}