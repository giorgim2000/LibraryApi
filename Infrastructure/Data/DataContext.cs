using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Entities.UserAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data
{
    public class DataContext : IdentityDbContext<AppUser,AppRole,int>
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<BookRentalHistory> RentalHistory { get; set; } = null!;
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
                book.HasMany(x => x.Authors).WithMany(x => x.Books)
                                            .UsingEntity("AuthorBook",
                                            l => l.HasOne(typeof(Author)).WithMany()
                                                            .HasForeignKey("AuthorsId")
                                                            .HasPrincipalKey(nameof(Author.Id)),
                                            r => r.HasOne(typeof(Book)).WithMany()
                                                            .HasForeignKey("BooksId")
                                                            .HasPrincipalKey(nameof(Book.Id)),
                                            j => j.HasData(new[]
                                                            {
                                                                new {  AuthorsId = 1, BooksId= 3},
                                                                new {  AuthorsId = 2, BooksId= 2},
                                                                new {  AuthorsId = 3, BooksId= 4},
                                                                new {  AuthorsId = 3, BooksId= 5},
                                                                new {  AuthorsId = 4, BooksId= 1}
                                                            })
                                            );


                book.HasMany(x => x.Rentals).WithOne(x => x.Book).HasPrincipalKey(i => i.Id);
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

            modelBuilder.Entity<BookRentalHistory>(brh =>
            {
                brh.ToTable("BookRentalHistory");
                brh.HasKey("Id");
                brh.HasIndex(x => x.Id).IsUnique();
                brh.HasOne(x => x.User).WithMany(x => x.Rentals).HasForeignKey(i => i.UserId);
                brh.Property(x => x.UserId).IsRequired();
                brh.HasOne(x => x.Book).WithMany(x => x.Rentals).HasForeignKey(i => i.BookId);
                brh.Property(x => x.BookId).IsRequired();
                brh.Property(x => x.CreationDate).IsRequired().HasColumnType("date");
                brh.Property(x => x.Status).IsRequired();
            });


            modelBuilder.Entity<Book>().HasData(new[]
            {
                new Book()
                {
                    Id = 1,
                    Title = "The Catcher in the Rye",
                    Description = "J.D. Salinger",
                    Rating = 9.99,
                    Year = new DateTime(1951, 7, 16),
                    Taken = false,
                    Image = null
                },
                new Book()
                {
                    Id = 2,
                    Title = "To Kill a Mockingbird",
                    Description = "Harper Lee",
                    Rating = 12.99,
                    Year = new DateTime(1960, 7, 11),
                    Taken = false,
                    Image = null
                },
                new Book()
                {
                    Id = 3,
                    Title = "1984",
                    Description = "George Orwell",
                    Rating = 8.99,
                    Year =  new DateTime(1949, 6, 8),
                    Taken = false,
                    Image = null
                },
                new Book()
                {
                    Id = 4,
                    Title = "The Hobbit",
                    Description = "J.R.R. Tolkien",
                    Rating = 14.99,
                    Year = new DateTime(1937, 9, 21),
                    Taken = false,
                    Image = null
                },
                new Book()
                {
                    Id = 5,
                    Title = "The Lord of the Rings",
                    Description = "J.R.R. Tolkien",
                    Rating = 29.99,
                    Year = new DateTime(1954, 7, 29),
                    Taken = false,
                    Image = null
                }
            });

            modelBuilder.Entity<Author>().HasData(new[]
            {
                new Author("George", "Orwell", new DateTime(1933,12,25)){ Id = 1},
                new Author("Harper", "Lee", new DateTime(1953,5,1)){ Id = 2},
                new Author("J.R.R.", "Tolkien", new DateTime(1961,8,19)){ Id = 3},
                new Author("J.D.", "Salinger", new DateTime(1957,5,9)){ Id = 4}
            });
            
            modelBuilder.Entity<AppRole>().HasData(new[] 
            { 
                new AppRole { Id = 1, Name = "User", NormalizedName = "USER", ConcurrencyStamp=Guid.NewGuid().ToString() }, 
                new AppRole { Id = 2, Name = "Admin", NormalizedName = "ADMIN",ConcurrencyStamp=Guid.NewGuid().ToString() } 
            });

            AppUser adminUser = new AppUser()
            {
                Id = 1,
                UserName = "admin",
                Email = "admin@admin.com",
                NormalizedUserName = "ADMIN",
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var hasher = new PasswordHasher<AppUser>();
            var hashedPassword = hasher.HashPassword(adminUser, "admin");
            adminUser.PasswordHash = hashedPassword;
            modelBuilder.Entity<AppUser>().HasData(adminUser);

            var adminAccRole = new IdentityUserRole<int> { UserId = 1, RoleId = 2 };
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(adminAccRole);

           

            base.OnModelCreating(modelBuilder);
        }
    }

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<DataContext>().UseSqlServer($"Server=.; Database=LibraryDB;Encrypt=false;Trusted_Connection=True;");
            return new DataContext(options.Options);
        }
    }
}
