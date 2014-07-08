using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Common;

public class MyDbContext : DbContext
{

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<AccountCurrency> AccountCurrencies { get; set; }
    public DbSet<CashAccount> CashAccounts { get; set; }
    public DbSet<ClientUser> ClientUsers { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public MyDbContext()
        : base()
    {
    }

    public MyDbContext(DbConnection connection)
        : base(connection, true)
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        // Map all decimal field to Oracle NUMBER(28,8)
        modelBuilder.Properties<decimal>().Configure(config => config.HasPrecision(28, 8));

        /*-------------------------------------------------------------
        If you don't want to create and use EdmMetadata table
        for monitoring the correspondence
        between the current model and table structure
        created in a database, then turn off IncludeMetadataConvention:
        -------------------------------------------------------------*/
        /*
        modelBuilder.Conventions
          .Remove<System.Data.Entity.Infrastructure.IncludeMetadataConvention>();
        */

        /*-------------------------------------------------------------
        In the sample above we have defined autoincrement columns in the primary key
        and non-nullable columns using DataAnnotation attributes.
        Similarly, the same can be done with Fluent mapping
        -------------------------------------------------------------*/

        //modelBuilder.Entity<Product>().HasKey(p => p.ProductID);
        //modelBuilder.Entity<Product>().Property(p => p.ProductID)
        //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        //modelBuilder.Entity<Product>().Property(p => p.ProductName)
        //    .IsRequired()
        //    .HasMaxLength(50);
        //modelBuilder.Entity<ProductCategory>().HasKey(p => p.CategoryID);
        //modelBuilder.Entity<ProductCategory>().Property(p => p.CategoryID)
        //    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        //modelBuilder.Entity<ProductCategory>().Property(p => p.CategoryName)
        //    .IsRequired()
        //    .HasMaxLength(20);
        //modelBuilder.Entity<Product>().ToTable("Product", "TEST");
        //modelBuilder.Entity<ProductCategory>().ToTable("ProductCategory", "TEST");

        //-------------------------------------------------------------//
    }
}

public class MyDbContextDropCreateDatabaseAlways
 : DropCreateDatabaseAlways<MyDbContext>
{
    protected override void Seed(MyDbContext context)
    {
        MyDbContextSeeder.Seed(context);
    }
}

public class MyDbContextDropCreateDatabaseIfModelChanges
  : DropCreateDatabaseIfModelChanges<MyDbContext>
{
    protected override void Seed(MyDbContext context)
    {
        MyDbContextSeeder.Seed(context);
    }
}

public class MyDbContextCreateDatabaseIfNotExists
  : CreateDatabaseIfNotExists<MyDbContext>
{
    protected override void Seed(MyDbContext context)
    {
        MyDbContextSeeder.Seed(context);
    }
}

public static class MyDbContextSeeder
{
    public static void Seed(MyDbContext context)
    {
        context.ProductCategories.Add(new ProductCategory()
        {
            CategoryName = "prose"
        });
        context.ProductCategories.Add(new ProductCategory()
        {
            CategoryName = "novel"
        });
        context.ProductCategories.Add(new ProductCategory()
        {
            CategoryName = "poem",
            ParentCategory =
             context.ProductCategories.Local.Single(p => p.CategoryName == "novel")
        });
        context.ProductCategories.Add(new ProductCategory()
        {
            CategoryName = "fantasy",
            ParentCategory =
              context.ProductCategories.Local.Single(p => p.CategoryName == "novel")
        });
        context.Products.Add(new Product()
        {
            ProductName = "Shakespeare W. Shakespeare's dramatische Werke",
            Price = 78,
            Category =
              context.ProductCategories.Local.Single(p => p.CategoryName == "prose")
        });
        context.Products.Add(new Product()
        {
            ProductName = "Plutarchus. Plutarch's moralia",
            Price = 89,
            Category =
              context.ProductCategories.Local.Single(p => p.CategoryName == "prose")
        });
        context.Products.Add(new Product()
        {
            ProductName = "Harrison G. B. England in Shakespeare's day",
            Price = 540,
            Category =
              context.ProductCategories.Local.Single(p => p.CategoryName == "novel")
        });
        context.Products.Add(new Product()
        {
            ProductName = "Corkett Anne. The salamander's laughter",
            Price = 5,
            Category =
              context.ProductCategories.Local.Single(p => p.CategoryName == "poem")
        });
    }
}
