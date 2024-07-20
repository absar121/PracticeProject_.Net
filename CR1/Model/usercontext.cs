using Microsoft.EntityFrameworkCore;
using System;

namespace CR1.Model
{
    public class usercontext:DbContext
    {
        public usercontext(DbContextOptions<usercontext> options):base(options) { }
        public DbSet<user> user { get; set; }
        public DbSet<login> login { get; set; }
        //public DbSet<user> dummyuser { get; set; }

        // protected override void OnConfiguration(DbContextOptionsBuilder optionsBuilder)
        //{
        //  optionsBuilder.UseMySQL(connection string)
        //}
    }
   
}
