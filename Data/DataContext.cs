﻿using Library_API.Models;
using Microsoft.EntityFrameworkCore;
namespace Library_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }

        public DbSet<BookWithId> Books => Set<BookWithId>();
    }
}
