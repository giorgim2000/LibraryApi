using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(book =>
            {
                book.ToTable("Books");
                book.HasKey(x => x.Id);
                book.HasIndex(x => x.Id).IsUnique();
                book.Property(x => x.Title).IsRequired().HasMaxLength(100);
                book.Property(x => x.Description).HasMaxLength(300).IsRequired(false);
                book.Property(x => x.Image).IsRequired(false);
                book.Property(x => x.Rating).IsRequired(false);
                book.Property(x => x.Year).IsRequired(false).HasColumnType("date");
                book.Property(x => x.Taken).IsRequired();
                book.HasMany(x => x.Authors).WithMany(x => x.Books);
            });

            modelBuilder.Entity<Author>(author =>
            {
                author.ToTable("Authors");
                author.HasKey(x => x.Id);
                author.HasIndex(x => x.Id).IsUnique();
                author.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
                author.Property(x => x.LastName).IsRequired().HasMaxLength(100);
                author.Property(x => x.BirthDate).IsRequired(false).HasColumnType("date");
                author.HasMany(x => x.Books).WithMany(x => x.Authors);
            });
                
            base.OnModelCreating(modelBuilder);
        }
    }

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<DataContext>().UseSqlServer($"Server={Environment.MachineName}\\SQLEXPRESS; Database=LibraryDB;Encrypt=false;Trusted_Connection=True;");
            return new DataContext(options.Options);
        }
    }
}
