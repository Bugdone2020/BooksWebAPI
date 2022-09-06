using Microsoft.EntityFrameworkCore;
using System;
using BooksWebAPI_DAL.Entities;

namespace BooksWebAPI_DAL
{
    public class EFCoreDbContext: DbContext
    {
        public DbSet<Book> Books { get; set;}
        public DbSet<Client> Clients { get; set;}

        public EFCoreDbContext(DbContextOptions<EFCoreDbContext> options) : base(options)
        { 
            Database.EnsureCreated();
        }
    }
}
