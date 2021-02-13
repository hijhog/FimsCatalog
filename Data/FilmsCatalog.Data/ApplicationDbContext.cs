using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using FilmsCatalog.Data.Contract.Identity;
using FilmsCatalog.Data.Contract.Entities;
using Microsoft.AspNetCore.Identity;

namespace FilmsCatalog.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Movie>()
                .HasOne(x => x.CreatedBy).WithMany(x => x.Movies).HasForeignKey(x => x.CreatedById);
        }
    }
}
