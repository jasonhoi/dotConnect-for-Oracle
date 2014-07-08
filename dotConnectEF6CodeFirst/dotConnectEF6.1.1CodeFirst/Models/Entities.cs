using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//[Table("AccountCurrencies")], all types of account only can use these supported currencies, ex. HKD, MOP, USD, EUR, AUD
public class AccountCurrency
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(10)]
    public string IsoCode { get; set; }
}

//[Table("ClientUsers")]
public class ClientUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Index(IsUnique = true)]
    [StringLength(20)]
    public string ClientId { get; set; }

    [Required]
    [Index(IsUnique = true)]
    [StringLength(30)]
    public string Username { get; set; }

    [Required]
    [StringLength(200)]
    public string PasswordHash { get; set; }

    [Required]
    [StringLength(100)]
    public string Email { get; set; }

    [StringLength(60)]
    public string Mobile { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
}

//[Table("CashAccounts")], shows current balance of each currency account for a client user.
public class CashAccount
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public virtual ClientUser Client { get; set; }

    [Required]
    public virtual AccountCurrency Currency { get; set; }

    [Required]
    public decimal Balance { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; }
}

//[Table("Transactions")], cash account transactions
public class Transaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public virtual CashAccount CashAccount { get; set; }

    [Required]
    public decimal Dr { get; set; }

    [Required]
    public decimal Cr { get; set; }

    [Required]
    public decimal Balance { get; set; }

    [Required]
    [StringLength(500)]
    public string Description { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
}

/*
//[Table("Product", Schema = "TEST")]
public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long ProductID { get; set; }

    [Required]
    [MaxLength(50)]
    public string ProductName { get; set; }

    public string UnitName { get; set; }
    public int UnitScale { get; set; }
    public long InStock { get; set; }
    public double Price { get; set; }
    public double DiscontinuedPrice { get; set; }

    public virtual ProductCategory Category { get; set; }
}

//[Table("ProductCategory", Schema = "TEST")]
public class ProductCategory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long CategoryID { get; set; }

    [Required]
    [MaxLength(50)]
    public string CategoryName { get; set; }

    public virtual ProductCategory ParentCategory { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}
*/