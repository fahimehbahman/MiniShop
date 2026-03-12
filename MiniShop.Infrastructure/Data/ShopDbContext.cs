using Microsoft.EntityFrameworkCore;

using MiniShop.Domain.Entities;
using System.Reflection.Emit;


namespace MiniShop.Infrastructure.Data;

public class ShopDbContext : DbContext
 {

   public ShopDbContext(DbContextOptions<ShopDbContext> options):base (options)
   {
    
   }

   public DbSet<User> Users=>Set<User>();
   public DbSet<Product> Products=> Set<Product>();
   public DbSet<Order> Order=> Set<Order>();

   protected override void OnModelCreating(ModelBuilder  modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopDbContext).Assembly);
    }


 }