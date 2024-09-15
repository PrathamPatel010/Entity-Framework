﻿using Microsoft.EntityFrameworkCore;

namespace DbOperations.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Currency>().HasData(
                new Currency() { Id = 1 ,Title="INR",Description="Indian INR"},
                new Currency() { Id = 2 ,Title="Dollar",Description="American Dollar"},
                new Currency() { Id = 3 ,Title="Euro",Description="Euro"},
                new Currency() { Id = 4 ,Title="Dinar",Description="Dinar"}
            );

            modelBuilder.Entity<Language>().HasData(
                new Currency() { Id = 1, Title = "English", Description = "English" },
                new Currency() { Id = 2, Title = "Gujarati", Description = "Gujarati" },
                new Currency() { Id = 3, Title = "Hindi", Description = "Hindi" },
                new Currency() { Id = 4, Title = "German", Description = "German" }
            );
        }

        public DbSet<Book> Books{ get; set;}
        public DbSet<Language> Languages{ get; set;}
        public DbSet<BookPrice> BookPrices{ get; set;}
        public DbSet<Currency> Currencies{ get; set;}


    }
}
