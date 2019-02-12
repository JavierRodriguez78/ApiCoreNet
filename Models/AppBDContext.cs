using Microsoft.EntityFrameworkCore;
using System.Data;
namespace ApiGTT.Models{

    public class AppBDContext: DbContext{
        
       public AppBDContext(DbContextOptions<AppBDContext> options) : 
        base(options){
        }

         public DbSet<Jira> Jira{get; set;}
         public DbSet<Users> Users{get; set;}

         public DbSet<Certificates> Certificates{get; set;}
    }

}