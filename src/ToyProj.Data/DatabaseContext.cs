using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyProj.Abstractions.ResultData;
using ToyProj.Data.Data;

namespace ToyProj.Data
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<ProductionCompany> ProductionCompanies { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguageRoles> LanguageRoles { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<ProductionCountry> ProductionCountries { get; set; }
        public DbSet<MovieCompany> MovieCompany { get; set; }
        public DbSet<MovieLanguages> MovieLanguages { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieKeywords> MovieKeywords { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<MovieCrew> MovieCrews { get; set; }

        public DbSet<MovieRankingData> MovieRankingData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MovieRankingData>().HasNoKey().ToView(null);

            modelBuilder.Entity<ProductionCountry>()
                .HasKey(pc => new { pc.MovieId, pc.CountryId });

            modelBuilder.Entity<MovieCompany>()
                .HasKey(mc => new { mc.MovieId, mc.CompanyId });

            modelBuilder.Entity<MovieLanguages>()
                .HasKey(ml => new { ml.MovieId, ml.LanguageId, ml.LanguageRoleId });

            modelBuilder.Entity<MovieGenre>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });

            modelBuilder.Entity<MovieKeywords>()
                .HasKey(mk => new { mk.MovieId, mk.KeywordId });

            modelBuilder.Entity<MovieCast>()
                .HasKey(mc => new { mc.MovieId, mc.PersonId, mc.GenderId });

            modelBuilder.Entity<MovieCrew>()
                .HasKey(mc => new { mc.MovieId, mc.PersonId, mc.DepartmentId });
        }
    }
}
