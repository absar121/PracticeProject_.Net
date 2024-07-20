using Microsoft.EntityFrameworkCore;
using System;


namespace CR1.Model
{
    public class dummycontext : DbContext
    {

        public dummycontext(DbContextOptions<usercontext> options) : base(options) { }
      
        public DbSet<user> dummyuser { get; set; }
    }
}
