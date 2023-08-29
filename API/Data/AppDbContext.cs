using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using API.Models.Domain;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace API.Data
{
	public class AppDbContext:DbContext
	{
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasMany(r => r.room).WithOne(a => a.author).HasForeignKey(ai => ai.authorId);
            modelBuilder.Entity<Room_Category>().HasOne(c => c.category).WithMany(cr => cr.category_room).HasForeignKey(ci => ci.categoryId);
            modelBuilder.Entity<Room_Category>().HasOne(r => r.room).WithMany(rc => rc.room_category).HasForeignKey(ri => ri.roomId);
            modelBuilder.Entity<Room>().Property(p => p.price).HasPrecision(18, 2);
            modelBuilder.Entity<Room>().HasMany(p=>p.room_photo).WithOne(r=>r.room).HasForeignKey(ri=>ri.roomId);
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Room_Category> Room_Category { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Room> Room { get; set; }

    }
}

