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

        public DbSet<Country> Country { get; set; }
        public DbSet<ProductionCompany> ProductionCompany { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguageRoles> LanguageRole { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Keyword> Keyword { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<ProductionCountry> ProductionCountry { get; set; }
        public DbSet<MovieCompany> MovieCompany { get; set; }
        public DbSet<MovieLanguages> MovieLanguages { get; set; }
        public DbSet<MovieGenre> MovieGenre { get; set; }
        public DbSet<MovieKeywords> MovieKeywords { get; set; }
        public DbSet<MovieCast> MovieCast { get; set; }
        public DbSet<MovieCrew> MovieCrew { get; set; }

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
