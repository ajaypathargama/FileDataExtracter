using FileDataExtracterApi.Model;
using Microsoft.EntityFrameworkCore;

namespace FileDataExtracterApi.Data
{
    public class FileDataExtracterDBContext : DbContext
    {
        public FileDataExtracterDBContext(DbContextOptions<FileDataExtracterDBContext> options) : base(options)
        {
            
        }

        public DbSet<Artikel> Artikels { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {             
            modelBuilder.Entity<Artikel>().HasKey(x => x.Key); //considering key is unique in the file
        }
    }
}
