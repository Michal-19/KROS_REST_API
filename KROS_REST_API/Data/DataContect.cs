﻿using KROS_REST_API.Models;
using Microsoft.EntityFrameworkCore;

namespace KROS_REST_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Employee> employees { get; set; }
    }
}
