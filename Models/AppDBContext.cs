using Microsoft.EntityFrameworkCore;

namespace ApiGTT.Models{

    public class AppDBContext: DbContext{
        public AppDBContext(DbContextOptions<AppDBContext> options) : 
        base(options){
        }

         public DbSet<Jira> Jira{get; set;}
         public DbSet<Users> Users{get; set;}

         public DbSet<Certificates> Certificates{get; set;}
    }

}