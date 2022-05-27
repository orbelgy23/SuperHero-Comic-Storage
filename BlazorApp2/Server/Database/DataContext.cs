using BlazorApp2.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp2.Server.Database
{
    public class DataContext : DbContext
    {
        public DbSet<SuperHero> SuperHeroes { get; set; }
        public DbSet<Comic> Comics { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    var folder = Environment.SpecialFolder.LocalApplicationData;
        //    var path = Environment.GetFolderPath(folder);
        //    var DbPath = System.IO.Path.Join(path, "SuperHeroDB.db");
        //    options.UseSqlite($"Data Source={DbPath}");
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Comic>().HasData(
        //        new Comic { Id = 1, Name = "Marvel" },
        //        new Comic { Id = 2, Name = "DC" }
        //    );

        //    modelBuilder.Entity<Comic>().HasData(
        //        new SuperHero
        //        {
        //            Id = 1,
        //            FirstName = "Peter",
        //            LastName = "Parker",
        //            HeroName = "Spiderman",
        //            ComicId = 1
        //        },

        //        new SuperHero
        //        {
        //            Id = 2,
        //            FirstName = "Bruce",
        //            LastName = "Wayne",
        //            HeroName = "Batman",
        //            ComicId = 2
        //        }
        //    );
        //}
    }
}
