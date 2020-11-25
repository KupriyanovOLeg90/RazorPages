﻿using Microsoft.EntityFrameworkCore;
using RazorPagesLessons.Models;


namespace RazorPagesLessons.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employe> Employes { get; set; }
    }
}
