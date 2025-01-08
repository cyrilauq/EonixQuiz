using Microsoft.EntityFrameworkCore;
using People.Domain.Entities;

namespace People.Infrastructure.Data
{
    public class PeopleDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Person>()
                .Property(i => i.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Person>()
                .Property(p => p.Name)
                .IsRequired();
            modelBuilder.Entity<Person>()
                .Property(p => p.Name)
                .IsRequired();
        }
    }
}
